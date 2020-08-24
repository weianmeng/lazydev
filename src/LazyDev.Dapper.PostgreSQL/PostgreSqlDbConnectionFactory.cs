using lazyDev.Dapper;
using Npgsql;
using System.Data.Common;
using Microsoft.Extensions.Options;

namespace LazyDev.Dapper.PostgreSql
{
    public class PostgreSqlDbConnectionFactory:BaseDbConnectionFactory
    {
        public override DbConnection GetDbConnection(string conn)
        {
           return new NpgsqlConnection(conn);
        }

        public PostgreSqlDbConnectionFactory(IOptions<ConnectionOption> options) 
            : base(options)
        {
        }
    }
}
