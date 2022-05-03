using MediatR;
using PenService.Application.USAZCTAAggregate.Queries;
using PenService.Application.USAZCTAAggregate.Services;
using PenService.Distance;
using PenService.Domain.USAZCTAAggregates;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PenService.Application.USAZCTAAggregate.Handlers
{
    public class USAZctaQueryHandler : IRequestHandler<SearchNearByZctasByRadius, IEnumerable<USAZcta>>
    {
        private readonly IUSAZctaService service;

        public USAZctaQueryHandler(IUSAZctaService service)
        {
            this.service = service;
        }
        public async Task<IEnumerable<USAZcta>> Handle(SearchNearByZctasByRadius request, CancellationToken cancellationToken)
        {

            //TODO:: Add Cache, HandleValidation for NonExistance PIN.
            var result = new List<USAZcta>();
            var wholeZCTAs = await service.SearchAsync();
            var zip1 = wholeZCTAs.First(x => x.Zcta == int.Parse(request.Zcta!));
            foreach (var item in wholeZCTAs)
            {
                var distance = HaversineFormula.Distance(zip1.Latitude, zip1.Longitude, item.Latitude, item.Longitude);
                if (distance <= request.Radius) result.Add(item);
            }

            return result;
        }
    }
}
