using MySqlConnector;
using System.Data.Common;

namespace lazyDev.Dapper.MySql
{
    public class MySqlDbConnectionFactory : BaseDbConnectionFactory
    {
        public MySqlDbConnectionFactory(IConnectionOption options) : base(options)
        {
        }

        public override DbConnection GetDbConnection(string conn)
        {
            return new MySqlConnection(conn);
        }
    }
}
