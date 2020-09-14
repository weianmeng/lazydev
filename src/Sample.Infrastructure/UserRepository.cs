using System.Threading.Tasks;
using lazyDev.Dapper;
using Sample.Core.Entities;
using Sample.Core.Repositories;


namespace Sample.Infrastructure
{

    public class UserRepository:IUserRepository
    {
        private readonly IDapperProxy _dapperProxy;

        public UserRepository(IDapperProxy dapperProxy)
        {
            _dapperProxy = dapperProxy;
        }

        public async Task<AppUser> GetAppUserById(int id)
        {
           return await _dapperProxy.QueryFirstOrDefaultAsync<AppUser>("select id,mobile from app_member where id=@id", new {id});
        }

        public async Task<int> UpdateName(AppUser appUser)
        {
          return  await _dapperProxy.ExecuteAsync("update user set name=@name where id", new {name = appUser.Mobile});
        }
    }
}
