namespace LazyDev.Log
{
    public interface ILogWriter
    {
        void Write(LogMessage logMessage);
        void Flush();
    }
}
