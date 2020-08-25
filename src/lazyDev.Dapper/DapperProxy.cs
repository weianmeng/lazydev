using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;

namespace lazyDev.Dapper
{
    public class DapperProxy : IDapperProxy
    {
        private readonly IDbConnectionFactory _dbConnectionFactory;
        private readonly ILogger<DapperProxy> _logger;
        private readonly List<Func<IDbConnection, IDbTransaction, Task>> _commands;
        public DapperProxy(IDbConnectionFactory dbConnectionFactory,ILogger<DapperProxy> logger)
        {
            _dbConnectionFactory = dbConnectionFactory;
            _logger = logger;
            _commands = new List<Func<IDbConnection, IDbTransaction, Task>>();
        }

        public IDbConnection ReadConnection => _dbConnectionFactory.GetDbConnection(false);
        public IDbConnection WriteReadConnection => _dbConnectionFactory.GetDbConnection();

        #region Query
        private async Task<T> ProxyQueryAsync<T>(Func<IDbConnection, Task<T>> func)
        {
            using (var conn = _dbConnectionFactory.GetDbConnection(false))
            {
                
                return await func(conn);
            }
        }
        public async Task<IEnumerable<T>> QueryAsync<T>(string sql, object param = null)
        {
            return await ProxyQueryAsync(conn => conn.QueryAsync<T>(sql, param));
        }

        public async Task<T> QueryFirstAsync<T>(string sql, object param = null)
        {
            return await ProxyQueryAsync(conn => conn.QueryFirstAsync<T>(sql, param));
        }
        public async Task<T> QueryFirstOrDefaultAsync<T>(string sql, object param = null)
        {
            return await ProxyQueryAsync(conn => conn.QueryFirstOrDefaultAsync<T>(sql, param));
        }
        public async Task<T> QuerySingleAsync<T>(string sql, object param = null)
        {
            return await ProxyQueryAsync(conn => conn.QuerySingleAsync<T>(sql, param));
        }
        public async Task<T> QuerySingleOrDefaultAsync<T>(string sql, object param = null)
        {
            return await ProxyQueryAsync(conn => conn.QuerySingleOrDefaultAsync<T>(sql, param));
        }
        public async Task<SqlMapper.GridReader> QueryMultipleAsync(string sql, object param = null)
        {
            return await ProxyQueryAsync(conn => conn.QueryMultipleAsync(sql, param));
        }
        #endregion

        #region Execute

        private async Task<T> ProxyExecuteAsync<T>(Func<IDbConnection, Task<T>> func)
        {
            using (var conn = _dbConnectionFactory.GetDbConnection())
            {
              
                return await func(conn);
            }
        }

        public async Task<int> ExecuteAsync(string sql, object param = null)
        {
            return await ProxyExecuteAsync(conn => conn.ExecuteAsync(sql, param));
        }

        public async Task<T> ExecuteScalarAsync<T>(string sql, object param = null)
        {
            return await ProxyQueryAsync(conn => conn.ExecuteScalarAsync<T>(sql, param));
        }
        public async Task<IDataReader> ExecuteReaderAsync(string sql, object param = null)
        {
            return await ProxyQueryAsync(conn => conn.ExecuteReaderAsync(sql, param));
        }

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
