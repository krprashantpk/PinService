using MediatR;
using PinService.Domain.Core.Seed;

namespace PinService.Domain.Core.MediatR
{

    public abstract class RequestValidationException : BaseException
    {
        public abstract string[] Errors { get; }

        protected RequestValidationException(ErrorCode errorCode, string message) : base(errorCode, message)
        {

        }
    }

    public class RequestValidationException<TRequest, TResponse> : RequestValidationException where TRequest : IRequest<TResponse>
    {
        private string[]? _errors;

        public RequestValidationException(ErrorCode errorCode, string message) : base(errorCode, message)
        {
            _errors = new string[] { message };
        }
        public RequestValidationException(ErrorCode errorCode, string[] errors) : base(errorCode, $" Validation failed for the request {typeof(TRequest).FullName}.")
        {
            if (errors == null || !errors.Any()) throw new ArgumentNullException(nameof(errors));
            _errors = errors;

        }
        public override string[] Errors => _errors!;
    }
}
