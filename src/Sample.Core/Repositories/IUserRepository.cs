using System.Threading.Tasks;
using Sample.Services.Entities;

namespace Sample.Services.Repositories
{
    public interface IUserRepository
    {
        Task<AppUser> GetAsync(int id);
        void UpdateName(AppUser appUser);
    }
}
