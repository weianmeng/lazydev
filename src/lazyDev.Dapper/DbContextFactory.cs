using Microsoft.Extensions.DependencyInjection;
using System;

namespace lazyDev.Dapper
{
    public class DbContextFactory:IDbContextFactory
    {
        private readonly IServiceProvider _serviceProvider;

        public DbContextFactory(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }
        public IDbContext CreateDbContext(string configSectionName)
        {
            return _serviceProvider.GetService<IDbContext>();
        }
    }
}
