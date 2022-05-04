using FluentValidation;
using MediatR;
using Microsoft.Extensions.Logging;
using PenService.Domain.Core.MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PenService.Application.Behaviors
{
    public class RequestValidationBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse> where TRequest : IRequest<TResponse>
    {
        private readonly ILogger<RequestValidationBehavior<TRequest, TResponse>> logger;
        private readonly IEnumerable<IValidator<TRequest>> validators;

        public RequestValidationBehavior(ILogger<RequestValidationBehavior<TRequest, TResponse>> logger, IEnumerable<IValidator<TRequest>> validators)
        {
            this.logger = logger;
            this.validators = validators;
        }
        public async Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken, RequestHandlerDelegate<TResponse> next)
        {
            if (validators.Any())
            {
                var context = new ValidationContext<TRequest>(request);

                var errors = validators.Select(x => x.Validate(context))
                    .Where(result => result.Errors != null)
                    .SelectMany(result => result.Errors)
                    .Select(x => x.ErrorMessage)
                    .Distinct();

                if (errors != null && errors.Any()) throw new RequestValidationException<TRequest, TResponse>(Domain.Core.Seed.ErrorCode.RequestValidationFailed, errors.ToArray());
            }
            else
            {
                logger.LogWarning($"Validator not found of the Type {typeof(TRequest).FullName}");
            }
            return await next().ConfigureAwait(false);
        }
    }
}
