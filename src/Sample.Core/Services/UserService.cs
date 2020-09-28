using System.Threading.Tasks;

namespace Sample.Services.Services
{
    public class UserService :IUserService
    {



        public  Task Update()
        {
          return  Task.CompletedTask;
        }
    }
}
