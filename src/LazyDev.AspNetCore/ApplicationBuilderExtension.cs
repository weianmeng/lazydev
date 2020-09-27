using Microsoft.AspNetCore.Builder;

namespace LazyDev.AspNetCore
{
    public static class ApplicationBuilderExtension
    {
        public static IApplicationBuilder UseLazyDev(this IApplicationBuilder app)
        {
            app.UseOpenApi();
            app.UseSwaggerUi3();
            return app;
        }
    }
}
