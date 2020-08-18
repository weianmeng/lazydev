
using Dapper;
using lazyDev.Dapper;
using lazyDev.Dapper.MySql;
using Microsoft.Extensions.DependencyInjection;
using System.Threading.Tasks;
using Dapper.Contrib.Extensions;
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
            db.AddCommand((conn, tran) => conn.InsertAsync(new {x=3}));

            await db.QueryAsync(x => x.QuerySingleAsync<string>("",null));
            db.Query(x => x.Query<string>(""));
            using (var multi = await db.QueryAsync(x => x.QueryMultipleAsync("")))
            {
                
            }


            await db.CommitAsync();

        }
    }
}
