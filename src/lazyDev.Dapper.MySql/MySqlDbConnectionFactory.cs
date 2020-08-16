using Microsoft.Extensions.Options;
using MySqlConnector;
using System.Data.Common;

namespace lazyDev.Dapper.MySql
{
    public class MySqlDbConnectionFactory : DbConnectionFactory
    {
        public MySqlDbConnectionFactory(IOptions<DbConnectionOptions> options) 
            : base(options)
        {
        }

        public override DbConnection GetDbConnection(string conn)
        {
            return new MySqlConnection(conn);
        }
    }
}
