using System;
using System.Data;
using System.Threading.Tasks;
using Npgsql;
using Dapper;
using LazyDev.Utilities.Extensions;
using Microsoft.Extensions.Options;

namespace LazyDev.Log.Postgres
{
    public class PostgresLogWriter: ILogWriter
    {
        private readonly IOptions<PostgresLogOption> _options;
        public PostgresLogWriter(IOptions<PostgresLogOption> options)
        {
            _options = options;
        }
        public async Task Write(LogMessage logMessage)
        {
            using (var conn = GetDbConnection())
            {
                await conn.ExecuteAsync(
                    $"insert into app_logs(message,created_time) values ('{logMessage.ToJson()}','{logMessage.LogTime}')");
            }
        }

        public void Flush()
        {

        }

        private IDbConnection GetDbConnection()
        {
            return  new NpgsqlConnection(_options.Value.LogDb);
        }
    }
}
