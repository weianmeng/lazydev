using Microsoft.Extensions.DependencyInjection;
using System;

namespace lazyDev.Dapper
{
    public class DbContextFactory : IDbContextFactory
    {
        private readonly IDbConnectionFactory _dbConnectionFactory;
        private readonly IServiceProvider _serviceProvider;
       
        public DbContextFactory(IDbConnectionFactory dbConnectionFactory,IServiceProvider serviceProvider)
        {
            _dbConnectionFactory = dbConnectionFactory;
            _serviceProvider = serviceProvider;
        }
        public IDbContext CreateDbContext(string dbName)
        {
            var dbContext = _serviceProvider.GetService<IDbContext>();
            
            dbContext.SetDbConnection(isMaster => _dbConnectionFactory.GetLazyDbConnection(dbName, isMaster));
            return dbContext; 
        }
    }
}
