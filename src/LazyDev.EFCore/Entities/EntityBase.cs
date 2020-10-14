using System;

namespace LazyDev.EFCore.Entities
{
    public abstract class EntityBase:IEntity
    {
        public int Id { get; set; }
        public int UpdatedBy { get; set; }
        public DateTime UpdatedAt { get; set; }
        public int CreatedBy { get; set; }
        public DateTime CreatedAt { get; set; }
        public bool SoftDeleted { get; set; }
        public int TenantId { get; set; }
    }
}
