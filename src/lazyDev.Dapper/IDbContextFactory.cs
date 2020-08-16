namespace lazyDev.Dapper
{
    public interface IDbContextFactory
    {
        IDbContext CreateDbContext(string dbName);
    }
}
