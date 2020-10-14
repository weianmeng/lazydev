using LazyDev.EFCore.Entities;

namespace Rock.Core.Entities
{
    public class Entity:EntityBase,ITenantEntity<int>
    {
        public int TenantId { get; set; }
    }
}
