using System;

namespace LazyDev.Log
{
    public class LogMessage
    {
        /// <summary>
        /// 应用程序ID
        /// </summary>
        public string AppId { get; set; }
        /// <summary>
        /// 主机IP
        /// </summary>
        public string HostIp { get; set; }
        /// <summary>
        /// 链路ID
        /// </summary>
        public string ChainId { get; set; }
        /// <summary>
        /// 追踪ID
        /// </summary>
        public string TraceId { get; set; }
        /// <summary>
        /// 父级追踪ID
        /// </summary>
        public string ParentTraceId { get; set; }
        /// <summary>
        /// 日志类型
        /// </summary>
        public string LogType { get; set; }
        /// <summary>
        /// 日志级别
        /// </summary>
        public string LogLevel { get; set; }
        /// <summary>
        /// 事件Id
        /// </summary>
        public int EventId { get; set; }
        /// <summary>
        /// 日志名称
        /// </summary>
        public string LogName { get; set; }
        /// <summary>
        /// 错误信息
        /// </summary>
        public string Exception { get; set; }
        /// <summary>
        /// 日志
        /// </summary>
        public string Message { get; set; }
        /// <summary>
        /// 日志记录时间
        /// </summary>
        public DateTime LogTime { get; set; }

    }
}
