using MediatR;
using PenService.Domain.Core.Seed;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PenService.Domain.Core.MediatR
{
    public class RequestValidationException<TRequest, TResponse> : BaseException where TRequest : IRequest<TResponse>
    {
        public Dictionary<string, string[]> ErrorMessages { get; private set; }

        public RequestValidationException(ErrorCode errorCode, KeyValuePair<string, string[]>[] errors) : base(errorCode, $" Validation failed for the request {typeof(TRequest).FullName}.")
        {
            ErrorMessages = new Dictionary<string, string[]>();
            
        }

        public RequestValidationException(ErrorCode errorCode, Exception? innerException) : base(errorCode, $" Validation failed for the request {typeof(TRequest).FullName}.", innerException)
        {
            ErrorMessages = new Dictionary<string, string[]>();
        }
    }
}
