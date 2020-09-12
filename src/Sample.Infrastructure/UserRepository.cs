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
           return await _dapperProxy.QueryFirstAsync<AppUser>("select id,name from app_user where id=@id", new {id});
        }

        public async Task<int> UpdateName(AppUser appUser)
        {
          return  await _dapperProxy.ExecuteAsync("update user set name=@name where id", new {name = appUser.Name});
        }
    }
}
