using Application.Exceptions;

namespace Presentation.Middleware
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionMiddleware> _logger;

        public ExceptionMiddleware(RequestDelegate next, ILogger<ExceptionMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try 
            {
                await _next.Invoke(context); 
            }
            catch (ValidatorFactoryException ex) 
            {
                _logger.LogError($"{ex.Message}, Unknown validator for type: {ex.ValidatorType?.ToString()}, Sourse: {ex.Source}, MethodName: {ex.TargetSite}");
                _logger.LogError($" STACK_TRACE: \n {ex}");

                context.Response.StatusCode = 500;
            }
            catch (Exception ex) 
            {
                _logger.LogError($"EXCEPTION: {ex.Message}, Sourse: {ex.Source}, MethodName: {ex.TargetSite}");
                _logger.LogError($" STACK_TRACE: \n {ex}");

                context.Response.StatusCode = 500;
            }
        }
    }
}
