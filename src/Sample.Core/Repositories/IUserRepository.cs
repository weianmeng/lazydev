using System.Threading.Tasks;
using Sample.Core.Entities;

namespace Sample.Core.Repositories
{
    public interface IUserRepository
    {
        Task<AppUser> GetAppUserById(int id);
        Task<int> UpdateName(AppUser appUser);
    }
}
