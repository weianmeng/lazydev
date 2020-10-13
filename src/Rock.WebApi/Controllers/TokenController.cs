using LazyDev.Core.Runtime;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Rock.Core.AccountApp;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using LazyDev.Core.Extensions;
using Newtonsoft.Json;

namespace Rock.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TokenController : ControllerBase
    {
        private readonly IAccountService _accountService;

        public TokenController(IAccountService accountService)
        {
            _accountService = accountService;
        }
        [HttpPost]
        public async Task<TokenModel> Get(AccountLoginInput loginInput)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var info = await _accountService.LoginAsync(loginInput);
            var key = Encoding.ASCII.GetBytes("PDv7DrqznYL6nv7DrqzjnQYO9JxIsWdcjnQYL6nu0f");
            var expDate = DateTime.Now.AddMinutes(30);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Issuer = "localhost",
                Audience = "localhost",
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim(LazyDevSessionDefaults.UId, info.Uid.ToString()),
                    new Claim(LazyDevSessionDefaults.TenantId, info.TenantId.ToString())
                }),
                Expires = expDate,
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);

            return new TokenModel
            {
                AccessToken = tokenHandler.WriteToken(token),
                Expires = expDate.ToTimeStamp()
            };
        }

        public class TokenModel
        {
            [JsonProperty("access_token")]
            public string AccessToken { get; set; }
            public long Expires { get; set; }
        }
    }
}
