namespace PinService.Domain.Core
{
    public class ResponseBuilder
    {
        public static PinServiceResponse<T> Ok<T>(T data) => new PinServiceResponse<T> { Data = data, Success = true };
        public static PinServiceResponse<T> Ok<T>(T data, string message) => new PinServiceResponse<T> { Data = data, Success = true, Message = message };


        public static PinServiceResponse<object> Fail(string[] errors) => new PinServiceResponse<object> { Success = false, Error = errors };
        public static PinServiceResponse<object> Fail(string message) => new PinServiceResponse<object> { Success = false, Message = message };
        public static PinServiceResponse<object> Fail(string message, string[] errors) => new PinServiceResponse<object> { Success = false, Message = message, Error = errors };


        public static PinServiceResponse<T> Fail<T>(string[] errors) => new PinServiceResponse<T> { Success = false, Error = errors };
        public static PinServiceResponse<T> Fail<T>(string message) => new PinServiceResponse<T> { Success = false, Message = message };
        public static PinServiceResponse<T> Fail<T>(string message, string[] errors) => new PinServiceResponse<T> { Success = false, Message = message, Error = errors };

        public class PinServiceResponse<T>
        {
            public PinServiceResponse()
            {
                ResponseUTCDateTm = DateTime.UtcNow;
            }
            public T? Data { get; internal set; }
            public bool Success { get; internal set; }
            public DateTime ResponseUTCDateTm { get; internal set; }
            public string[]? Error { get; internal set; }
            public string? Message { get; internal set; }
        }
    }
}
