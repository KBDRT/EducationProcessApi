using CSharpFunctionalExtensions;
using Microsoft.AspNetCore.Http;

namespace Application
{
    public static class Extensions
    {
        public static Result<Guid> CheckGuidForEmpty(this Guid id)
        {
            if (id == Guid.Empty)
            {
                return Result.Failure<Guid>("Error");
            }
            else
            {
                return Result.Success(id);
            }
        }

        public static int GetStatusCodeForController(this ServiceResultCode resultCode)
        {
            return resultCode switch
            {
                ServiceResultCode.Success => StatusCodes.Status200OK,
                _ => StatusCodes.Status400BadRequest,
            };
        }


    }
}
