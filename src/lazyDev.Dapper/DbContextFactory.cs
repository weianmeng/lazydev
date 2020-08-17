using Microsoft.Extensions.DependencyInjection;
using System;

namespace lazyDev.Dapper
{
    public class DbContextFactory : IDbContextFactory
    {
        private readonly IDbConnectionFactory dbConnectionFactory;
        private readonly IServiceProvider serviceProvider;
       
        public DbContextFactory(IDbConnectionFactory dbConnectionFactory,IServiceProvider serviceProvider)
        {
            this.dbConnectionFactory = dbConnectionFactory;
            this.serviceProvider = serviceProvider;
        }
        public IDbContext CreateDbContext(string dbName)
        {
            var dbContext = serviceProvider.GetService<IDbContext>();
            
            dbContext.SetDbConnection(isMaster => dbConnectionFactory.GetLazyDbConnection(dbName, isMaster));
            return dbContext; 
        }
    }
}
