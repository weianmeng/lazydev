using System.Threading.Tasks;

namespace lazyDev.Dapper
{
    public interface IUnitOfWork
    {
        Task<int> CommitAsync();
    }
}
