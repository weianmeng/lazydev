using System.Threading.Tasks;
using LazyDev.Core.Dependency;

namespace Rock.Core.AccountApp
{
    public interface IAccountService:IScopedDependency
    {
        Task<AddAccountOutput> AddAsync(AccountInput accountInput);
        Task<AccountInfoOutput> GetAccountInfo(AccountInfoInput accountInfoInput);
    }
}