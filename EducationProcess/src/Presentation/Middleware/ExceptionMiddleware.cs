using Application.Exceptions;
using System.Linq.Expressions;

namespace Presentation.Middleware
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate next;

        public ExceptionMiddleware(RequestDelegate next)
        {
            this.next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try 
            { 
                await next.Invoke(context); 
            }
            catch (ValidatorFactoryException ex) 
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"{ex.Message}, Unknown validator for type: {ex.ValidatorType?.ToString()}");
                Console.ResetColor();
                Console.WriteLine(ex);

                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"-------------------------");
                Console.ResetColor();

                context.Response.StatusCode = 500;
            }
            catch
            {

            }
        }
    }
}
