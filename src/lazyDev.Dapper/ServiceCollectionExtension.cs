using System;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace lazyDev.Dapper
{
    public static class ServiceCollectionExtension
    {
        public static IServiceCollection AddDapper(this IServiceCollection services,Action<DbContextBuilder> builder)
        {
            var dbContextBuilder = new DbContextBuilder();
            builder(dbContextBuilder);
            //注册连接字符串
            services.TryAddSingleton<IConnectionOption>(new ConnectionOption
            {
                MasterConn = dbContextBuilder.MasterConn,
                ReplicasConn = dbContextBuilder.ReplicasConn
            });
            //注册连接对象构造工厂
            services.TryAddSingleton(typeof(IDbConnectionFactory), dbContextBuilder.DbConnectionFactory);
            //注册数据库操作对象
            services.TryAddScoped<IDbContext,DbContext>();
            return services;
        }
    }
}
