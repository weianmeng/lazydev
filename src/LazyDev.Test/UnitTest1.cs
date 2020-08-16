using Dapper;
using lazyDev.Dapper;
using Microsoft.Extensions.DependencyInjection;
using System.Net.Http;
using Xunit;

namespace LazyDev.Test
{
    public class UnitTest1
    {
        [Fact]
        public void Test1()
        {
            var services = new ServiceCollection();
            var provider = services.BuildServiceProvider();
            var dbFactory =  provider.GetService<IDbContextFactory>();
            var db = dbFactory.CreateDbContext("laydevdb");
            db.AddCommand((conn,tran)=> conn.ExecuteAsync("",null,tran) );



        }
    }
}
