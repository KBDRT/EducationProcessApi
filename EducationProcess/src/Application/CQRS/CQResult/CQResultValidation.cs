namespace Application.CQRS.Result.CQResult
{
    public class CQResultValidation
    {
        public string Message { get; set; } = string.Empty;
        public string FieldName { get; set; }  = string.Empty;
        public CQResultValidationStatusCode StatusCode { get; set; } = CQResultValidationStatusCode.Error;
    }
}
