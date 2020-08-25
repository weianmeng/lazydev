using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using Dapper;

namespace lazyDev.Dapper
{
    public interface IDapperProxy
    {
        IDbConnection ReadConnection { get; }
        IDbConnection WriteReadConnection { get; }
        Task<IEnumerable<T>> QueryAsync<T>(string sql, object param = null);
        Task<T> QueryFirstAsync<T>(string sql, object param = null);
        Task<T> QueryFirstOrDefaultAsync<T>(string sql, object param = null);
        Task<T> QuerySingleAsync<T>(string sql, object param = null);
        Task<T> QuerySingleOrDefaultAsync<T>(string sql, object param = null);
        Task<SqlMapper.GridReader> QueryMultipleAsync(string sql, object param = null);
        Task<int> ExecuteAsync(string sql, object param = null);
        Task<T> ExecuteScalarAsync<T>(string sql, object param = null);
        Task<IDataReader> ExecuteReaderAsync(string sql, object param = null);
        void AddCommand(Func<IDbConnection, IDbTransaction, Task> func);
        Task<int> CommitAsync();
    }
}