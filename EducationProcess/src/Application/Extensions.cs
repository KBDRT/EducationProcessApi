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




    }
}
