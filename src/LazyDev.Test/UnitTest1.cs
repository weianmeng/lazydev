using Dapper;
using lazyDev.Dapper;
using Microsoft.Extensions.DependencyInjection;
using System.Threading.Tasks;
using Xunit;

namespace LazyDev.Test
{
    public class UnitTest1
    {
        [Fact]
        public async Task Test1Async()
        {
            var services = new ServiceCollection();
            var provider = services.BuildServiceProvider();
            var dbFactory =  provider.GetService<IDbContextFactory>();
            var db = dbFactory.CreateDbContext("laydevdb");
            db.AddCommand((conn,tran)=> conn.ExecuteAsync("",new {name="уехЩ" },tran) );
            await db.CommitAsync();


        }
    }
}
