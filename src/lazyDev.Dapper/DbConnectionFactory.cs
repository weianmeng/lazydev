using Microsoft.Extensions.Options;
using System;
using System.Data.Common;
using System.Linq;

namespace lazyDev.Dapper
{
    public abstract class DbConnectionFactory : IDbConnectionFactory
    {
        private readonly IOptions<DbConnectionOptions> _options;
        private static ushort _dbIndex;

        protected DbConnectionFactory(IOptions<DbConnectionOptions> options)
        {
            this._options = options;
        }

        public DbConnection GetLazyDbConnection(string dbName, bool master = true)
        {

            var dbConfig = _options.Value.DbConnectionConfigs.FirstOrDefault(x => x.DbName == dbName);

            if (dbConfig == null)
            {
                throw new ArgumentException($"数据库{nameof(dbName)}连接没有配置");
            }
            if (master)
            {
                return new LayDevDbConnection(GetDbConnection(dbConfig.MasterConn));
            }
            //从库轮询
            _dbIndex++;
            var index = _dbIndex % dbConfig.ReplicaConns.Count();
            return new LayDevDbConnection(GetDbConnection(dbConfig.ReplicaConns[index]));
        }

        public abstract DbConnection GetDbConnection(string conn);
    }
}
