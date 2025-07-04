using Application.Cache.Definition;
using Microsoft.Extensions.Caching.Memory;


namespace Application.Cache.Implementation.Memory
{
    public class MemoryCacheReader : ICacheRead
    {
        private readonly IMemoryCache _memoryCache;

        public MemoryCacheReader(IMemoryCache memoryCache)
        {
            _memoryCache = memoryCache;
        }

        public T? Get<T>(string key)
        {
            if (TryGetValue(key, out T? value))
            {
                return value;
            }
            else
            {
                return default;
            }
        }

        public bool TryGetValue<T>(string key, out T? value)
        {
            var result = _memoryCache.TryGetValue(key, out T? cacheValue);
            value = cacheValue;

            return result;
        }
    }
}
