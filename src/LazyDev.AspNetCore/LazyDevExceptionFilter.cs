using LazyDev.Core.Common;
using LazyDev.Core.Exception;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace LazyDev.AspNetCore
{
    public class LazyDevExceptionFilter : ExceptionFilterAttribute
    {
        private readonly ILogger<LazyDevExceptionFilter> _logger;
        private readonly IWebHostEnvironment _env;

        public LazyDevExceptionFilter(ILogger<LazyDevExceptionFilter> logger, IWebHostEnvironment env)
        {
            _logger = logger;
            _env = env;
        }

        public override void OnException(ExceptionContext context)
        {
            if (context.Exception is FriendlyException lazyDevException)
            {
                context.Result = new ObjectResult(LazyDevResult.Failed(lazyDevException.Code, lazyDevException.Message));

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
                    context.Result = new ObjectResult(LazyDevResult.Failed(500, context.Exception.Message));
                    context.ExceptionHandled = true;
                }
            }
        }
    }
}