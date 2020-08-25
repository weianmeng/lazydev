using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Logging;
using System;
using System.Data.Common;

namespace lazyDev.Dapper
{
    public static class ServiceCollectionExtension
    {
        public static IServiceCollection AddDapper(this IServiceCollection services, Action<DbBuilderOption> dbAction)
        {

            var dbConfig = new DbBuilderOption();
            dbAction(dbConfig);

            services.TryAddSingleton<IDbConnectionFactory>(c =>
                new DbConnectionFactory(
                    new ConnectionOption()
                    {
                        MasterConn = dbConfig.MasterConn,
                        ReplicasConn = dbConfig.ReplicasConn
                    },
                    dbConfig.ConnectionFunc,
                    c.GetService<ILogger<IDbConnectionFactory>>()));

            services.AddScoped<IDapperProxy, DapperProxy>();
            return services;
        }
        public class DbBuilderOption
        {
            public Func<string, DbConnection> ConnectionFunc { get; set; }
            public string MasterConn { get; set; }
            public string[] ReplicasConn { get; set; }
        }
    }
}
