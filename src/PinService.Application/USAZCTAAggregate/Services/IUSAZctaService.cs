using System.Collections.Generic;
using PinService.Domain.USAZCTAAggregates;


namespace PinService.Application.USAZCTAAggregate.Services;

interface IUSAZctaService
{
    Task<IEnumerable<USAZcta>> FindAllAsync();
    Task<IEnumerable<USAZcta>> FindByRadiusAsync(int zcta, int radius);

}