using System;

namespace LazyDev.Core.Domain.Entities
{
    public interface IAuditEntity<T>:IEntity<T>
    {
        bool SoftDeleted { get; set; }
        int? UpdatedBy { get; set; }
        DateTime? UpdatedTime { get; set; }
        int CreatedBy { get; set; }
        DateTime CreatedTime { get; set; }
    }
}