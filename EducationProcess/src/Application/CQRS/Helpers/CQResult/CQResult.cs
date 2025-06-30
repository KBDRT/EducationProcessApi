using FluentValidation.Results;
using Microsoft.AspNetCore.Http;

namespace Application.CQRS.Helpers.CQResult
{
    public class CQResult
    {
        public List<CQResultValidation> Messages { get; private set; } = [];

        private CQResultStatusCode _resultCode { get; set; } = CQResultStatusCode.None;

        public CQResultStatusCode ResultCode
        {
            get
            {
                if (_resultCode != CQResultStatusCode.None)
                {
                    return _resultCode;
                }

                var messages = Messages.FirstOrDefault(x => x.StatusCode == CQResultValidationStatusCode.Error);
                if (messages == null)
                {
                    return CQResultStatusCode.Success;
                }
                else
                {
                    return CQResultStatusCode.Error;
                }
            }
        }

        public void SetResultCode(CQResultStatusCode resultCode)
        {
            _resultCode = resultCode;
        }


        public CQResult(ValidationResult validationResult)
        {
            AddMessages(validationResult);
        }

        public CQResult()
        {

        }

        public void AddMessages(ValidationResult validationResult)
        {
            foreach (var item in validationResult.Errors)
            {
                CQResultValidation result = new()
                {
                    Message = item.ErrorMessage,
                    FieldName = item.PropertyName,
                };

                Messages.Add(result);
            }
        }

        public void AddMessage(string message, string fieldName)
        {
            CQResultValidation result = new()
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
                CQResultStatusCode.Success => StatusCodes.Status200OK,
                _ => StatusCodes.Status400BadRequest,
            };
        }

    }
}
