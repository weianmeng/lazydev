using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;

namespace lazyDev.Dapper
{
    public class DbContext:IDbContext
    {
        private readonly List<Func<IDbConnection, IDbTransaction, Task>> _commands;


        private Func<bool, IDbConnection> _dbConnectionFunc;

        public DbContext()
        {
           
            _commands = new List<Func<IDbConnection, IDbTransaction, Task>>();
        }

        public void SetDbConnection(Func<bool, IDbConnection> dbConnectionFunc)
        {
            if (_dbConnectionFunc == null)
            {
                _dbConnectionFunc = dbConnectionFunc;
            } 
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
            return _dbConnectionFunc(isMaster);
        }
    }
}
