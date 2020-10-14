using System;

namespace LazyDev.EFCore.Entities
{
    public interface IEntity
    {
        int Id { get; set; }
        int UpdatedBy { get; set; }
        DateTime UpdatedAt { get; set; }
        int CreatedBy { get; set; }
        DateTime CreatedAt { get; set; }
        bool SoftDeleted { get; set; }
        int TenantId { get; set; }
    }
}