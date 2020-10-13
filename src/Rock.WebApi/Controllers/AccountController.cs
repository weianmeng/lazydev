﻿using Microsoft.AspNetCore.Mvc;
using Rock.Core.AccountApp;
using System.Threading.Tasks;

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
        public async Task<AddAccountOutput> Add(AccountInput accountInput)
        {
            return await _accountService.AddAsync(accountInput);
        }

        [HttpPost("info")]
        public async Task<AccountInfoOutput> GetAccountInfo(AccountInfoInput accountInfoInput)
        {
            return await _accountService.GetAccountInfo(accountInfoInput);
        }
    }
}
