using System;
using Microsoft.Extensions.Logging;
using System.Collections.Concurrent;
using System.Collections.Generic;
using Microsoft.Extensions.Options;

namespace LazyDev.Log
{
    public class LazyDevLoggerProvider : ILoggerProvider
    {
        private readonly IOptionsMonitor<LazyDevLoggerOptions> _optionsMonitor;
        private readonly ILogWriter _logWriter;
        private readonly ITraceSession _traceSession;


        private readonly ConcurrentDictionary<string, LazyLogger> _loggers =
            new ConcurrentDictionary<string, LazyLogger>();

        public LazyDevLoggerProvider(
            IOptionsMonitor<LazyDevLoggerOptions> options,
            ILogWriter logWriter,
            ITraceSession traceSession)
        {
            _optionsMonitor = options;
            _logWriter = logWriter;
            _traceSession = traceSession;
            ReloadOption(options.CurrentValue);
            //配置文件发生变动
           options.OnChange(ReloadOption);
           
        }
        /// <summary>
        /// 配置文件发生变动
        /// </summary>
        /// <param name="options"></param>
        private void ReloadOption(LazyDevLoggerOptions options)
        {
            foreach (var logger in _loggers.Values)
            {
                logger.Console = options?.Console ?? false;
            }
        }

        public ILogger CreateLogger(string categoryName)
        {
            return _loggers.GetOrAdd(categoryName, name =>
                new LazyLogger(_optionsMonitor.CurrentValue.AppId,
                    name,
                    _optionsMonitor.CurrentValue.Console,
                    LogLevelFilter(name,_optionsMonitor.CurrentValue),
                    _traceSession,
                    new LoggerProcessor(_logWriter,_optionsMonitor.CurrentValue.MaxQueuedMessageCount))
            );
        }

        private Func<string, LogLevel, bool> LogLevelFilter(string name,LazyDevLoggerOptions options)
        {
            if (options == null) return (category, logLevel) => false;

            foreach (var prefix in this.GetKeyPrefixes(name))
            {
                if (options.LogLevel.TryGetValue(prefix, out var level))
                {
                    return (n, l) => l >= level;
                }
            }
            return (category, logLevel) => false;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        private IEnumerable<string> GetKeyPrefixes(string name)
        {
            while (!string.IsNullOrEmpty(name))
            {
                yield return name;
                var lastIndexOfDot = name.LastIndexOf('.');
                if (lastIndexOfDot == -1)
                {
                    yield return "Default";
                    break;
                }
                name = name.Substring(0, lastIndexOfDot);
            }
        }

        public void Dispose()
        {
            _loggers.Clear();
        }
    }
}
