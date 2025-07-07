using Application.CQRS.Teachers.Commands.CreateTeacher;
using FluentValidation;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Serilog;
using System.Security.Cryptography;
using System.Text;
using System.Text.Json.Serialization;

namespace Presentation.Extensions
{

    public static class ExtensionServicesConfiguration
    {

        public static void AddServices(this IServiceCollection services, IConfiguration configuration)
        {

            services.AddCors(options =>
            {
                options.AddDefaultPolicy(policy =>
                {
                    policy.WithOrigins("https://localhost:5173");
                    policy.AllowCredentials();
                    policy.AllowAnyMethod();
                    policy.AllowAnyHeader();
                });
            });

            AddAuth(services, configuration);

            services.AddDependency();

            AddDatabase(services, configuration);
            AddRedis(services, configuration);

            services.AddMemoryCache();

            services.AddMediatR(cfg =>
                cfg.RegisterServicesFromAssembly(typeof(CreateTeacherCommand).Assembly));

            services.AddControllers().AddJsonOptions(options =>
            {
                options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
            });

            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen();
        }


        private static void AddAuth(IServiceCollection services, IConfiguration configuration)
        {
            var key = "KEYKEYKEYKEYKEYKEYKEYKEYKEYKEYKEYKEY";


            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultSignInScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(JwtBearerDefaults.AuthenticationScheme, options =>
            {
                options.RequireHttpsMetadata = true;
                options.SaveToken = true;
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key))
                };

                options.Events = new JwtBearerEvents 
                {
                    OnMessageReceived = context =>
                    {
                        context.Token = context.Request.Cookies["token"];

                        return Task.CompletedTask;
                    }
                };

            });

            services.AddAuthorization();
        }

        private static void AddDatabase(IServiceCollection services, IConfiguration configuration)
        {
            string? connectionDB = configuration?.GetConnectionString("DefaultConnectionDataBase");

            if (String.IsNullOrWhiteSpace(connectionDB))
            {
                Log.Fatal("DatabasePostrge is not working! No connection string");
                return;
            }

            services.AddDbContext<ApplicationContext>(options => options.UseNpgsql(connectionDB));
        }

        private static void AddRedis(IServiceCollection services, IConfiguration configuration)
        {
            string? connectionRedis = configuration?.GetConnectionString("DefaultConnectionRedis");

            if (String.IsNullOrWhiteSpace(connectionRedis))
            {
                Log.Fatal("Redis is not working! No connection string");
                return;
            }

            services.AddStackExchangeRedisCache(options => {
                options.InstanceName = "local";
                options.ConfigurationOptions = new StackExchange.Redis.ConfigurationOptions
                {
                    EndPoints = { connectionRedis },
                    ConnectTimeout = 3500,
                    SyncTimeout = 3500,
                    AbortOnConnectFail = false
                };
            });
        }

    }
}
