using MediatR;
using Microsoft.AspNetCore.Mvc;
using PenService.Application.USAZCTAAggregate.Queries;
using PenService.Domain.USAZCTAAggregates;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static PenService.Domain.Core.ResponseBuilder;

namespace PenService.WebApi.Controllers
{
    public class USAZctaController : BaseController
    {
        private readonly IMediator mediator;

        public USAZctaController(IMediator mediator)
        {
            this.mediator = mediator;
        }

        [HttpGet()]
        public async Task<ActionResult<PinServiceResponse<IEnumerable<USAZcta>>>> GET([FromQuery] uint Radius, string Zcta)
        {
            return await SendAsync<SearchNearByZctasByRadius, IEnumerable<USAZcta>>(new SearchNearByZctasByRadius(Radius, Zcta));
        }

    }
}
