using Microsoft.AspNetCore.Mvc;
using PinService.Application.USAZCTAAggregate.Dtos;
using PinService.Application.USAZCTAAggregate.Queries;
using static PinService.Domain.Core.ResponseBuilder;

namespace PinService.WebApi.Controllers
{
    public class USAZctaController : BaseController
    {


        [HttpGet()]
        public async Task<ActionResult<PinServiceResponse<IEnumerable<USAZctaDto>>>> GET([FromQuery] uint Radius, string Zcta)
        {
            return await SendAsync<SearchNearByZctasByRadius, IEnumerable<USAZctaDto>>(new SearchNearByZctasByRadius(Radius, Zcta));
        }

    }
}
