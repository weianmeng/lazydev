using Microsoft.Extensions.Logging;
using System;
using LazyDev.Utilities;

namespace LazyDev.Log
{
    public class LazyLogger : ILogger
    {
        private readonly string _appId;
        private readonly string _hostIp;
        private readonly string _name;
        private readonly ITraceSession _traceSession;
        private readonly ILoggerProcessor _loggerProcessor;
        public bool Console { get; internal set; }

        public Func<string, LogLevel, bool> Filter { get; internal set; }
        private readonly ConsolePrint _consolePrint;

        public LazyLogger(string appId, string name, bool console, Func<string, LogLevel, bool> filter, ITraceSession traceSession, ILoggerProcessor loggerProcessor)
        {

            _appId = appId;
            _name = name;
            _traceSession = traceSession;
            _loggerProcessor = loggerProcessor;
            Console = console;
            _hostIp = NetUtility.GetHostIp();
            Filter = filter ?? ((category, logLevel) => true);
            _consolePrint = new ConsolePrint();
        }

        /// <summary>
        /// 写日志
        /// </summary>
        /// <typeparam name="TState"></typeparam>
        /// <param name="logLevel"></param>
        /// <param name="eventId"></param>
        /// <param name="state"></param>
        /// <param name="exception"></param>
        /// <param name="formatter"></param>
        public void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception exception, Func<TState, Exception, string> formatter)
        {
            if (!IsEnabled(logLevel))
            {
                return;
            }

            LogMessage baseMessage;
            if (state is LogMessage message)
            {
                baseMessage = message;
            }
            else
            {
                baseMessage = new LogMessage()
                {
                    Message = state.ToString()
                };
            }
            //补齐日志信息
            AppendLogMessage(logLevel, eventId, exception, baseMessage);
            //日志投入内存队列处理
            //_loggerProcessor.Enqueue(baseMessage);
            //是否控制台打印
            if (Console)
            {
                _consolePrint.Writer(logLevel, baseMessage);
            }

        }

        /// <summary>
        ///  补齐日志信息
        /// </summary>
        /// <param name="logLevel"></param>
        /// <param name="eventId"></param>
        /// <param name="exception"></param>
        /// <param name="baseMessage"></param>
        private void AppendLogMessage(LogLevel logLevel, EventId eventId, Exception exception, LogMessage baseMessage)
        {
            baseMessage.AppId = _appId;
            baseMessage.LogName = _name;
            baseMessage.HostIp = _hostIp;
            baseMessage.LogLevel = GetLogLevelString(logLevel);
            baseMessage.EventId = eventId.Id;
            baseMessage.LogType = eventId.ToString();
            baseMessage.Exception = exception?.ToString();
            baseMessage.LogTime = DateTime.Now;
            baseMessage.TraceId = _traceSession.TraceId;
            baseMessage.ChainId = _traceSession.ChainId;
            baseMessage.ParentTraceId = _traceSession.ParentTraceId;
        }


        public bool IsEnabled(LogLevel logLevel)
        {
            return logLevel != LogLevel.None && Filter(_name, logLevel);
        }

        public IDisposable BeginScope<TState>(TState state)
        {
            return null;
            //throw new NotImplementedException();
        }

        private static string GetLogLevelString(LogLevel logLevel)
        {
            return logLevel switch
            {
                LogLevel.Trace => "trace",
                LogLevel.Debug => "debug",
                LogLevel.Information => "information",
                LogLevel.Warning => "warn",
                LogLevel.Error => "error",
                LogLevel.Critical => "critical",
                LogLevel.None => "none",
                _ => throw new ArgumentOutOfRangeException(nameof(logLevel))
            };
        }
    }
}
