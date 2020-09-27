using System;

namespace LazyDev.Core.Domain.Entities
{
    public abstract class AuditEntityBase<T>:EntityBase<T>, IAuditEntity<T>
    {
        public  bool SoftDeleted { get; set; }
        public  int? UpdatedBy { get; set; }
        public  DateTime? UpdatedTime { get; set; }
        public  int CreatedBy { get; set; }
        public  DateTime CreatedTime { get; set; }
    }

    public abstract class AuditEntityBase : AuditEntityBase<int>
    {

    }
}
