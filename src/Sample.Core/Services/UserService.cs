using System.Threading.Tasks;
using lazyDev.Dapper;
using Sample.Core.Entities;
using Sample.Core.Repositories;

namespace Sample.Core.Services
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
            await userRepository.UpdateName(new AppUser());
            await _unitOfWork.CommitAsync();
            //_userRepository.UpdateName();
        }
    }
}
