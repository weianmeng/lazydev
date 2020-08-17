using Dapper;
using lazyDev.Dapper;
using Microsoft.Extensions.DependencyInjection;
using System.Threading.Tasks;
using lazyDev.Dapper.MySql;
using Xunit;

namespace LazyDev.Test
{
    public class UnitTest1
    {
        [Fact]
        public async Task Test1Async()
        {
            var services = new ServiceCollection();
            services.AddDapper(x =>
            {
                x.SetDbFactory<MySqlDbConnectionFactory>();
                x.MasterConn = "";
                x.ReplicasConn = new[]
                {
                    "",
                    ""
                };
            });

            var provider = services.BuildServiceProvider();

            var db = provider.GetService<IDbContext>();
            db.AddCommand((conn, tran) => conn.ExecuteAsync("", null, tran));

            await db.QueryAsync(x => x.QuerySingleAsync<string>("",null));
            db.Query(x => x.Query<string>(""));
            using (var multi = await db.QueryAsync(x => x.QueryMultipleAsync("")))
            {
                
            }

            await db.CommitAsync();

        }
    }
}
