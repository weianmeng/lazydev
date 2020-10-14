using System;

namespace LazyDev.EFCore.Entities
{
    public abstract class EntityBase:IEntity
    {
        public int Id { get; set; }
        public int UpdatedBy { get; set; }
        public DateTime UpdatedTime { get; set; }
        public int CreatedBy { get; set; }
        public DateTime CreatedTime { get; set; }
        public bool SoftDeleted { get; set; }
        public int TenantId { get; set; }
    }
}
