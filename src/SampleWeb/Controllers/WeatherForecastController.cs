using LazyDev.AspNetCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SampleWeb.Service;
using SampleWeb.Service.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SampleWeb.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        private readonly ILogger<WeatherForecastController> _logger;
        private readonly IStudentService _studentService;

        public WeatherForecastController(ILogger<WeatherForecastController> logger, IStudentService studentService)
        {
            _logger = logger;
            _studentService = studentService;

        }

        [HttpGet]
        public IEnumerable<WeatherForecast> Get()
        {
            var rng = new Random();
            return Enumerable.Range(1, 5).Select(index => new WeatherForecast
                {
                    Date = DateTime.Now.AddDays(index),
                    TemperatureC = rng.Next(-20, 55),
                    Summary = Summaries[rng.Next(Summaries.Length)]
                })
                .ToArray();
        }

        [HttpGet("hello")]
        public async Task<Student> Hello()
        {
            return await _studentService.GetName();
        }

        [HttpGet("helloError")]
        public Student GetNameException()
        {
            return _studentService.GetNameException();
        }

        [HttpGet("Teacher")]
        public ILazyResult Teacher()
        {
            return new LazyResult {Code = "10000", Data = new Student {Name = "小明"}};
        }

        [HttpGet("SysException")]
        public IActionResult SysException()
        {
            throw new Exception();
        }

        [HttpPost("addstudent")]
        public string AddStudent(AddStudentRequest student)
        {
            return "";
        }
        [HttpGet("HiTeacher")]
        public string HiTeacher()
        {
            return "";
        }
    }
}