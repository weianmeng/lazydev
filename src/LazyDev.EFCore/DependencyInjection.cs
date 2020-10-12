using System;
using LazyDev.EFCore.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace LazyDev.EFCore
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddLazyDevDbContext<TDbContext>(this IServiceCollection services,Action<DbContextOptionsBuilder> action) where TDbContext : DbContext
        {
            services.AddScoped(typeof(IRepository<,>), typeof(RepositoryBase<,>));
            services.AddScoped(typeof(IRepository<>), typeof(RepositoryBase<>));
            services.AddDbContext<TDbContext>(action);
            services.AddScoped<DbContext, TDbContext>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            return services;
        }
    }
}
