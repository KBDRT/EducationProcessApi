using Application.CQRS.Teachers.Commands.CreateTeacher;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Serilog;
using System.Text.Json.Serialization;

namespace Presentation.Extensions
{

    public static class ExtensionServicesConfiguration
    {

        public static void AddServices(this IServiceCollection services, IConfiguration configuration)
        {

            services.AddDependency();

            AddDatabase(services, configuration);
            AddRedis(services, configuration);

            services.AddMemoryCache();

            services.AddMediatR(cfg =>
                cfg.RegisterServicesFromAssembly(typeof(CreateTeacherCommand).Assembly));

            services.AddCors(options =>
            {
                options.AddDefaultPolicy(policy =>
                {
                    policy.WithOrigins("http://localhost:5173");
                    policy.AllowAnyHeader();
                    policy.AllowAnyMethod();
                });
            });

            services.AddControllers().AddJsonOptions(options =>
            {
                options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
            });

            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen();
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
