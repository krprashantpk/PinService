using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PinService.Cache.USAZctaCaches;

namespace PinService.Cache;

public static class Extensions
{

    public static IServiceCollection AddCacheServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddSingleton<IUSAZctaCacheService, USAZctaCacheService>();
        services.AddSingleton<IRedisConnection, RedisConnection>((services) => new RedisConnection(configuration));



        return services;
    }
}
