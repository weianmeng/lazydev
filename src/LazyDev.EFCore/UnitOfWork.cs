using System;
using System.Threading.Tasks;
using LazyDev.EFCore.Entities;
using LazyDev.EFCore.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;

namespace LazyDev.EFCore
{
    public class UnitOfWork:IUnitOfWork
    {
        private readonly DbContext _dbContext;

        public UnitOfWork(DbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IRepository<TEntity, TKey> Repository< TEntity,TKey>() where TEntity : class, IEntity<TKey>
        {
           var rep = _dbContext.GetService<IRepository<TEntity, TKey>>();

           return rep;

        }

        public IRepository<TEntity> Repository<TEntity>() where TEntity : class, IEntity<int>
        {
            var rep = _dbContext.GetService<IRepository<TEntity>>();
            return rep;
        }


        public async Task<int> CommitAsync()
        {
           return await _dbContext.SaveChangesAsync();
        }
    }
}
