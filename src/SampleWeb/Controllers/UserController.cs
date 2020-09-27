using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Sample.Core.Services;
using Sample.Services.Entities;
using Sample.Services.Repositories;

namespace SampleWeb.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserRepository _userRepository;

        private readonly IUserService _userService;
        public UserController(IUserRepository userRepository, IUserService userService)
        {
            _userRepository = userRepository;
            _userService = userService;
        }
        public async Task<AppUser> GetInfo(int id)
        {
           return await _userRepository.GetAsync(id);
        }
        [HttpGet("update")]
        public async Task Update()
        {
           await _userService.Update();
        }
    }
}
