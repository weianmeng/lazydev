using lazyDev.Dapper;
using LazyDev.AspNetCore;
using System.Threading.Tasks;

namespace SampleWeb.Service
{
    [Component]
    public class StudentService : IStudentService
    {
        private readonly IDapperProxy _dapper;


        public StudentService(IDapperProxy dapper)
        {
            _dapper = dapper;
        }


        public async Task<Student> GetName()
        {
            var id = await _dapper.QueryFirstOrDefaultAsync<string>("SELECT id from pay_sequence where id=@id",
                new {id = "4f500000-4c4f-0290-3869-08d6d5247fb4"});
            return new Student
            {
                Name = id
            };
        }

        public Student GetNameException()
        {
         
            throw  new LazyDevException("获取姓名失败","404");
        }
    }
}
