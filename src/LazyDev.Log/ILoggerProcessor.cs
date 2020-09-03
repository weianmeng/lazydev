using System;

namespace LazyDev.Log
{
    public interface ILoggerProcessor:IDisposable
    {
        void Enqueue(LogMessage message);
    }
}