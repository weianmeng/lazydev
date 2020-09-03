using Microsoft.Extensions.Logging;
using System;
using LazyDev.Utilities;

namespace LazyDev.Log
{
    public class LazyLogger:ILogger
    {
        private readonly string _appId;
        private readonly string _hostIp;
        private readonly string _name;
        public  bool Console { get; internal set; }

        public Func<string, LogLevel, bool> Filter { get; internal set; }
        private readonly ConsoleWriter _consoleWriter;

        public LazyLogger(string appId,string name,bool console,  Func<string, LogLevel, bool> filter)
        {

            _appId = appId;
            _name = name;
            Console = console;
            _hostIp = NetUtility.GetHostIp();
            Filter = filter ?? ((category, logLevel) => true);
            _consoleWriter = new ConsoleWriter();
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

            if (state is LogMessage baseMessage)
            {}
            else
            {
                baseMessage = new LogMessage()
                {
                  Message = state.ToString()
                };
            }
            //日志格式化
            FormatLog(logLevel, eventId, exception, baseMessage);

            if (Console)
            {
                _consoleWriter.Writer(logLevel, baseMessage);
            }
           
        }

        /// <summary>
        ///  格式化日志
        /// </summary>
        /// <param name="logLevel"></param>
        /// <param name="eventId"></param>
        /// <param name="exception"></param>
        /// <param name="baseMessage"></param>
        private void FormatLog(LogLevel logLevel, EventId eventId, Exception exception, LogMessage baseMessage)
        {
            baseMessage.AppId = _appId;
            baseMessage.LogName = _name;
            baseMessage.HostIp = _hostIp;
            baseMessage.LogLevel = GetLogLevelString(logLevel);
            baseMessage.EventId = eventId.Id;
            baseMessage.LogType = eventId.ToString();
            baseMessage.Exception = exception?.ToString();
            baseMessage.LogTime = DateTime.Now;
        }


        public bool IsEnabled(LogLevel logLevel)
        {
            return logLevel != LogLevel.None && Filter(_name,logLevel);
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
                LogLevel.Information => "info",
                LogLevel.Warning => "warn",
                LogLevel.Error => "fail",
                LogLevel.Critical => "cri",
                LogLevel.None => "none",
                _ => throw new ArgumentOutOfRangeException(nameof(logLevel))
            };
        }
    }
}
