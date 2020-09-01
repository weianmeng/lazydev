using lazyDev.Dapper;
using LazyDev.AspNetCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Npgsql;
using System.Reflection;
using LazyDev.Log.Trace;

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
                x.ConnectionFunc = conn => new NpgsqlConnection(conn);
                x.MasterConn = "";
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
            //������׷��
            app.UserTrace();
            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
        }
    }
}