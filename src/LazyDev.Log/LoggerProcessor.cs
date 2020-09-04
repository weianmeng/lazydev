using System.Collections.Concurrent;
using System.Threading;

namespace LazyDev.Log
{
    public class LoggerProcessor : ILoggerProcessor
    {
        private readonly ILogWriter _writer;
        private readonly BlockingCollection<LogMessage> _messageQueue;
        private readonly Thread _outputThread;
        public LoggerProcessor(ILogWriter writer,int maxQueuedMessageCount)
        {
            _writer = writer;
            _messageQueue = new BlockingCollection<LogMessage>(maxQueuedMessageCount);
            _outputThread = new Thread(ProcessLogQueue)
            {
                IsBackground = true,
                Name = "logger queue processing thread"
            };
            _outputThread.Start();
        }

        public void Enqueue(LogMessage message)
        {
            if (!_messageQueue.TryAdd(message))
            {
               //消息队列添加失败
               //写个本地文件日志？
            }
        }

        private void ProcessLogQueue()
        {
            try
            {
                foreach (var message in _messageQueue.GetConsumingEnumerable())
                {
                    _writer.Write(message);
                    _writer.Flush();
                }
            }
            catch
            {
                _messageQueue.CompleteAdding();
            }
        }

        public void Dispose()
        {
            _outputThread.Join(1500);
            _messageQueue.Dispose();
        }
    }
}
