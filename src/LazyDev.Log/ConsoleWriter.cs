using LazyDev.Utilities.Extensions;
using System;
using System.Threading.Tasks;

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
