using System.Threading.Tasks;

namespace lazyDev.Dapper
{
    public class UnitOfWork:IUnitOfWork
    {
        private readonly IDbContext _dbContext;

        public UnitOfWork(IDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<int> CommitAsync()
        {
           return await _dbContext.CommitAsync();
        }
    }
}
