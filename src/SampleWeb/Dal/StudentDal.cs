using lazyDev.Dapper;
using SampleWeb.Service;

namespace SampleWeb.Dal
{
    public class StudentDal
    {
        private readonly IDapperProxy _dapperProxy;

        public StudentDal(IDapperProxy dapperProxy)
        {
            _dapperProxy = dapperProxy;
        }

        public void Add(Student student)
        {
            /////
        }

    }
}
