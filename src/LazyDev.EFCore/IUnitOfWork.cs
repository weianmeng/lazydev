using System.Threading.Tasks;
using LazyDev.EFCore.Entities;

namespace LazyDev.EFCore
{
    public interface IUnitOfWork
    {
        IRepository<TEntity, TKey> Repository<TEntity,TKey>() where TEntity : class, IEntity<TKey>;
        IRepository<TEntity> Repository<TEntity>() where TEntity : class, IEntity<int>;
        Task<int> CommitAsync();
    }
}
