using System;
using Dapper;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Threading.Tasks;
using LazyDev.Utilities.Extensions;
using Microsoft.Extensions.Logging;

namespace lazyDev.Dapper
{
    public class DapperProxy : IDapperProxy
    {
        private readonly IDbConnectionFactory _dbConnectionFactory;
        private readonly ILogger<DapperProxy> _logger;

        private readonly List<Command> _commands;

        public DapperProxy(IDbConnectionFactory dbConnectionFactory,ILogger<DapperProxy> logger)
        {
            _dbConnectionFactory = dbConnectionFactory;
            _logger = logger;
            _commands = new List<Command>();
        }

        public IDbConnection GetDbConnection(bool master = false)
        {
           return _dbConnectionFactory.GetDbConnection(master);
        }

        /// <summary>
        /// 执行sql
        /// </summary>

        private async Task<T> Execute<T>(Func<IDbConnection, Task<T>> func, string sql, object param = null, bool master=false)
        {

            using (var conn = GetDbConnection(master))
            {
                var watch = new Stopwatch();
                watch.Start();

                var result = await func(conn);

                watch.Stop();
                _logger.LogInformation($"sql语句：{sql},参数{param?.ToJson()},执行时间：{watch.Elapsed.TotalMilliseconds}");
                return result;
            }
        }


        #region CUD
        public async Task<int> ExecuteAsync(string sql,
            object param = null,
            IDbTransaction transaction = null,
            int? commandTimeout = null,
            CommandType? commandType = null)
        {

            return await Execute(x => x.ExecuteAsync(sql,param, transaction,commandTimeout,commandType),sql,param,true);
        }


        public async Task<T> ExecuteScalarAsync<T>(
            string sql,
            object param = null,
            IDbTransaction transaction = null,
            int? commandTimeout = null,
            CommandType? commandType = null)
        {

            return await Execute(x => x.ExecuteScalarAsync<T>(sql, param, transaction, commandTimeout, commandType),
                sql, param, true);

        }

        public void AddExecute(string sql, object param = null, int? commandTimeout = null, CommandType? commandType = null)
        {
            _commands.Add(new Command()
            {
                Sql = sql,
                Param = param,
                CommandTimeout = commandTimeout,
                CommandType = commandType
            });
        }

        public async Task<bool> CommitAsync()
        {
            try
            {
                using (var conn = GetDbConnection(true))
                {

                    if (conn.State == ConnectionState.Closed)
                    {
                        conn.Open();
                    }

                    using (var tran = conn.BeginTransaction())
                    {
                        var watch = new Stopwatch();

                        foreach (var command in _commands)
                        {
                            watch.Start();

                            await conn.ExecuteAsync(command.Sql, command.Param, tran, command.CommandTimeout, command.CommandType);

                            watch.Stop();
                            _logger.LogInformation($"sql语句：{command.Sql},参数{command.Param?.ToJson()},执行时间：{watch.Elapsed.TotalMilliseconds}");
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
                _commands.Clear();
                _logger.LogError(e,"事务提交失败!");
                throw;
            }

        }
         internal class Command
         {
             public string Sql { get; set; }
             public object Param { get; set; }
             public int? CommandTimeout { get; set; }
             public CommandType? CommandType { get; set; }
         }
        

        #endregion

        #region Query

        public async Task<IEnumerable<T>> QueryAsync<T>(
            string sql,
            object param = null,
            IDbTransaction transaction = null,
            int? commandTimeout = null,
            CommandType? commandType = null)
        {

            return await Execute(x => x.QueryAsync<T>(sql, param, transaction, commandTimeout, commandType), sql, param);
        }


        public async Task<T> QueryFirstOrDefaultAsync<T>(
            string sql,
            object param = null,
            IDbTransaction transaction = null,
            int? commandTimeout = null,
            CommandType? commandType = null)
        {
            return await Execute(x => x.QueryFirstOrDefaultAsync<T>(sql, param, transaction, commandTimeout, commandType), sql, param);
        }

        public async Task<T> QueryFirstAsync<T>(
            string sql,
            object param = null,
            IDbTransaction transaction = null,
            int? commandTimeout = null,
            CommandType? commandType = null)
        {
            return await Execute(x => x.QueryFirstAsync<T>(sql, param, transaction, commandTimeout, commandType), sql, param);
        }

        public async Task<T> QuerySingleAsync<T>(

            string sql,
            object param = null,
            IDbTransaction transaction = null,
            int? commandTimeout = null,
            CommandType? commandType = null)
        {
            return await Execute(x => x.QuerySingleAsync<T>(sql, param, transaction, commandTimeout, commandType), sql, param);
        }

        public async Task<T> QuerySingleOrDefaultAsync<T>(
            string sql,
            object param = null,
            IDbTransaction transaction = null,
            int? commandTimeout = null,
            CommandType? commandType = null)
        {
            return await Execute(x => x.QuerySingleOrDefaultAsync<T>(sql, param, transaction, commandTimeout, commandType), sql, param);
        }

        public async Task<SqlMapper.GridReader> QueryMultipleAsync(
            string sql,
            object param = null,
            IDbTransaction transaction = null,
            int? commandTimeout = null,
            CommandType? commandType = null)
        {
            return await Execute(x => x.QueryMultipleAsync(sql, param, transaction, commandTimeout, commandType), sql, param);
        }
        #endregion


    }
}
