using System.Threading.Tasks;

namespace LazyDev.Log
{
    public interface ILogWriter
    {
        Task Write(LogMessage logMessage);
        void Flush();
    }
}
