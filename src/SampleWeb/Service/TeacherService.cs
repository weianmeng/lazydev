using LazyDev.AspNetCore;

namespace SampleWeb.Service
{

    public class TeacherService<T>:ITeacherService<T>
    {
        public string Hi()
        {
            return $"Hello{typeof(T).Name}";
        }
    }
}
