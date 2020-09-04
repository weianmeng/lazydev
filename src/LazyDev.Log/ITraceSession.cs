namespace LazyDev.Log
{
    public interface ITraceSession
    {
        string ChainId { get; }
        string TraceId { get; }
        string ParentTraceId { get; }
    }
}