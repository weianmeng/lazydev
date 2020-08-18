using System.Data;
using System.Data.Common;
using Microsoft.Extensions.Logging;

namespace lazyDev.Dapper
{
    public class LazyDevDbCommand:DbCommand
    {
        private readonly DbCommand _command;
        private readonly ILogger _logger;

        public LazyDevDbCommand(DbCommand command, ILogger logger)
        {
            _command = command;
            _logger = logger;
        }
        public override void Cancel()
        {
            _command.Cancel();
        }

        public override int ExecuteNonQuery()
        {
            _logger.LogInformation(_command.CommandText);
           return _command.ExecuteNonQuery();
        }

        public override object ExecuteScalar()
        {
            _logger.LogInformation(_command.CommandText);
            return _command.ExecuteScalar();
        }

        public override void Prepare()
        {
            _logger.LogInformation(_command.CommandText);
            _command.Prepare();
        }

        public override string CommandText
        {
            get => _command.CommandText;
            set => _command.CommandText = value;
        }

        public override int CommandTimeout
        {
            get => _command.CommandTimeout;
            set => _command.CommandTimeout = value;
        }
        public override CommandType CommandType
        {
            get => _command.CommandType;
            set => _command.CommandType = value;
        }
        public override UpdateRowSource UpdatedRowSource
        {
            get => _command.UpdatedRowSource;
            set => _command.UpdatedRowSource = value;
        }
        protected override DbConnection DbConnection
        {
            get => _command.Connection;
            set => _command.Connection = value;
        }
        protected override DbParameterCollection DbParameterCollection => _command.Parameters;
        protected override DbTransaction DbTransaction
        {
            get => _command.Transaction;
            set => _command.Transaction = value;
        }
        public override bool DesignTimeVisible
        {
            get => _command.DesignTimeVisible;
            set => _command.DesignTimeVisible = value;
        }

        protected override DbParameter CreateDbParameter()
        {
            return _command.CreateParameter();
        }

        protected override DbDataReader ExecuteDbDataReader(CommandBehavior behavior)
        {
            _logger.LogInformation(_command.CommandText);

            foreach (DbParameter parameter in _command.Parameters)
            {
                _logger.LogInformation(parameter.ParameterName);
               
            }
            return _command.ExecuteReader(behavior);
        }
    }
}
