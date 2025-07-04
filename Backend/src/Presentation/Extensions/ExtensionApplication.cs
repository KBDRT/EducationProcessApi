using Presentation.Middleware;
using Serilog;
using Serilog.Events;

namespace Presentation.Extensions
{
    public static class ExtensionApplication
    {
        public static void ConfigureApp(this WebApplication app)
        {
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
        }

    }
}
