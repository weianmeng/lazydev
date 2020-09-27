namespace LazyDev.Core.Exception
{

    /// <summary>
    /// 友好异常
    /// </summary>
    public class LazyDevException : System.Exception
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
        public LazyDevException(string message, System.Exception innerException, int code = 400)
            : base(message, innerException)
        {
            Code = code;
        }
    }
}
