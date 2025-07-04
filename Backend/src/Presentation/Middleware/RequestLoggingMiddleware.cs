
using Serilog;

namespace Presentation.Middleware
{
    public class RequestLoggingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<RequestLoggingMiddleware> _logger;

        public RequestLoggingMiddleware(RequestDelegate next, ILogger<RequestLoggingMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }


        public async Task InvokeAsync(HttpContext context)
        {
            HttpRequest? request = context.Request; 
            _logger.LogInformation($"REQUEST: {request.Path}, {request.Method}");

            await _next.Invoke(context);

            //_logger.LogDebug($" REQUEST RESULT: {request.Path}, {request.Method}, {context.Response.StatusCode}");

        }

    }
}
