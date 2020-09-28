using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;

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


        #region Query
        public async Task<T> QueryAsync<T>(Func<IDbConnection, Task<T>> func, bool readMaster = false)
        {
            using (var conn = _dbConnectionFactory.GetDbConnection(readMaster))
            {
                return await func(conn);
            }
        }
        #endregion

        #region Execute
        //事务
        public void AddCommand(Func<IDbConnection, IDbTransaction, Task> func)
        {
            _commands.Add(func);
        }
        //事务提交
        public async Task<int> CommitAsync()
        {
            try
            {
                using (var conn = _dbConnectionFactory.GetDbConnection())
                {
                    if (conn.State != ConnectionState.Open)
                    {
                        await conn.OpenAsync();
                    }

                    using (var tran = conn.BeginTransaction())
                    {
                        foreach (var func in _commands)
                        {
                            await func(conn, tran);
                        }
                        tran.Commit();
                    }
                    
                }

                return _commands.Count;
            }
            catch (Exception e)
            {
                _logger.LogError("事务提交失败",e);
                throw;
            }
            finally
            {
                _commands.Clear();
            }


        }
        #endregion



    }
}
