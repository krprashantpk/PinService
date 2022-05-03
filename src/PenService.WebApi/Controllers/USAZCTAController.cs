using MediatR;
using Microsoft.AspNetCore.Mvc;
using PenService.Application.USAZCTAAggregate.Queries;
using PenService.Domain.USAZCTAAggregates;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PenService.WebApi.Controllers
{
    public class USAZCTAController : BaseController
    {
        private readonly IMediator mediator;

        public USAZCTAController(IMediator mediator)
        {
            this.mediator = mediator;
        }

        [HttpGet]

        public async Task<IActionResult> GET(int pin, int radius)
        {
            var reuslt = await mediator.Send<IEnumerable<USAZCTA>>(new NearByZCTAByRadius(radius, pin));
            return Ok(reuslt);
        }

    }
}
