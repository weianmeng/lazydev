using LazyDev.AspNetCore;

namespace SampleWeb.Service
{
    [Component]
    public class StudentService : IStudentService
    {
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
