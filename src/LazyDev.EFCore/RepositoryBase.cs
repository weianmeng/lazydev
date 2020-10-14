using LazyDev.EFCore.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace LazyDev.EFCore
{
    public class RepositoryBase<TEntity> : IRepository<TEntity> where TEntity : class, IEntity
    {
        private readonly DbSet<TEntity> _dbSet;
        public RepositoryBase(DbContext dbContext)
        {
            _dbSet = dbContext.Set<TEntity>();
        }

        private bool _ignoreQueryFilter;
        public void IgnoreQueryFilters()
        {
            _ignoreQueryFilter = true;
        }

        public async Task AddAsync(TEntity entity)
        {
           await _dbSet.AddAsync(entity);
        }

        public async Task AddAsync(IEnumerable<TEntity> entities)
        {
            await _dbSet.AddRangeAsync(entities);
        }

        public async Task<bool> AnyAsync(Expression<Func<TEntity, bool>> expression)
        {
            return await GetQueryable().AnyAsync(expression);
        }
        public async Task<long> CountAsync(Expression<Func<TEntity, bool>> expression)
        {
            return await GetQueryable().LongCountAsync(expression);
        }
        public async Task<TEntity> FindAsync(int id)
        {
            return await FindAsync(x => x.Id == id);
        }

        public async Task<TEntity> FindAsync(Expression<Func<TEntity, bool>> expression)
        {
            return await GetQueryable().FirstOrDefaultAsync(expression);
        }

        public IQueryable<TEntity> GetQueryable()
        {
            return _ignoreQueryFilter ? _dbSet.IgnoreQueryFilters() : _dbSet.AsQueryable();
        }

        public void Remove(TEntity entity)
        {
            _dbSet.Remove(entity);
        }

        public void Remove(IEnumerable<TEntity> entities)
        {
            _dbSet.RemoveRange(entities);
        }

        public void Update(TEntity entity)
        {
            _dbSet.Update(entity);
        }

        public void Update(IEnumerable<TEntity> entities)
        {
            _dbSet.UpdateRange(entities);
        }
    }
}
