using System;
using LazyDev.Core.Domain.Entities;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace LazyDev.Core.Domain.Repositories
{
    public interface IRepository<TEntity, in TKey>  where TEntity:class,IEntity<TKey>
    {
        void Add(TEntity entity);
        void Add(IEnumerable<TEntity> entities);
        void Remove(TEntity entity);
        void Remove(IEnumerable<TEntity> entities);
        void Update(TEntity entity);
        void Update(IEnumerable<TEntity> entities);

        IQueryable<TEntity> GetQueryable();
        Task<TEntity> FindAsync(TKey id);
        Task<TEntity> FindAsync(Expression<Func<TEntity, bool>> expression);
        Task<bool> AnyAsync(Expression<Func<TEntity, bool>> expression);

    }
    public interface IRepository<TEntity>:IRepository<TEntity, int> where TEntity : class, IEntity<int>
    {

    }
}
