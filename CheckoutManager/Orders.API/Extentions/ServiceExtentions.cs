using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Orders.API.Configuration;
using System.Collections.Generic;
using System.Text;

namespace Orders.API.Extentions
{
    public static class ServiceExtentions
    {
        public static void AddCORSService(this IServiceCollection services, AppSettings settings)
        {
            services.AddCors(opt => opt.AddPolicy("CorsPolicy", builder =>
            {
                builder
                    .AllowAnyHeader()
                    .AllowAnyMethod()
                    .AllowCredentials()
                    .WithOrigins(settings.AllowedAuthOrigins);
            }));
        }

        public static void AddJWTAuthentication(this IServiceCollection services, AppSettings settings)
        {
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(settings.Secret)),
                    ValidateIssuer = false,
                    ValidateAudience = false
                };
            });
        }

        public static void AddDBHealthCheck (this IServiceCollection services, string connString)
        {
            services.AddHealthChecks().AddCheck("DB Order check", new SqlConnectionHealthCheck(connString), tags: new List<string> { "order_db" });
        }
    }
}
