using System;
using System.Data;
using System.Threading.Tasks;

namespace lazyDev.Dapper
{
    public interface IDbContext
    {
        void AddCommand(Func<IDbConnection, IDbTransaction, Task> func);
        Task<bool> CommitAsync();
        IDbConnection GetConnection(bool isMaster = true);
    }
}
