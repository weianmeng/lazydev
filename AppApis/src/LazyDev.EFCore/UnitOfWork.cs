using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using System.Threading.Tasks;
using LazyDev.EFCore.Entities;

namespace LazyDev.EFCore
{
    public class UnitOfWork:IUnitOfWork
    {
        private readonly DbContext _dbContext;

        public UnitOfWork(DbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IRepository<TEntity> Repository<TEntity>() where TEntity : class, IEntity
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
