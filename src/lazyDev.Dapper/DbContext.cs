using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;

namespace lazyDev.Dapper
{
    public class DbContext : IDbContext
    {
        private readonly IDbConnectionFactory _dbConnectionFactory;
        private readonly ILogger<DbContext> _logger;

        private readonly List<Func<IDbConnection, IDbTransaction, Task>> _commands;

        public DbContext(IDbConnectionFactory dbConnectionFactory,ILogger<DbContext> logger)
        {
            _dbConnectionFactory = dbConnectionFactory;
            _logger = logger;


            _commands = new List<Func<IDbConnection, IDbTransaction, Task>>();
        }


        public void AddCommand(Func<IDbConnection, IDbTransaction, Task> func)
        {
            _commands.Add(func);
        }

        public async Task<bool> CommitAsync()
        {
            using (var conn = GetConnection())
            {
                
                if (conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                }
                using (var tran = conn.BeginTransaction())
                {
                    foreach (var fun in _commands)
                    {

                        await fun(conn, tran);
                    }

                    tran.Commit();
                }
            }

            var count = _commands.Count;
            _commands.Clear();
            return count > 0;
        }

        public IDbConnection GetConnection(bool isMaster = true)
        {
            return _dbConnectionFactory.GetDbConnection(isMaster);
        }

        #region Query

        public async Task<T> QueryAsync<T>(Func<IDbConnection, Task<T>> func)
        {
            using (var conn = GetConnection(false))
            {
                return await func(conn);
            }
        }

        public T Query<T>(Func<IDbConnection, T> func)
        {
            using (var conn = GetConnection(false))
            {
                return func(conn);
            }
        }

        #endregion
    }
}
