namespace LazyDev.EFCore.Entities
{
    public interface ITenantEntity<T>
    {
         T TenantId { get; set; }
    }
}
