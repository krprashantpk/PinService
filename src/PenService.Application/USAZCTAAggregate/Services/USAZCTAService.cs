using Microsoft.EntityFrameworkCore;
using PenService.Domain.USAZCTAAggregates;
using PenService.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PenService.Application.USAZCTAAggregate.Services
{
    public class USAZCTAService : IUSAZCTAService
    {
        private readonly PinServiceContext context;

        public USAZCTAService(PinServiceContext context)
        {
            this.context = context;
        }

        public async Task <IEnumerable<USAZCTA>> SearchAsync()
        {
            return await  context.USAZCTAs.ToListAsync();
        }
    }
}
