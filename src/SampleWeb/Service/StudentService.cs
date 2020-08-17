using lazyDev.Dapper;
using LazyDev.AspNetCore;

namespace SampleWeb.Service
{
    [Component]
    public class StudentService : IStudentService
    {
        private readonly IDbContext _dbContext;

        public StudentService(IDbContext dbContext)
        {
            _dbContext = dbContext;
        }


        public Student GetName()
        {
            return new Student
            {
                Name = "李时珍"
            };
        }

        public Student GetNameException()
        {
            
            throw  new LazyDevException("获取姓名失败","404");
        }
    }
}
