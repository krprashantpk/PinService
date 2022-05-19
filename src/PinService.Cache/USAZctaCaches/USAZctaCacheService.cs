
using System;
using System.Collections.Generic;
using PinService.Domain.Core.Converters;
using PinService.Domain.USAZCTAAggregates;
using StackExchange.Redis;
namespace PinService.Cache.USAZctaCaches;

class USAZctaCacheService : IUSAZctaCacheService
{

    private readonly IRedisConnection cache;
    public USAZctaCacheService(IRedisConnection cache)
    {
        this.cache = cache;
    }


    public async Task SetAllAsync(IEnumerable<USAZcta> Zctas)
    {
        var values = JsonConverter.Serialize<IEnumerable<USAZcta>>(Zctas);
        await cache.Db.StringSetAsync(USAZctaCachekeys.EveryZcta, value: values, TimeSpan.MaxValue, when: When.Always);
    }

    public async Task SetByRadiusAsync(IEnumerable<USAZcta> Zctas, int zcta, int radius)
    {
        var values = JsonConverter.Serialize<IEnumerable<USAZcta>>(Zctas);
        await cache.Db.StringSetAsync(USAZctaCachekeys.NearByRadius(zcta, radius), value: values, TimeSpan.MaxValue, when: When.Always);
    }
    public async Task<IEnumerable<USAZcta>?> FindAllAsync()
    {
        var result = await cache.Db.StringGetAsync(USAZctaCachekeys.EveryZcta);
        if (result.HasValue)
        {
            return JsonConverter.Deserialize<IEnumerable<USAZcta>>(result.ToString());
        }
        return null;
    }
    public async Task<IEnumerable<USAZcta>?> FindByRadiusAsync(int zcta, int radius)
    {
        var result = await cache.Db.StringGetAsync(USAZctaCachekeys.NearByRadius(zcta, radius));
        if (result.HasValue)
        {
            return JsonConverter.Deserialize<IEnumerable<USAZcta>>(result.ToString());
        }
        return null;
    }



}