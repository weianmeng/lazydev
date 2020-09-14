using System.Data;
using System.Threading.Tasks;
using Npgsql;
using Dapper;
using LazyDev.Utilities.Extensions;

namespace LazyDev.Log.Postgres
{
    public class PostgresLogWriter: ILogWriter
    {
        private string conStr = "Host=127.0.0.1;Database=lazy_db;Username=postgres;Password=123456";

        public async Task Write(LogMessage logMessage)
        {
            using (var conn = GetDbConnection())
            {
                await conn.ExecuteAsync(
                    $"insert into log_message(message) values ('{logMessage.ToJson()}')");
            }
        }

        public void Flush()
        {

        }

        private IDbConnection GetDbConnection()
        {
            return  new NpgsqlConnection(conStr);
        }
    }
}
