using System;
using System.Threading.Tasks;
using LazyDev.Core.Extensions;

namespace LazyDev.Log
{
    public class ConsoleWriter:ILogWriter
    {
        public Task Write(LogMessage logMessage)
        {
            
            Console.WriteLine(logMessage.ToJson());
            return Task.CompletedTask;
        }

        public void Flush()
        {
           
        }
    }
}
