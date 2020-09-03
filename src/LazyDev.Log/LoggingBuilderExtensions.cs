using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace LazyDev.Log
{
    public static class LoggingBuilderExtensions
    {

        public static ILoggingBuilder AddLayDevLogging(this ILoggingBuilder builder, IConfiguration configuration)
        {
            builder.AddProvider(new LazyDevLoggerProvider());
            return builder;
        }
    }
}
