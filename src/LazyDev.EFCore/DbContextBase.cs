using System.Reflection;
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
            foreach (var entityType in modelBuilder.Model.GetEntityTypes())
            {
                if (typeof(ITenant).IsAssignableFrom(entityType.ClrType))
                {
                    ConfigureGlobalFiltersMethodInfo.MakeGenericMethod(entityType.ClrType)
                        .Invoke(this, new object[] { modelBuilder });
                }
            }
        }

        #region 租户全局过滤

        private static readonly MethodInfo ConfigureGlobalFiltersMethodInfo =
            typeof(DbContextBase).GetMethod(nameof(ConfigureGlobalFilter),
                BindingFlags.Instance | BindingFlags.NonPublic);
        protected virtual void ConfigureGlobalFilter<T>(ModelBuilder builder) where T : class, ITenant
        {
            builder.Entity<T>().HasQueryFilter(x => x.TenantId == _lazyDevSession.TenantId);
        }

        #endregion

    }
}
