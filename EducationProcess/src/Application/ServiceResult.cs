using FluentValidation.Results;
using Microsoft.AspNetCore.Http;
using System.Web.Mvc;

namespace Application
{

    public enum ServiceResultCode
    {
        Success,
        Error
    }

    public enum ValidationFieldResult
    {
        Success,
        Error
    }


    public class ServiceResult
    {
        public string Message { get; set; } = string.Empty;
        public string FieldName { get; set; }  = string.Empty;
        public ValidationFieldResult StatusCode { get; set; } = ValidationFieldResult.Error;
    }

    public class ServiceResultManager
    {
        public List<ServiceResult> Messages { get; private set; } = new List<ServiceResult>();

        public ServiceResultCode ResultCode
        {
            get
            {
                var messages = Messages.FirstOrDefault(x => x.StatusCode == ValidationFieldResult.Error);
                if (messages == null)
                {
                    return ServiceResultCode.Success;
                }
                else
                {
                    return ServiceResultCode.Error;
                }
            }
        }

        public ServiceResultManager(ValidationResult validationResult)
        {
            AddMessages(validationResult);
        }

        public ServiceResultManager()
        {

        }

        public void AddMessages(ValidationResult validationResult)
        {
            foreach (var item in validationResult.Errors)
            {
                ServiceResult result = new()
                {
                    Message = item.ErrorMessage,
                    FieldName = item.PropertyName,
                };

                Messages.Add(result);
            }
        }

        public void AddMessage(string message, string fieldName)
        {
            ServiceResult result = new()
            {
                Message = message,
                FieldName = fieldName
            };
            Messages.Add(result);
        }


        public int GetStatusCodeForController()
        {
            return ResultCode switch
            {
                ServiceResultCode.Success => StatusCodes.Status200OK,
                _ => StatusCodes.Status400BadRequest,
            };
        }

    }

    public class ServiceResultManager<T> : ServiceResultManager
    {
        public T? ResultData { get; private set; }

        public ServiceResultManager(T result, ValidationResult validationResult) : base(validationResult)
        {
            ResultData = result;
        }

        public ServiceResultManager(T result) : base()
        {
            ResultData = result;
        }

        public ServiceResultManager() : base()
        {
        }

        public ServiceResultManager(ValidationResult validationResult) : base(validationResult)
        {

        }

        public void SetResultData(T? data)
        {
            ResultData = data;
        }

    }
}
