using LazyDev.Utilities.Extensions;
using System;

namespace LazyDev.Log
{
    public class ConsoleWriter:ILogWriter
    {
        public void Write(LogMessage logMessage)
        {
            Console.Write(logMessage.ToJson());
        }
    }
}
