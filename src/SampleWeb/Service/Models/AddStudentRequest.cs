using System;

namespace SampleWeb.Service.Models
{
    public class AddStudentRequest
    {
        public string Name { get; set; }
        public int Age { get; set; }
        public DateTime CreateTime { get; set; }
    }
}
