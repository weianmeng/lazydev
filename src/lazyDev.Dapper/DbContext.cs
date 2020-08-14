using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;

namespace lazyDev.Dapper
{
    public class DbContext:IDbContext
    {
        private readonly List<Func<IDbConnection, IDbTransaction, Task>> _commands;

        private readonly ILogger<DbContext> _logger;

        public DbContext(ILogger<DbContext> logger)
        {
            _commands = new List<Func<IDbConnection, IDbTransaction, Task>>();
            _logger = logger;
        }

        public void AddCommand(Func<IDbConnection, IDbTransaction, Task> func)
        {

            _commands.Add(func);
        }

        public async Task<bool> CommitAsync()
        {
            try
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
            catch (Exception e)
            {
                _logger.LogError(e,"事务提交失败");
                throw;
            }

        }

        public IDbConnection GetConnection(bool isMaster = true)
        {
           
            throw new NotImplementedException();
        }
    }
}
