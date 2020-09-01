using Microsoft.Extensions.Logging;
using System;
using LazyDev.Utilities.Extensions;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace LazyDev.Log
{
    public class LazyLogger:ILogger
    {
        private readonly LazyDevLoggerConfiguration _config;

        public LazyLogger(LazyDevLoggerConfiguration config)
        {
            _config = config;
        }

        public void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception exception, Func<TState, Exception, string> formatter)
        {
            if (!IsEnabled(logLevel))
            {
                return;
            }

            ConsoleWrite(logLevel, state);
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

        private static void ConsoleWrite<TState>(LogLevel logLevel, TState state)
        {
            ConsoleColors consoleColors;
            switch (logLevel)
            {
                case LogLevel.Critical:
                    consoleColors = new ConsoleColors(ConsoleColor.White, ConsoleColor.Red);
                    break;
                case LogLevel.Error:
                    consoleColors = new ConsoleColors(ConsoleColor.Black, ConsoleColor.Red);
                    break;
                case LogLevel.Warning:
                    consoleColors = new ConsoleColors(ConsoleColor.Yellow, ConsoleColor.Black);
                    break;
                case LogLevel.Information:
                    consoleColors = new ConsoleColors(ConsoleColor.DarkGreen, ConsoleColor.Black);
                    break;
                case LogLevel.Debug:
                    consoleColors = new ConsoleColors(ConsoleColor.Gray, ConsoleColor.Black);
                    break;
                case LogLevel.Trace:
                    consoleColors = new ConsoleColors(ConsoleColor.Gray, ConsoleColor.Black);
                    break;
                default:
                    consoleColors = new ConsoleColors(ConsoleColor.DarkGreen, ConsoleColor.Black);
                    break;
            }

            Console.BackgroundColor = consoleColors.Background;
            Console.ForegroundColor = consoleColors.Foreground;
            Console.WriteLine("===================================================================================");
            var jsonFormatted = JToken.Parse(state.ToJson()).ToString(Formatting.Indented);
            Console.WriteLine(jsonFormatted);
            Console.BackgroundColor = ConsoleColor.Black;
            Console.ForegroundColor = ConsoleColor.DarkGreen;
            Console.Write("");
        }
        private readonly struct ConsoleColors
        {
            public ConsoleColors(ConsoleColor foreground, ConsoleColor background)
            {
                Foreground = foreground;
                Background = background;
            }

            public ConsoleColor Foreground { get; }

            public ConsoleColor Background { get; }
        }
    }
}
