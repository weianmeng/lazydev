using System.Threading.Tasks;
using Dapper;
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


        public async Task<Student> GetName()
        {
            var id = await _dbContext.QueryAsync(x =>
                x.QueryFirstOrDefaultAsync<string>("SELECT id from pay_sequence where id=@id",
                    new {id = "4f500000-4c4f-0290-3869-08d6d5247fb4"}));
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
