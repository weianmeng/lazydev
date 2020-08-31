using Microsoft.AspNetCore.Builder;

namespace LazyDev.Trace
{
    public static class ApplicationExtensions
    {
        public static IApplicationBuilder UserTrace(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<TraceMiddleware>();
        }
    }
}
