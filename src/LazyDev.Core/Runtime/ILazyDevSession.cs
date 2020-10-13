namespace LazyDev.Core.Runtime
{
    public interface ILazyDevSession<out TKey>
    {
        public TKey UId { get;}
        public TKey TenantId { get; }
    }

    public interface ILazyDevSession
    {
        public int UId { get; }
        public int TenantId { get; }
    }
}
