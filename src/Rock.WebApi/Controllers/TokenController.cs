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
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using Rock.WebApi.Jwt;

namespace Rock.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TokenController : ControllerBase
    {
        private readonly IAccountService _accountService;
        private readonly IOptions<JwtOptions> _jwtOptions;

        public TokenController(IAccountService accountService,IOptions<JwtOptions> jwtOptions)
        {
            _accountService = accountService;
            _jwtOptions = jwtOptions;
        }
        [HttpPost]
        public async Task<TokenModel> Get(AccountLoginInput loginInput)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var info = await _accountService.LoginAsync(loginInput);
            var key = Encoding.ASCII.GetBytes(_jwtOptions.Value.SigningKey);

            var expDate = DateTime.Now.AddMinutes(_jwtOptions.Value.ExpTime);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Issuer = _jwtOptions.Value.Issuer,
                Audience = _jwtOptions.Value.Audience,
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim(LazyDevSessionDefaults.UId, info.Uid.ToString()),
                    new Claim(LazyDevSessionDefaults.TenantId, info.TenantId.ToString())
                }),
                Expires = expDate,
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key),
                    SecurityAlgorithms.HmacSha256Signature)
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
