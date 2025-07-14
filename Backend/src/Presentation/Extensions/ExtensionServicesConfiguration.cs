using Application.Auth;
using Application.Auth.Policy.Requirements;
using Application.CQRS.Teachers.Commands.CreateTeacher;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Serilog;
using Serilog.Events;
using Serilog.Sinks.SpectreConsole;
using System.Text;
using System.Text.Json.Serialization;

namespace Presentation.Extensions
{
    public static class ExtensionServicesConfiguration
    {
        private static IServiceCollection _services = null!;
        private static IConfiguration _configuration = null!;

        public static void AddServices(this IServiceCollection services, IConfiguration configuration)
        {
            _services = services;
            _configuration = configuration;

            AddSettings();

            _services.AddCors(options =>
            {
                options.AddDefaultPolicy(policy =>
                {
                    policy.WithOrigins("https://localhost:5173");
                    policy.AllowCredentials();
                    policy.AllowAnyMethod();
                    policy.AllowAnyHeader();
                });
            });

            AddAuth();

            _services.AddDependency();

            AddSerilogCustom();
            AddDatabase();
            AddRedis();

            _services.AddMemoryCache();

            _services.AddMediatR(cfg =>
                cfg.RegisterServicesFromAssembly(typeof(CreateTeacherCommand).Assembly));

            _services.AddControllers().AddJsonOptions(options =>
            {
                options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
            });

            _services.AddEndpointsApiExplorer();
            _services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "My API", Version = "v1" });

                c.AddSecurityDefinition("basic", new OpenApiSecurityScheme
                {
                    Name = "Authorization",
                    Type = SecuritySchemeType.Http,
                    Scheme = "basic",
                    In = ParameterLocation.Header,
                    Description = "Basic Authorization header using the Bearer scheme."
                });

                c.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "basic"
                            }
                        },
                        new string[] {}
                    }
                });
            });



        }

        private static void AddAuth()
        {
            var key = _configuration?.GetSection("AuthenticationSettings")?.GetSection("SecretKey").Value;

            if (String.IsNullOrWhiteSpace(key))
            {
                Log.Fatal("No Secretkey for Authentication!");
                return;
            }

            var tokenName = _configuration?.GetSection("AuthenticationSettings")?.GetSection("CookieNameForToken").Value;
            _services.AddAuthentication(options =>
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
                        context.Token = context.Request.Cookies[tokenName];

                        return Task.CompletedTask;
                    }
                };

            });

            _services.AddAuthorization(options => {
                options.AddPolicy("RoleAdmin", policy => policy.Requirements.Add(new RoleRequirement("Admin")));
                options.AddPolicy("RoleTeacher", policy => policy.Requirements.Add(new RoleRequirement("Teacher")));
                options.AddPolicy("RoleHead", policy => policy.Requirements.Add(new RoleRequirement("Head")));
            });
        }

        private static void AddDatabase()
        {
            string? connectionDB = _configuration?.GetConnectionString("DefaultConnectionDataBase");

            if (String.IsNullOrWhiteSpace(connectionDB))
            {
                Log.Fatal("DatabasePostrge is not working! No connection string");
                return;
            }

            _services.AddDbContext<ApplicationContext>(options => options.UseNpgsql(connectionDB));
        }

        private static void AddRedis()
        {
            string? connectionRedis = _configuration?.GetConnectionString("DefaultConnectionRedis");

            if (String.IsNullOrWhiteSpace(connectionRedis))
            {
                Log.Fatal("Redis is not working! No connection string");
                return;
            }

            _services.AddStackExchangeRedisCache(options => {
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

        private static void AddSerilogCustom()
        {
            Log.Logger = new LoggerConfiguration()
                .WriteTo.SpectreConsole("{Timestamp:HH:mm:ss} [{Level}] {Message}{NewLine}{Exception}")
                .MinimumLevel.Verbose()
                .MinimumLevel.Override("Microsoft.AspNetCore", LogEventLevel.Information)
                .MinimumLevel.Override("Microsoft.AspNetCore.Hosting", LogEventLevel.Warning)
                .MinimumLevel.Override("Microsoft.AspNetCore.Mvc", LogEventLevel.Warning)
                .MinimumLevel.Override("Microsoft.EntityFrameworkCore", LogEventLevel.Warning)
                .MinimumLevel.Override("Microsoft.AspNetCore.Routing", LogEventLevel.Warning)
                .MinimumLevel.Override("Microsoft.AspNetCore.Cors", LogEventLevel.Warning)
                .CreateLogger();

            _services.AddSerilog();
        }

        private static void AddSettings()
        {
            _services.Configure<AuthSettings>(_configuration.GetSection("AuthenticationSettings"));
        }

    }
}
