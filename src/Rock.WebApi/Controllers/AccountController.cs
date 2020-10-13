using Microsoft.AspNetCore.Mvc;
using Rock.Core.AccountApp;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;

namespace Rock.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IAccountService _accountService;

        public AccountController(IAccountService accountService)
        {
            _accountService = accountService;
        }

        [HttpPost("add")]
        [Authorize]
        public async Task<AddAccountOutput> Add(AccountInput accountInput)
        {
            return await _accountService.AddAsync(accountInput);
        }

        [HttpPost("info")]
        [Authorize]
        public async Task<AccountInfoOutput> GetAccountInfo(AccountInfoInput accountInfoInput)
        {
            return await _accountService.GetAccountInfo(accountInfoInput);
        }
    }
}
