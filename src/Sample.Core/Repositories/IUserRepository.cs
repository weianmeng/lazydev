using System.Threading.Tasks;
using lazyDev.Dapper;
using Sample.Core.Entities;

namespace Sample.Services.Repositories
{
    public interface IUserRepository:IRepository
    {
        Task<AppUser> GetAppUserById(int id);
        void UpdateName(AppUser appUser);
    }
}
