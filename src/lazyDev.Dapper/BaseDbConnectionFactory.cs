using System.Data.Common;
using Microsoft.Extensions.Options;

namespace lazyDev.Dapper
{
    public abstract class BaseDbConnectionFactory : IDbConnectionFactory
    {
        private readonly IOptions<ConnectionOption> _options;
        private static ushort _dbIndex;

        protected BaseDbConnectionFactory(IOptions<ConnectionOption> options)
        {
            _options = options;
        }

        public DbConnection GetDbConnection(bool master = false)
        {
            //没有从库
            if (_options.Value.ReplicasConn.Length ==0)
            {
                return GetDbConnection(_options.Value.MasterConn);
            }

            if (master)
            {
                return GetDbConnection(_options.Value.MasterConn);
            }
            //从库轮询
            _dbIndex++;
            var index = _dbIndex % _options.Value.ReplicasConn.Length;
            return GetDbConnection(_options.Value.ReplicasConn[index]);
        }

        public abstract DbConnection GetDbConnection(string conn);
    }
}
