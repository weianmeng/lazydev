using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace LazyDev.AspNetCore
{
    public class GlobalExceptionFilter : ExceptionFilterAttribute
    {
        private readonly ILogger<GlobalExceptionFilter> _logger;
        private readonly IWebHostEnvironment _env;

        public GlobalExceptionFilter(ILogger<GlobalExceptionFilter> logger, IWebHostEnvironment env)
        {
            _logger = logger;
            _env = env;
        }

        public override void OnException(ExceptionContext context)
        {
            if (context.Exception is LazyDevException lazyDevException)
            {
                context.Result = new ObjectResult(new LazyResult
                    {Success = false, Code = lazyDevException.Code, Msg = lazyDevException.Message});

                context.ExceptionHandled = true;
            }
            else
            {
                _logger.LogError(context.Exception, "system error");

                if (_env.IsDevelopment())
                {
                    base.OnException(context);
                }
                else
                {
                    context.Result = new ObjectResult(new LazyResult
                        { Success = false, Code ="500", Msg = context.Exception.Message });
                }
            }
        }
    }
}