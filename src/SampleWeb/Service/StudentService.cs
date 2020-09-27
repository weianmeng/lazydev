using LazyDev.Core.Dependency;
using LazyDev.Core.Exception;
using System;
using System.Threading.Tasks;

namespace SampleWeb.Service
{
    [Service]
    public class StudentService : IStudentService
    {



        public  Task<Student> GetName()
        {

            throw  new NotImplementedException();
        }

        public Student GetNameException()
        {
         
            throw  new LazyDevException("获取姓名失败",404);
        }
    }
}
