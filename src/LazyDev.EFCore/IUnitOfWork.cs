using LazyDev.EFCore.Entities;
using LazyDev.EFCore.Repositories;
using System.Threading.Tasks;

namespace LazyDev.EFCore
{
    public interface IUnitOfWork
    {
        IRepository<TEntity, TKey> Repository<TEntity,TKey>() where TEntity : class, IEntity<TKey>;
        IRepository<TEntity> Repository<TEntity>() where TEntity : class, IEntity<int>;
        Task<int> CommitAsync();
    }
}
