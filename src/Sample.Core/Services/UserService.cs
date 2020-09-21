using System.Threading.Tasks;
using lazyDev.Dapper;
using Sample.Core.Entities;
using Sample.Core.Services;
using Sample.Services.Repositories;

namespace Sample.Services.Services
{
    public class UserService :IUserService
    {
        private readonly IUnitOfWork _unitOfWork;

        public UserService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }


        public async Task Update()
        {
            var userRepository = _unitOfWork.Repository<IUserRepository>();
             userRepository.UpdateName(new AppUser());
            await _unitOfWork.CommitAsync();
            //_userRepository.UpdateName();
        }
    }
}
