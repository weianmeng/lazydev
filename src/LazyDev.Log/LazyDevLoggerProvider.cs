using Microsoft.Extensions.Logging;
using System.Collections.Concurrent;

namespace LazyDev.Log
{
    public class LazyDevLoggerProvider : ILoggerProvider
    {

        private readonly ConcurrentDictionary<string, LazyLogger> _loggers = new ConcurrentDictionary<string, LazyLogger>();



        public ILogger CreateLogger(string categoryName)
        {
            return _loggers.GetOrAdd(categoryName, name => new LazyLogger("am.app.web",name,true,null));
        }

        public void Dispose()
        {
            _loggers.Clear();
        }
    }
}
