using System.Threading.Tasks;

namespace SampleWeb.Service
{
    public interface IStudentService
    {
        Task<Student> GetName();
        Student GetNameException();

    }
    public class Student
    {
        public string Name { get; set; }
    }
}