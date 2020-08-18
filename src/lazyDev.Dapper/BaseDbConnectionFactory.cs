using System.Data.Common;
using Microsoft.Extensions.Logging;

namespace lazyDev.Dapper
{
    public abstract class BaseDbConnectionFactory : IDbConnectionFactory
    {
        private readonly IConnectionOption _options;
        private readonly ILogger _logger;
        private static ushort _dbIndex;

        protected BaseDbConnectionFactory(IConnectionOption options,ILogger logger)
        {
            _options = options;
            _logger = logger;
        }

        public DbConnection GetDbConnection(bool master = false)
        {
            //没有从库
            if (_options.ReplicasConn.Length ==0)
            {
                return new LayDevDbConnection(GetDbConnection(_options.MasterConn), _logger);
            }

            if (master)
            {
                return new LayDevDbConnection(GetDbConnection(_options.MasterConn), _logger);
            }
            //从库轮询
            _dbIndex++;
            var index = _dbIndex % _options.ReplicasConn.Length;
            return new LayDevDbConnection(GetDbConnection(_options.ReplicasConn[index]), _logger);
        }

        public abstract DbConnection GetDbConnection(string conn);
    }
}
