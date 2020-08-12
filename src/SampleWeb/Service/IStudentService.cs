namespace SampleWeb.Service
{
    public interface IStudentService
    {
        Student GetName();
        Student GetNameException();

    }
    public class Student
    {
        public string Name { get; set; }
    }
}