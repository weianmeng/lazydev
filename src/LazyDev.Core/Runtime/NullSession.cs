namespace LazyDev.Core.Runtime
{
    public class NullSession<TKey>:ILazyDevSession<TKey>
    {
        public TKey UId { get; } = default;
        public TKey TenantId { get; } = default;
    }

    public class NullSession : NullSession<int>, ILazyDevSession
    {

    }
}
