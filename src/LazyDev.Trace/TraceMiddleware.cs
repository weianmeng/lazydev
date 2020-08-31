using System.Diagnostics;
using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;

namespace LazyDev.Trace
{
    public class TraceMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<TraceMiddleware> _logger;
        public TraceMiddleware(RequestDelegate next, ILogger<TraceMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }
        public async Task InvokeAsync(HttpContext context)
        {
            var sw = new Stopwatch();
            sw.Start();
            await _next(context);
           _logger.LogTrace(LogLevel.Information,new TraceMessage()
           {
               TimeSpan = sw.ElapsedMilliseconds
           });
        }
    }
}
