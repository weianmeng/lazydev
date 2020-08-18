using System.Data.Common;

namespace lazyDev.Dapper
{
    public interface IDbConnectionFactory
    {
     
        DbConnection GetDbConnection(bool master = true);
    }
}