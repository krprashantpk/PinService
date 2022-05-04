using MediatR;
using PenService.Domain.Core.Seed;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PenService.Domain.Core.MediatR
{

    public abstract class RequestValidationException : BaseException
    {
        public abstract string[] Errors { get;  }

        protected RequestValidationException(ErrorCode errorCode, string message) : base(errorCode, message)
        {

        }
    }



    public class RequestValidationException<TRequest, TResponse> : RequestValidationException where TRequest : IRequest<TResponse>
    {
        private string[]? _errors;
        public RequestValidationException(ErrorCode errorCode, string[] errors) : base(errorCode, $" Validation failed for the request {typeof(TRequest).FullName}.")
        {
            if (errors == null || !errors.Any()) throw new ArgumentNullException(nameof(errors));
            _errors = errors;

        }
        public override string[] Errors => _errors!;
    }
}
