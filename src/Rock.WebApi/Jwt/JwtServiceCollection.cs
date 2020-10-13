using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;

namespace Rock.WebApi.Jwt
{
    public static class JwtServiceCollection
    {
        public static IServiceCollection AddJwtAuth(this IServiceCollection services,IConfigurationSection section)
        {
             services.Configure<JwtOptions>(section);

            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(options =>
            {
                options.SaveToken = true;
                options.RequireHttpsMetadata = false;
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(section["SigningKey"])),
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidIssuer = section["Issuer"],
                    ValidAudience = section["Audience"]
                };
            });

            return services;
        }
    }
}
