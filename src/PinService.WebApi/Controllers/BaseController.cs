using MediatR;
using Microsoft.AspNetCore.Mvc;
using PinService.Domain.Core;
using PinService.Domain.Core.MediatR;
using static PinService.Domain.Core.ResponseBuilder;

namespace PinService.WebApi.Controllers
{
    [ApiController]
    [Route("api/[Controller]")]
    public abstract class BaseController : ControllerBase
    {
        private IMediator? _mediator;
        protected IMediator Mediator => _mediator ??= HttpContext.RequestServices.GetService<IMediator>() ?? throw new ArgumentNullException();
        public async Task<ActionResult<PinServiceResponse<TResponse>>> SendAsync<TRequest, TResponse>(TRequest request) where TRequest : IRequest<TResponse>
        {
            try
            {
                return Ok(ResponseBuilder.Ok<TResponse>(await Mediator.Send<TResponse>(request)));
            }
            catch (RequestValidationException e)
            {
                return BadRequest(ResponseBuilder.Fail<TResponse>(e.Errors));
            }
            catch (Exception e)
            {
                return BadRequest(ResponseBuilder.Fail<TResponse>(e.Message));
            }

        }
    }
}
