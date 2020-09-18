using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Logging;

namespace LazyDev.Log.Postgres
{
    public static class LoggingBuilderExtensions
    {
        public static ILoggingBuilder AddPostgresLogging(this ILoggingBuilder builder, IConfiguration configuration)
        {
            builder.Services.Configure<PostgresLogOption>(configuration.GetSection("PostgresLog"));
            builder.Services.RemoveAll<ILogWriter>();
            builder.Services.TryAddSingleton<ILogWriter, PostgresLogWriter>();
            return builder;
        }
    }
}
