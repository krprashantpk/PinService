using MediatR;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PenService.WebApi.Controllers
{
    [ApiController]
    [Route("api/[Controller]")]
    public class BaseController : ControllerBase
    {
        private IMediator? _mediator;
        protected IMediator Mediator => _mediator ??= HttpContext.RequestServices.GetService<IMediator>() ?? throw new ArgumentNullException();



        public async Task<IActionResult> SendAsync<TRequest, TResponse>(TRequest request) where TRequest : IRequest<TResponse>
        {
            return Ok(await Mediator.Send<TResponse>(request));
        }
    }
}
