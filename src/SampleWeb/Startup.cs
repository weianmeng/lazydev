using lazyDev.Dapper;
using LazyDev.AspNetCore;
using LazyDev.Log.Trace;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Npgsql;
using Sample.Core.Repositories;
using Sample.Infrastructure;
using System.Reflection;

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
                    c.RegisterComponents(Assembly.GetExecutingAssembly());
                    c.RegisterFluentValidation(Assembly.GetExecutingAssembly());
                });

            services.AddDapper(x =>
            {
                x.ConnectionFunc = conn => new NpgsqlConnection(conn);
                x.MasterConn = "Host=127.0.0.1;Database=lazy_db;Username=postgres;Password=123456";
            });

            services.AddScoped<IUserRepository, UserRepository>();
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
            //µ÷ÓÃÁ´×·×Ù
            app.UserTrace();
            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
        }
    }
}