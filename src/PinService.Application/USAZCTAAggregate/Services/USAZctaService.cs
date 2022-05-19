using System.Collections.Generic;
using Microsoft.Extensions.Logging;
using PinService.Distance;
using PinService.Domain.Core.Interfaces;
using PinService.Domain.Core.Seed;
using PinService.Domain.USAZCTAAggregates;


namespace PinService.Application.USAZCTAAggregate.Services;

class USAZctaService : IUSAZctaService
{
    private readonly IGenericRepository repository;
    private readonly ILogger<USAZctaService> logger;

    public USAZctaService(IGenericRepository repository, ILogger<USAZctaService> logger)
    {
        this.repository = repository;
        this.logger = logger;
    }
    public async Task<IEnumerable<USAZcta>> FindAllAsync()
    {
        return await repository.SearchAsync<USAZcta>();
    }

    public async Task<IEnumerable<USAZcta>> FindByRadiusAsync(int zcta, int radius)
    {
        var wholeZCTAs = await repository.SearchAsync<USAZcta>();
        var zip1 = wholeZCTAs.SingleOrDefault(x => x.Zcta == zcta);

        if (zip1 == null)
        {
            logger.LogWarning($"Entered USA Zcta {zcta} doesn't exit.");
            //TODO: CREATE DIFFERENT EXCEPTION FOR DIFFERENT BSUINESS SCENARIO
            throw new Exception("Doesn't exit.");
        }

        var isWithInRadius = (USAZcta item) => HaversineFormula.Distance(zip1.Latitude, zip1.Longitude, item.Latitude, item.Longitude) <= radius;

        return wholeZCTAs.Where(item => isWithInRadius(item));
    }
}