using Application.Cache.Definition;
using Microsoft.Extensions.Caching.Memory;

namespace Application.Cache.Implementation.Memory
{
    public class MemoryCacheWriter : ICacheWrite
    {
        private readonly IMemoryCache _memoryCache;

        public MemoryCacheWriter(IMemoryCache memoryCache)
        {
            _memoryCache = memoryCache;
        }


        public void Remove(string key)
        {
            _memoryCache.Remove(key);
        }

        public bool Set<T>(string key, T value, double seconds = 60)
        {
            var cacheOptions = new MemoryCacheEntryOptions()
            {
                AbsoluteExpirationRelativeToNow = TimeSpan.FromSeconds(seconds),
            };

            _memoryCache.Set(key, value, cacheOptions);

            return true;
        }

    }
}
