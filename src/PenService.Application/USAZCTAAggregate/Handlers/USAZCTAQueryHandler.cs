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
    public class USAZCTAQueryHandler : IRequestHandler<NearByZCTAByRadius, IEnumerable<USAZCTA>>
    {
        private readonly IUSAZCTAService service;

        public USAZCTAQueryHandler(IUSAZCTAService service)
        {
            this.service = service;
        }

        public async Task<IEnumerable<USAZCTA>> Handle(NearByZCTAByRadius request, CancellationToken cancellationToken)
        {
            var result = new List<USAZCTA>();
            var wholeZCTAs = await service.SearchAsync();

            var zip1 = wholeZCTAs.First(x => x.Zcta == request.ZCTA);

            foreach (var item in wholeZCTAs)
            {
                var distance = HaversineFormula.Distance(zip1.Latitude, zip1.Longitude, item.Latitude, item.Longitude);
                if (distance <= request.Radius) result.Add(item);
            }

            return result;
        }
    }
}
