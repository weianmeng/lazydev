using System.Data;
using System.Data.Common;

namespace lazyDev.Dapper
{
    public class LayDevDbConnection : DbConnection
    {
        private readonly DbConnection _dbConnection;

        public LayDevDbConnection(DbConnection dbConnection)
        {
            _dbConnection = dbConnection;

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
            return cmd;
        }
    }
}
