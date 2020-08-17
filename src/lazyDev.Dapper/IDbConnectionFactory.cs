using System.Data.Common;

namespace lazyDev.Dapper
{
    public interface IDbConnectionFactory
    {
     
        DbConnection GetLazyDbConnection(bool master = true);
    }
}