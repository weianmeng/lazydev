using Microsoft.Extensions.Logging;

namespace LazyDev.Log
{
    internal class BaseMessage
    {
        public string AppId { get; set; }
        public string MessageType { get; set; }
        public LogLevel LogLevel { get; set; }
        public string ChainId { get; set; }
        public string TraceId { get; set; }
        public string ParentTraceId { get; set; }
    }
}
