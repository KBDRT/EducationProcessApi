using FluentValidation.Results;
using Microsoft.AspNetCore.Http;

namespace Application.CQRS.Helpers.CQResult
{
    public class CQResult<T> : CQResult
    {
        public T? ResultData { get; private set; }

        public CQResult(T result, ValidationResult validationResult) : base(validationResult)
        {
            ResultData = result;
        }

        public CQResult(T result) : base()
        {
            ResultData = result;
        }

        public CQResult() : base()
        {
        }

        public CQResult(ValidationResult validationResult) : base(validationResult)
        {

        }

        public void SetResultData(T? data)
        {
            ResultData = data;
        }

        public new int GetStatusCodeForController()
        {
            if (ResultData == null)
            {
                return StatusCodes.Status404NotFound;
            }

            return ResultCode switch
            {
                CQResultStatusCode.Success => StatusCodes.Status200OK,
                _ => StatusCodes.Status400BadRequest,
            };
        }



    }
}
