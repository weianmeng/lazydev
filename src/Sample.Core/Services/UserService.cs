using Sample.Core.Repositories;

namespace Sample.Core.Services
{
    public class UserService :IUserService
    {
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public void Update()
        {
            //_userRepository.UpdateName();
        }
    }
}
