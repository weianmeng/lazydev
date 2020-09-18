using System.Threading.Tasks;

namespace lazyDev.Dapper
{
    public interface IUnitOfWork
    {
        T Repository<T>() where T : class, IRepository;
        Task<int> CommitAsync();
    }
}
