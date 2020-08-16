using System.Data.Common;

namespace lazyDev.Dapper
{
    public interface IDbConnectionFactory
    {
     
        DbConnection GetLazyDbConnection(string dbName, bool master = true);
    }
}