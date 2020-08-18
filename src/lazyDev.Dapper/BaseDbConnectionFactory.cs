using System.Data.Common;

namespace lazyDev.Dapper
{
    public abstract class BaseDbConnectionFactory : IDbConnectionFactory
    {
        private readonly IConnectionOption _options;
        private static ushort _dbIndex;

        protected BaseDbConnectionFactory(IConnectionOption options)
        {
            _options = options;
        }

        public DbConnection GetDbConnection(bool master = false)
        {

            if (master)
            {
                return new LayDevDbConnection(GetDbConnection(_options.MasterConn));
            }
            //从库轮询
            _dbIndex++;
            var index = _dbIndex % _options.ReplicasConn.Length;
            return new LayDevDbConnection(GetDbConnection(_options.ReplicasConn[index]));
        }

        public abstract DbConnection GetDbConnection(string conn);
    }
}
