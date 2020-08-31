using Microsoft.Extensions.Logging;
using System;

namespace LazyDev.Log
{
    public class LazyLogger:ILogger
    {
        private readonly string _name;
        private readonly LazyDevLoggerConfiguration _config;

        public LazyLogger(LazyDevLoggerConfiguration config, string name)
        {
            _config = config;
            _name = name;
        }

        public void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception exception, Func<TState, Exception, string> formatter)
        {
            if (!IsEnabled(logLevel))
            {
                return;
            }

            if (_config.EventId == 0 || _config.EventId == eventId.Id)
            {
                var color = Console.ForegroundColor;
                Console.ForegroundColor = _config.Color;
                Console.WriteLine($"{logLevel} - {eventId.Id} " +
                                  $"- {_name} - {formatter(state, exception)}");
                Console.ForegroundColor = color;
            }
        }

        public bool IsEnabled(LogLevel logLevel)
        {
            return logLevel == _config.LogLevel;
        }

        public IDisposable BeginScope<TState>(TState state)
        {
            return null;
            //throw new NotImplementedException();
        }
    }
}
