using System;

namespace LazyDev.EFCore.Entities
{
    public abstract class AuditEntityBase:EntityBase, IAuditEntity
    {
        public  bool SoftDeleted { get; set; }
        public  int UpdatedBy { get; set; }
        public  DateTime UpdatedTime { get; set; }
        public  int CreatedBy { get; set; }
        public  DateTime CreatedTime { get; set; }
    }
}
