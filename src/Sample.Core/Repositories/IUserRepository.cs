using System.Threading.Tasks;
using lazyDev.Dapper;
using Sample.Core.Entities;

namespace Sample.Core.Repositories
{
    public interface IUserRepository:IRepository
    {
        Task<AppUser> GetAppUserById(int id);
        Task<int> UpdateName(AppUser appUser);
    }
}
