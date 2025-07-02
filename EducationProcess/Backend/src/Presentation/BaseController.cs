using Application;
using Application.CQRS.Result.CQResult;
using Microsoft.AspNetCore.Mvc;

namespace Presentation
{
    public class BaseController : ControllerBase
    {
        [NonAction]
        public JsonResult FormResultFromService(object? resultData, List<ServiceResult> message, ServiceResultCode resultCode)
        {
            int resultCodeRequest = GetStatusCodeForController(resultCode);    

            return new JsonResult(resultCodeRequest == 200 ? resultData : message)
            {
                StatusCode = resultCodeRequest
            };
        }

        [NonAction]
        public JsonResult FormResultFromService(ServiceResultManager result)
        {
            int resultCodeRequest = result.GetStatusCodeForController();

            return new JsonResult(resultCodeRequest == 200 ? null : result.Messages)
            {
                StatusCode = resultCodeRequest
            };
        }

        [NonAction]
        public JsonResult FormResultFromService<T>(ServiceResultManager<T> result)
        {
            int resultCodeRequest = result.GetStatusCodeForController();

            return new JsonResult(resultCodeRequest == 200 ? result.ResultData : result.Messages)
            {
                StatusCode = resultCodeRequest
            };
        }

        [NonAction]
        public JsonResult FormResultFromService(CQResult result)
        {
            int resultCodeRequest = result.GetStatusCodeForController();

            return new JsonResult(resultCodeRequest == 200 ? null : result.Messages)
            {
                StatusCode = resultCodeRequest
            };
        }

        [NonAction]
        public JsonResult FormResultFromService<T>(CQResult<T> result)
        {
            int resultCodeRequest = result.GetStatusCodeForController();

            return new JsonResult(resultCodeRequest == 200 ? result.ResultData : result.Messages)
            {
                StatusCode = resultCodeRequest
            };
        }

        private int GetStatusCodeForController(ServiceResultCode resultCode)
        {
            return resultCode switch
            {
                ServiceResultCode.Success => StatusCodes.Status200OK,
                _ => StatusCodes.Status400BadRequest,
            };
        }

    }
}
