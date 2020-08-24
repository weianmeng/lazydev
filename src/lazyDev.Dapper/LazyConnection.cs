using Microsoft.Extensions.Logging;
using System.Data;
using System.Data.Common;
using System.Threading;
using System.Threading.Tasks;

namespace lazyDev.Dapper
{
    public class LazyConnection:DbConnection
    {
        private readonly DbConnection _dbConnection;
        private readonly ILogger _logger;

        public LazyConnection(DbConnection dbConnection,ILogger logger)
        {
            _dbConnection = dbConnection;
            _logger = logger;
        }
        protected override DbTransaction BeginDbTransaction(IsolationLevel isolationLevel)
        {
            return _dbConnection.BeginTransaction(isolationLevel);
        }

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

        public override Task OpenAsync(CancellationToken cancellationToken)
        {
            return _dbConnection.OpenAsync(cancellationToken);
        }

        public override string ConnectionString { get=> _dbConnection.ConnectionString; set=> _dbConnection.ConnectionString=value; }
        public override string Database => _dbConnection.Database;
        public override ConnectionState State => _dbConnection.State;
        public override string DataSource => _dbConnection.DataSource;
        public override string ServerVersion => _dbConnection.ServerVersion;

        protected override DbCommand CreateDbCommand()
        {
            return new LazyCommand(_dbConnection.CreateCommand(), _logger);
        }

        
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                 _dbConnection?.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
