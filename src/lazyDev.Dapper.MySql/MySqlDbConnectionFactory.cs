using MySqlConnector;
using System.Data.Common;
using Microsoft.Extensions.Logging;

namespace lazyDev.Dapper.MySql
{
    public class MySqlDbConnectionFactory : BaseDbConnectionFactory
    {
 

        public override DbConnection GetDbConnection(string conn)
        {
            return new MySqlConnection(conn);
        }

        public MySqlDbConnectionFactory(IConnectionOption options, ILogger<MySqlDbConnectionFactory> logger) 
            : base(options, logger)
        {
        }
    }
}
