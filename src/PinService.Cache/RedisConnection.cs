using System;
using System.Collections.Generic;
using Microsoft.Extensions.Configuration;
using PinService.Domain.USAZCTAAggregates;
using StackExchange.Redis;
using StackExchange.Redis.Configuration;

namespace PinService.Cache;
class RedisConnection : IRedisConnection
{
    IDatabase? _database = null;

    public RedisConnection(IConfiguration configuration)
    {
        var options = ConfigurationOptions.Parse(configuration["RedisConfiguration"]);
        _database = ConnectionMultiplexer.Connect(options)
        .GetDatabase();
    }

    public IDatabase Db => _database ?? throw new NotImplementedException();
}