using System;
using System.Data;
using System.Threading.Tasks;

namespace lazyDev.Dapper
{
    public interface IDbContext
    {
        Task<T> QueryAsync<T>(Func<IDbConnection, Task<T>> func,bool readMaster =false);
        //Task<T> ExecuteAsync<T>(Func<IDbConnection, Task<T>> func);
        void AddCommand(Func<IDbConnection, IDbTransaction, Task> func);
        Task<int> CommitAsync();
    }
}