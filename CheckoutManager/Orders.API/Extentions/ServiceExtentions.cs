using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Orders.API.Configuration;
using RawRabbit;
using RawRabbit.DependencyInjection.ServiceCollection;
using RawRabbit.Instantiation;
using Service.Common.RabbitMQ;
using System;
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

        public static void AddDBHealthCheck(this IServiceCollection services, string connString)
        {
            services.AddHealthChecks().AddCheck("DB Order check", new SqlConnectionHealthCheck(connString), tags: new List<string> { "order_db" });
        }

        public static void AddRabbitMq(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddRawRabbit(new RawRabbitOptions
            {
                ClientConfiguration = new RawRabbit.Configuration.RawRabbitConfiguration
                {
                    Username = "guest",
                    Password = "guest",
                    VirtualHost = "/",
                    Port = 5672,
                    Hostnames = new List<string> { "localhost" },
                    RequestTimeout = TimeSpan.FromSeconds(10),
                    PublishConfirmTimeout = TimeSpan.FromSeconds(1),
                    RecoveryInterval = TimeSpan.FromSeconds(1),
                    PersistentDeliveryMode = true,
                    AutoCloseConnection = true,
                    AutomaticRecovery = true,
                    TopologyRecovery = true,
                    Exchange = new RawRabbit.Configuration.GeneralExchangeConfiguration
                    {
                        Durable = true,
                        AutoDelete = false,
                        Type = RawRabbit.Configuration.Exchange.ExchangeType.Topic
                    },
                    Queue = new RawRabbit.Configuration.GeneralQueueConfiguration
                    {
                        Durable = true,
                        AutoDelete = false,
                        Exclusive = false
                    }
                }
            });
        }
    }
}
