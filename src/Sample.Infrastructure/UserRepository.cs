using Dapper;
using lazyDev.Dapper;
using Sample.Services.Repositories;
using System.Threading.Tasks;
using Sample.Services.Entities;

namespace Sample.Dal
{
    public class UserRepository:IUserRepository
    {
        private readonly IDbContext _dbContext;

        public UserRepository(IDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<AppUser> GetAsync(int id)
        {
            return await _dbContext.QueryAsync(x =>
                x.QueryFirstOrDefaultAsync<AppUser>("select id,mobile from app_member where id=@id", new {id}));
        }

        public void  UpdateName(AppUser appUser)
        {
            _dbContext.AddCommand((conn, tran) =>
                conn.ExecuteAsync("update app_member set mobile=@Mobile where id=@Id", appUser, tran));
        }

        public  void Insert(AppUser appUser)
        {
            const string insertSql = "insert into app_member(mobile) values (@mobile)";
            _dbContext.AddCommand((conn, tran) => conn.ExecuteAsync(insertSql, appUser, tran));
        }

    }
}
