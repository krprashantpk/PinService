
using System;
using System.Collections.Generic;
using PinService.Domain.USAZCTAAggregates;
using StackExchange.Redis;
using StackExchange.Redis.Configuration;

namespace PinService.Cache;

interface IRedisConnection
{
    IDatabase Db { get; }
}