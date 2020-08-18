using System;
using System.Data;
using System.Threading.Tasks;

namespace lazyDev.Dapper
{
    public interface IDbContext
    {
        IDbConnection GetConnection(bool isMaster = true);
        void AddCommand(Func<IDbConnection, IDbTransaction, Task> func);
        Task<bool> CommitAsync();

        Task<T> QueryAsync<T>(Func<IDbConnection, Task<T>> func);
        T Query<T>(Func<IDbConnection,T> func);
    }
}
