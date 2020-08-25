using System;
using System.Threading.Tasks;
using lazyDev.Dapper;
using Microsoft.Extensions.DependencyInjection;
using Npgsql;
using Xunit;

namespace LazyDev.Test
{
    public class LazyDevDapperTest
    {


        [Fact]
        public async Task TestFactoryTestConnection()
        {
            var services = new ServiceCollection();
            services.AddLogging();
            services.AddDapper(x =>
            {
                x.ConnectionFunc = conn => new NpgsqlConnection(conn);
                x.MasterConn = @"PORT=5432;DATABASE=lazy_db;HOST=127.0.0.1;PASSWORD=123456;USER ID=postgres";
            });

            using var serviceScope = services.BuildServiceProvider().CreateScope();
            var serviceProvider = serviceScope.ServiceProvider;
            var connFactory = serviceProvider.GetService<IDbConnectionFactory>();
            Assert.NotNull(connFactory);
            var dbConnection = connFactory.GetDbConnection();
            Assert.NotNull(dbConnection);
            var proxy = serviceProvider.GetService<IDapperProxy>();
            var id = await proxy.QueryFirstOrDefaultAsync<string>("select id from student");
            Assert.NotEmpty(id);

        }
    }
}
