using LazyDev.EFCore.Entities;

namespace Rock.Core.Entities
{
    public class Entity:EntityBase,ITenant
    {
        public int TenantId { get; set; }
    }
}
