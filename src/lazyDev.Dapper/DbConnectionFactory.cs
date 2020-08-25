using Microsoft.Extensions.Logging;
using System;
using System.Data.Common;

namespace lazyDev.Dapper
{
    public  class DbConnectionFactory : IDbConnectionFactory
    {
        private readonly ConnectionOption _option;
        private readonly Func<string, DbConnection> _func;
        private readonly ILogger _logger;
        private static ushort _dbIndex;

        public DbConnectionFactory(ConnectionOption option, Func<string,DbConnection> func,ILogger logger)
        {
            _option = option;
            _func = func;
            _logger = logger;
        }

        public DbConnection GetDbConnection(bool master = false)
        {
            if (master)
            {
                return new LazyConnection(_func(_option.MasterConn), _logger);
            }

            //没有从库
            if (_option.ReplicasConn==null || _option.ReplicasConn.Length == 0)
            {

                return new LazyConnection(_func(_option.MasterConn), _logger);
            }

            //从库轮询
            _dbIndex++;
            var index = _dbIndex % _option.ReplicasConn.Length;
            return  new LazyConnection(_func(_option.ReplicasConn[index]),_logger) ;
        }
    }
}
