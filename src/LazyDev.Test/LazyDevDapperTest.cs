using Dapper;
using lazyDev.Dapper;
using Microsoft.Extensions.DependencyInjection;
using Npgsql;
using System.Threading.Tasks;
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

            var insertSql = "INSERT INTO student (name, age) VALUES (@Name, @Age)";
            proxy.AddCommand((conn,tran)=>conn.ExecuteAsync(insertSql,
                new Student
                {
                  Age = 18,
                  Name = "Ð¡ºì"
                },tran));

            var commandCount = await proxy.CommitAsync();
            Assert.Equal(1, commandCount);

            var name = await proxy.QueryFirstOrDefaultAsync<string>("select name from student");
            Assert.Equal("Ð¡ºì",name);

            proxy.AddCommand((conn,tran)=>conn.ExecuteAsync("delete from student"));
            await proxy.CommitAsync();

        }
    }


    public class Student
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Age { get; set; }
    }
}
