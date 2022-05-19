
using System;
using System.Collections.Generic;
using PinService.Domain.USAZCTAAggregates;

namespace PinService.Cache.USAZctaCaches;

public interface IUSAZctaCacheService
{

    Task<IEnumerable<USAZcta>?> FindAllAsync();
    Task<IEnumerable<USAZcta>?> FindByRadiusAsync(int zcta, int radius);


}