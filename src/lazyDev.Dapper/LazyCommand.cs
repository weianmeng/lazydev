using System.Collections.Generic;
using Microsoft.Extensions.Logging;
using System.Data;
using System.Data.Common;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using LazyDev.Utilities.Extensions;

namespace lazyDev.Dapper
{
    public class LazyCommand:DbCommand
    {
        private readonly DbCommand _dbCommand;
        private readonly ILogger _logger;

        public LazyCommand(DbCommand dbCommand,ILogger logger)
        {
            _dbCommand = dbCommand;
            _logger = logger;
        }
        public override void Cancel()
        {
            _dbCommand.Cancel();
        }

        public override int ExecuteNonQuery()
        {
            var sw = Stopwatch.StartNew();
            try
            {
                return _dbCommand.ExecuteNonQuery();
            }
            finally
            {
                _logger.LogInformation($"执行sql：{_dbCommand.CommandText},参数:{GetParameters()?.ToJson()},耗时：{sw.ElapsedMilliseconds}");
            }
           
        }
        

        public override async Task<int> ExecuteNonQueryAsync(CancellationToken cancellationToken)
        {
            var sw = Stopwatch.StartNew();
            try
            {
                return await _dbCommand.ExecuteNonQueryAsync(cancellationToken);
            }
            finally
            {
                _logger.LogInformation($"执行sql：{_dbCommand.CommandText},参数:{GetParameters()?.ToJson()},耗时：{sw.ElapsedMilliseconds}");
            }

           
        }

        public override object ExecuteScalar()
        {
            var sw = Stopwatch.StartNew();
            try
            {
                return _dbCommand.ExecuteScalar();
            }
            finally
            {
                _logger.LogInformation($"执行sql：{_dbCommand.CommandText},参数:{GetParameters()?.ToJson()},耗时：{sw.ElapsedMilliseconds}");
            }
           
        }

        public override async Task<object> ExecuteScalarAsync(CancellationToken cancellationToken)
        {
            var sw = Stopwatch.StartNew();
            try
            {
                return await _dbCommand.ExecuteScalarAsync(cancellationToken);
            }
            finally
            {
                _logger.LogInformation($"执行sql：{_dbCommand.CommandText},参数:{GetParameters()?.ToJson()},耗时：{sw.ElapsedMilliseconds}");
            }
    
        }

        public override void Prepare()
        {
             _dbCommand.Prepare();
        }

        public override string CommandText { get=>_dbCommand.CommandText; set=>_dbCommand.CommandText=value; }
        public override int CommandTimeout { get=>_dbCommand.CommandTimeout; set=>_dbCommand.CommandTimeout=value; }
        public override CommandType CommandType { get=>_dbCommand.CommandType; set=>_dbCommand.CommandType=value; }
        public override UpdateRowSource UpdatedRowSource { get=>_dbCommand.UpdatedRowSource; set=>_dbCommand.UpdatedRowSource=value; }
        protected override DbConnection DbConnection { get=>_dbCommand.Connection; set=>_dbCommand.Connection=value; }
        protected override DbParameterCollection DbParameterCollection => _dbCommand.Parameters;
        protected override DbTransaction DbTransaction { get=>_dbCommand.Transaction; set=>_dbCommand.Transaction=value; }
        public override bool DesignTimeVisible { get=>_dbCommand.DesignTimeVisible; set=>_dbCommand.DesignTimeVisible=value; }

        protected override DbParameter CreateDbParameter()
        {
            return _dbCommand.CreateParameter();
        }

        protected override DbDataReader ExecuteDbDataReader(CommandBehavior behavior)
        {
            var sw = Stopwatch.StartNew();
            try
            {
                return _dbCommand.ExecuteReader(behavior);
            }
            finally
            {
                _logger.LogInformation($"执行sql：{_dbCommand.CommandText},参数:{GetParameters()?.ToJson()},耗时：{sw.ElapsedMilliseconds}");
            }
           
        }

        protected override async Task<DbDataReader> ExecuteDbDataReaderAsync(CommandBehavior behavior, CancellationToken cancellationToken)
        {
            var sw = Stopwatch.StartNew();
            try
            {
                return await _dbCommand.ExecuteReaderAsync(behavior, cancellationToken);
            }
            finally
            {
                _logger.LogInformation($"执行sql：{_dbCommand.CommandText},参数:{GetParameters()?.ToJson()},耗时：{sw.ElapsedMilliseconds}");
            }
           
        }
        private Dictionary<string, object> GetParameters()
        {
            var parameters = _dbCommand.Parameters.Cast<DbParameter>().ToList();

            return parameters.Any() 
                ? parameters.ToDictionary(k => k.ParameterName, v => v.Value)
                : null;
        }
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
              _dbCommand?.Dispose();  
            }
            base.Dispose(disposing);
        }
    }
}
