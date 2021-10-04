namespace SaudiExpress.Business.Models
{
    public class ResponseModel
    {
        public ResponseModel()
        {

        }
        public ResponseModel(ResponseStatus responseStatus, string message)
        {
            Status = responseStatus;
            Message = message;
        }

        public ResponseStatus Status { get; set; } = ResponseStatus.Succeeded;
        public string Message { get; }
    }
    public class ResponseModel<T> : ResponseModel
    {
        public ResponseModel(T result)
        {
            Result = result;
            Status = ResponseStatus.Succeeded;
        }
        public ResponseModel(T result, ResponseStatus responseStatus)
        {
            Result = result;
            Status = responseStatus;
        }
        public ResponseModel(T result, ResponseStatus responseStatus, string message)
        {
            Result = result;
            Status = responseStatus;
        }
        public T Result { get; set; }
    }
    public enum ResponseStatus
    {
        Unauthorized,
        Succeeded,
        BadRequest,
        NotFound,
        Failed
    }

}
