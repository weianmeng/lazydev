using lazyDev.Dapper;
using LazyDev.AspNetCore;

namespace SampleWeb.Service
{
    [Component]
    public class StudentService : IStudentService
    {
        private readonly IDapper dapper;

        public StudentService(IDapper dapper)
        {
            this.dapper = dapper;
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
            dapper.Query()
            throw  new LazyDevException("获取姓名失败","404");
        }
    }
}
