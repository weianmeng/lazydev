using System.Threading.Tasks;
using LazyDev.EFCore.Entities;

namespace LazyDev.EFCore
{
    public interface IUnitOfWork
    {
        IRepository<TEntity> Repository<TEntity>() where TEntity : class, IEntity;
        Task<int> CommitAsync();
    }
}
