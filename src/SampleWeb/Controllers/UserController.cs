using Microsoft.AspNetCore.Mvc;
using Sample.Core.Entities;
using Sample.Core.Repositories;
using System.Threading.Tasks;

namespace SampleWeb.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserRepository _userRepository;

        public UserController(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }
        public async Task<AppUser> GetInfo(int id)
        {
           return await _userRepository.GetAppUserById(id);
        }
    }
}
