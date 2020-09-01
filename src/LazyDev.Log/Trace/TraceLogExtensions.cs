using System;
using Microsoft.Extensions.Logging;

namespace LazyDev.Log.Trace
{
    public static class TraceLogExtensions
    {
        private static readonly EventId TraceEventId = new EventId(-112, "Trace");
        internal static void LogTrace(this ILogger logger, LogLevel logLevel, TraceMessage state, Exception exception = null)
        {
            logger.Log(logLevel, TraceEventId, state, exception, (m, e) => m?.ToString());
        }
    }
}
