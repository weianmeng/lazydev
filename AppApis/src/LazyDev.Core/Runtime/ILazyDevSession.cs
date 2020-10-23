namespace LazyDev.Core.Runtime
{

    public interface ILazyDevSession
    {
        public int UId { get; }
        public int TenantId { get; }
    }
}
