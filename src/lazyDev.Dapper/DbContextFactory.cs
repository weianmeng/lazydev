namespace lazyDev.Dapper
{
    public class DbContextFactory : IDbContextFactory
    {
        private readonly IDbConnectionFactory dbConnectionFactory;

        public DbContextFactory(IDbConnectionFactory dbConnectionFactory)
        {
            this.dbConnectionFactory = dbConnectionFactory;
        }
        public IDbContext CreateDbContext(string dbName)
        {
            return new DbContext(isMaster => dbConnectionFactory.GetLazyDbConnection(dbName, isMaster));
        }
    }
}
