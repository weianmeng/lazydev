using Microsoft.Extensions.Logging;
using System.Collections.Generic;

namespace LazyDev.Log
{
    public class LazyDevLoggerOptions
    {
        /// <summary>
        /// 应用程序ID
        /// </summary>
        public string AppId { get; set; }
        /// <summary>
        /// 是否控制台打印
        /// </summary>
        public bool Console { get; set; } = false;
        /// <summary>
        /// 本地缓冲队列值
        /// </summary>
        public int MaxQueuedMessageCount { get; set; } = 10240;
        /// <summary>
        /// 本地日志目录
        /// </summary>
        public string LocalPath { get; set; } = "/var/logs/";
        /// <summary>
        /// 日志级别
        /// </summary>
        public IDictionary<string, LogLevel> LogLevel { get; set; } = new Dictionary<string, LogLevel>();

    }
}
