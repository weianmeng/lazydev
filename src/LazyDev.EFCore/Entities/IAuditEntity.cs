﻿using System;

namespace LazyDev.EFCore.Entities
{
    public interface IAuditEntity:IEntity
    {
        bool SoftDeleted { get; set; }
        int UpdatedBy { get; set; }
        DateTime UpdatedTime { get; set; }
        int CreatedBy { get; set; }
        DateTime CreatedTime { get; set; }
    }
}