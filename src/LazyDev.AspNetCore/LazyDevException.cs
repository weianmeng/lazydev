using System;

namespace LazyDev.AspNetCore
{

    /// <summary>
    /// 业务异常
    /// </summary>
    public class LazyDevException : Exception
    {
        public int Code { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="message"></param>
        /// <param name="code"></param>
        public LazyDevException(string message, int code = 400)
            : base(message)
        {
            Code = code;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="message"></param>
        /// <param name="code"></param>
        /// <param name="innerException"></param>
        public LazyDevException(string message, Exception innerException, int code = 400)
            : base(message, innerException)
        {
            Code = code;
        }
    }
}
