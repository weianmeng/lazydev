using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using Dapper;

namespace lazyDev.Dapper
{
    public interface IDapperProxy
    {
        void AddExecute(string sql, object param = null, int? commandTimeout = null, CommandType? commandType = null);
        Task<bool> CommitAsync();

        Task<int> ExecuteAsync(string sql,
            object param = null,
            IDbTransaction transaction = null,
            int? commandTimeout = null,
            CommandType? commandType = null);

        Task<T> ExecuteScalarAsync<T>(
            string sql,
            object param = null,
            IDbTransaction transaction = null,
            int? commandTimeout = null,
            CommandType? commandType = null);

        Task<IEnumerable<T>> QueryAsync<T>(
            string sql,
            object param = null,
            IDbTransaction transaction = null,
            int? commandTimeout = null,
            CommandType? commandType = null);

        Task<T> QueryFirstOrDefaultAsync<T>(
            string sql,
            object param = null,
            IDbTransaction transaction = null,
            int? commandTimeout = null,
            CommandType? commandType = null);

        Task<T> QueryFirstAsync<T>(
            string sql,
            object param = null,
            IDbTransaction transaction = null,
            int? commandTimeout = null,
            CommandType? commandType = null);

        Task<T> QuerySingleAsync<T>(

            string sql,
            object param = null,
            IDbTransaction transaction = null,
            int? commandTimeout = null,
            CommandType? commandType = null);

        Task<T> QuerySingleOrDefaultAsync<T>(
            string sql,
            object param = null,
            IDbTransaction transaction = null,
            int? commandTimeout = null,
            CommandType? commandType = null);

        Task<SqlMapper.GridReader> QueryMultipleAsync(
            string sql,
            object param = null,
            IDbTransaction transaction = null,
            int? commandTimeout = null,
            CommandType? commandType = null);
    }
}