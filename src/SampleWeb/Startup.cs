using LazyDev.AspNetCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Reflection;
using lazyDev.Dapper;
using lazyDev.Dapper.MySql;

namespace SampleWeb
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers()
                .AddLazyDev(c =>
                {
                    c.RegisterServiceFromAssemblies(Assembly.GetExecutingAssembly());
                    c.RegisterValidatorsFromAssemblies(Assembly.GetExecutingAssembly());
                });

            services.AddDapper(x =>
            {
                x.SetDbFactory<MySqlDbConnectionFactory>();
                x.MasterConn = "Server=106.75.10.95;Port=13306;Database=pay;User ID=root;Password=_sys31UC;";
                x.ReplicasConn = new[]
                {
                    "Server=106.75.10.95;Port=13306;Database=pay;User ID=root;Password=_sys31UC;"
                };
            });
            services.AddSwaggerDocument();
            services.AddHttpClient();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseOpenApi();
            app.UseSwaggerUi3();
            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
        }
    }
}