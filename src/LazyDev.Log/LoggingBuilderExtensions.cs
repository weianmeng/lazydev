using LazyDev.Log.Trace;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Logging;

namespace LazyDev.Log
{
    public static class LoggingBuilderExtensions
    {

        public static ILoggingBuilder AddLayDevLogging(this ILoggingBuilder builder, IConfiguration configuration)
        {
             builder.Services.Configure<LazyDevLoggerOptions>(configuration.GetSection("LazyDevLog"));
             builder.Services.TryAddSingleton<ILogWriter, ConsoleWriter>();
             builder.Services.TryAddSingleton<ITraceSession,DefaultTraceSession>();
             builder.Services.TryAddSingleton<ILoggerProvider,LazyDevLoggerProvider>();
            return builder;
        }
    }
}
