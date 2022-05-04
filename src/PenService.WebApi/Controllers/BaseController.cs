using MediatR;
using Microsoft.AspNetCore.Mvc;
using PenService.Domain.Core;
using PenService.Domain.Core.MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static PenService.Domain.Core.ResponseBuilder;

namespace PenService.WebApi.Controllers
{
    [ApiController]
    [Route("api/[Controller]")]
    public class BaseController : ControllerBase
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
