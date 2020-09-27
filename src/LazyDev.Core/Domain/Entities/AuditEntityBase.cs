using System;

namespace LazyDev.Core.Domain.Entities
{
    public class AuditEntityBase:EntityBase
    {
        public bool SoftDeleted { get; set; }
        public int? UpdatedBy { get; set; }
        public DateTime? UpdatedTime { get; set; }
        public int CreatedBy { get; set; }
        public DateTime CreatedTime { get; set; }
    }
}
