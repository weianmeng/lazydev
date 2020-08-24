using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace lazyDev.Dapper
{
    public static class ServiceCollectionExtension
    {
        public static IServiceCollection AddDapper<T>(this IServiceCollection services, IConfigurationSection section)
            where T : class, IDbConnectionFactory
        {
            
            services.Configure<ConnectionOption>(section);

            //注册连接对象构造工厂
            services.TryAddSingleton(typeof(IDbConnectionFactory), typeof(T));
            //注册数据库操作对象
            services.TryAddScoped<IDapperProxy,DapperProxy>();
            return services;
        }
    }
}
