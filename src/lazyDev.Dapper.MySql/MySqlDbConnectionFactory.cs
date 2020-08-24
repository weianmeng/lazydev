using MySqlConnector;
using System.Data.Common;
using Microsoft.Extensions.Options;

namespace lazyDev.Dapper.MySql
{
    public class MySqlDbConnectionFactory : BaseDbConnectionFactory
    {
        public override DbConnection GetDbConnection(string conn)
        {
            return new MySqlConnection(conn);
        }

        public MySqlDbConnectionFactory(IOptions<ConnectionOption> options)
            : base(options)
        {
        }
    }
}
