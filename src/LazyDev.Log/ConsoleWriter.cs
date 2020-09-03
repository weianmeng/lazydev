using System;
using LazyDev.Utilities.Extensions;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace LazyDev.Log
{
    public class ConsoleWriter
    {
        /// <summary>
        /// 控制台日志输出
        /// </summary>
        /// <param name="logLevel"></param>
        /// <param name="message"></param>
        public void Writer(LogLevel logLevel, LogMessage message)
        {
            var consoleColors = logLevel switch
            {
                LogLevel.Critical => new ConsoleColors(ConsoleColor.White, ConsoleColor.Red),
                LogLevel.Error => new ConsoleColors(ConsoleColor.Black, ConsoleColor.Red),
                LogLevel.Warning => new ConsoleColors(ConsoleColor.Yellow, ConsoleColor.Black),
                LogLevel.Information => new ConsoleColors(ConsoleColor.DarkGreen, ConsoleColor.Black),
                LogLevel.Debug => new ConsoleColors(ConsoleColor.Gray, ConsoleColor.Black),
                LogLevel.Trace => new ConsoleColors(ConsoleColor.Gray, ConsoleColor.Black),
                _ => new ConsoleColors(ConsoleColor.DarkGreen, ConsoleColor.Black)
            };

            Console.BackgroundColor = consoleColors.Background;
            Console.ForegroundColor = consoleColors.Foreground;
            Console.WriteLine("===================================================================================");
            var jsonFormatted = JToken.Parse(message.ToJson()).ToString(Formatting.Indented);
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