using Microsoft.Extensions.Logging;
using System.Collections.Concurrent;

namespace LazyDev.Log
{
    public class LazyDevLoggerProvider : ILoggerProvider
    {
        private readonly LazyDevLoggerConfiguration _config;
        private readonly ConcurrentDictionary<string, LazyLogger> _loggers = new ConcurrentDictionary<string, LazyLogger>();

        public LazyDevLoggerProvider(LazyDevLoggerConfiguration config)
        {
            _config = config;
        }

        public ILogger CreateLogger(string categoryName)
        {
            return _loggers.GetOrAdd(categoryName, name => new LazyLogger(_config,name));
        }

        public void Dispose()
        {
            _loggers.Clear();
        }
    }
}
