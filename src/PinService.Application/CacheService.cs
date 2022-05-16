using Microsoft.Extensions.Caching.Memory;
using PinService.Domain.Core.Interfaces;

namespace PinService.Application
{
    public class CacheService : ICacheService
    {
        private readonly IMemoryCache cache;

        public CacheService(IMemoryCache cache)
        {
            this.cache = cache;
        }

        public string Key(params string[] inputs)
        {
            return inputs.Aggregate((x, y) => $"{x}_{y}");
        }

        public async Task<T> SetValueAsync<T>(string key, Func<Task<T>> action)
        {
            var value = await action();
            cache.Set(key, value, new MemoryCacheEntryOptions() { AbsoluteExpiration = DateTime.MaxValue });
            return value;
        }

        public async Task<T> CreateOrGetValueAsync<T>(string key, Func<Task<T>> action)
        {
            T value = default(T);
            if (!TryGetValue(key, out value))
            {
                value = await action();
                if (value == null) throw new ArgumentNullException(nameof(value));
                cache.Set<T>(key, value, new MemoryCacheEntryOptions() { AbsoluteExpiration = DateTime.MaxValue });
            }
            return value!;
        }

        public bool TryGetValue<T>(string key, out T value)
        {
            cache.TryGetValue(key, out value);
            return (value != null);
        }
    }
}
