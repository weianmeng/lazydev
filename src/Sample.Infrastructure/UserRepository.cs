using System.Threading.Tasks;
using Dapper;
using lazyDev.Dapper;
using Sample.Core.Entities;
using Sample.Services.Repositories;

namespace Sample.Dal
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

        public void  UpdateName(AppUser appUser)
        {
            _dapperProxy.AddCommand((c, t) =>
                c.ExecuteAsync("update user set name=@name where id", new {name = appUser.Mobile}, t));
        }
    }
}
