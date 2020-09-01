using Microsoft.AspNetCore.Builder;

namespace LazyDev.Log.Trace
{
    public static class ApplicationExtensions
    {
        public static IApplicationBuilder UserTrace(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<TraceMiddleware>();
        }
    }
}
