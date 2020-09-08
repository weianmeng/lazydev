using LazyDev.Utilities.Extensions;
using System;

namespace LazyDev.Log
{
    public class ConsoleWriter:ILogWriter
    {
        public void Write(LogMessage logMessage)
        {
            
            Console.WriteLine(logMessage.ToJson());
        }

        public void Flush()
        {
           
        }
    }
}
