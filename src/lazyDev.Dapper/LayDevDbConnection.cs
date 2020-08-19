using System.Data;
using System.Data.Common;
using Microsoft.Extensions.Logging;

namespace lazyDev.Dapper
{
    internal class LayDevDbConnection : DbConnection
    {
        private readonly DbConnection _dbConnection;
        private readonly ILogger _logger;

        public LayDevDbConnection(DbConnection dbConnection,ILogger logger)
        {
            _dbConnection = dbConnection;
            _logger = logger;
        }
        public override string ConnectionString
        {
            get => _dbConnection.ConnectionString;
            set => _dbConnection.ConnectionString = value;
        }

        public override string Database => _dbConnection.Database;

        public override string DataSource => _dbConnection.DataSource;

        public override string ServerVersion => _dbConnection.ServerVersion;

        public override ConnectionState State => _dbConnection.State;

        public override void ChangeDatabase(string databaseName)
        {
            _dbConnection.ChangeDatabase(databaseName);
        }

        public override void Close()
        {
            _dbConnection.Close();
        }

        public override void Open()
        {
            _dbConnection.Open();
        }

        protected override DbTransaction BeginDbTransaction(IsolationLevel isolationLevel)
        {
            return _dbConnection.BeginTransaction(isolationLevel);
        }


        protected override DbCommand CreateDbCommand()
        {
            var cmd = _dbConnection.CreateCommand();
            
            return new LazyDevDbCommand(cmd,_logger);
        }
    }
}
