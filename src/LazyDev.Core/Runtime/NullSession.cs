namespace LazyDev.Core.Runtime
{
    public class NullSession:ILazyDevSession
    {
        public int UId { get; } = 0;
        public int TenantId { get; } = 0;
    }


}
