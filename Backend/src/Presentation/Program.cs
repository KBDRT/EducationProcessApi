using Application.CQRS.Teachers.Commands.CreateTeacher;
using EducationProcess.Presentation;
using Microsoft.EntityFrameworkCore;
using Presentation.Middleware;
using Serilog;
using Serilog.Events;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

string connection = builder.Configuration.GetConnectionString("DefaultConnection");


builder.AddSerilog();

builder.Services.AddDbContext<ApplicationContext>(options => options.UseNpgsql(connection));

builder.AddDependency();

builder.Services.AddMediatR(cfg =>
    cfg.RegisterServicesFromAssembly(typeof(CreateTeacherCommand).Assembly));

builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(policy =>
    {
        policy.WithOrigins("http://localhost:5173");
        policy.AllowAnyHeader();
        policy.AllowAnyMethod();
    });
});


builder.Services.AddControllers().AddJsonOptions(options =>
{
    options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
});

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

app.UseSerilogRequestLogging(options =>
{
    options.MessageTemplate = " REQUEST RESULT! - REQUEST: {RequestPath}, METHOD: {RequestMethod}, STATUS_CODE: {StatusCode}, ELAPSED: {Elapsed}";
    options.GetLevel = (httpContext, elapsed, ex) => LogEventLevel.Information;
});

app.UseMiddleware<RequestLoggingMiddleware>();
app.UseMiddleware<ExceptionMiddleware>();

//if (app.Environment.IsDevelopment())
//{
app.UseSwagger();
    app.UseSwaggerUI(options =>
    {
        options.SwaggerEndpoint("/swagger/v1/swagger.json", "v1");
        options.RoutePrefix = string.Empty;
    });
//}

app.UseCors();
app.MapControllers();

app.Run();
