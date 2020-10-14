using System;
using System.Linq;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using LazyDev.Core.Runtime;
using LazyDev.EFCore.Entities;
using Microsoft.EntityFrameworkCore;

namespace LazyDev.EFCore
{
    public abstract class DbContextBase:DbContext
    {
        private readonly ILazyDevSession _lazyDevSession;

        protected DbContextBase(DbContextOptions options,ILazyDevSession lazyDevSession):base(options)
        {
            _lazyDevSession = lazyDevSession;
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

            //全局租户过滤
            foreach (var entityType in modelBuilder.Model.GetEntityTypes())
            {
                ConfigureGlobalFiltersMethodInfo.MakeGenericMethod(entityType.ClrType)
                    .Invoke(this, new object[] { modelBuilder });
            }
        }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
        {
            foreach (var entry in ChangeTracker.Entries().ToList())
            {
                var entity = (IEntity)entry.Entity;
                var time = DateTime.Now;
                switch (entry.State)
                {
                    case EntityState.Added:
                        entity.CreatedBy = _lazyDevSession.UId;
                        entity.CreatedAt = time;
                        entity.UpdatedBy = _lazyDevSession.UId;
                        entity.UpdatedAt = time;
                        entity.TenantId = _lazyDevSession.TenantId;
                        break;
                    case EntityState.Modified:
                        entity.UpdatedBy = _lazyDevSession.UId;
                        entity.UpdatedAt = time;
                        break;
                    case EntityState.Deleted:
                        entry.Reload();
                        entity.UpdatedBy = _lazyDevSession.UId;
                        entity.UpdatedAt = time;
                        entity.SoftDeleted = true;
                        entry.State = EntityState.Modified;
                        break;
                }
            }
            return base.SaveChangesAsync(cancellationToken);
        }

        #region 租户软删除全局过滤

        private static readonly MethodInfo ConfigureGlobalFiltersMethodInfo =
            typeof(DbContextBase).GetMethod(nameof(ConfigureGlobalFilter),
                BindingFlags.Instance | BindingFlags.NonPublic);
        protected virtual void ConfigureGlobalFilter<T>(ModelBuilder builder) where T : class, IEntity
        {
            builder.Entity<T>().Property(x => x.Id).HasColumnName("id");
            builder.Entity<T>().Property(x => x.UpdatedBy).HasColumnName("updated_by");
            builder.Entity<T>().Property(x => x.UpdatedAt).HasColumnName("updated_at");
            builder.Entity<T>().Property(x => x.CreatedBy).HasColumnName("created_by");
            builder.Entity<T>().Property(x => x.CreatedAt).HasColumnName("created_at");
            builder.Entity<T>().Property(x => x.SoftDeleted).HasColumnName("soft_deleted");
            builder.Entity<T>().Property(x => x.TenantId).HasColumnName("tenant_id");

            builder.Entity<T>().HasQueryFilter(x => x.TenantId == _lazyDevSession.TenantId && x.SoftDeleted == false);
        }

        #endregion

    }
}
