using PenService.Domain.USAZCTAAggregates;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PenService.Application.USAZCTAAggregate.Services
{
    public interface IUSAZCTAService
    {
        Task<IEnumerable<USAZCTA>> SearchAsync();
    }
}
