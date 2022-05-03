using MediatR;
using PenService.Domain.USAZCTAAggregates;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PenService.Application.USAZCTAAggregate.Queries
{
    public class NearByZCTAByRadius : IRequest<IEnumerable<USAZCTA>>
    {

        public NearByZCTAByRadius(int radius, int zcta)
        {
            Radius = radius;
            ZCTA = zcta;
        }
        public int Radius { get; }
        public int ZCTA { get; }
    }
}
