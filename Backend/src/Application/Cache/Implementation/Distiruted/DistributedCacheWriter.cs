using Application.Cache.Definition;
using Microsoft.Extensions.Caching.Distributed;
using System.Text.Json;

namespace Application.Cache.Implementation.Distributed
{
    public class DistributedCacheWriter : ICacheWrite
    {
        private readonly IDistributedCache _cache;

        public DistributedCacheWriter(IDistributedCache cache)
        {
            _cache = cache;
        }

        public void Remove(string key)
        {
            try
            {
                _cache.Remove(key);
            }
            catch (Exception ex)
            {

            }
        }

        public bool Set<T>(string key, T value, double seconds = 60)
        {
            if (seconds < 0)
            {
                throw new ArgumentOutOfRangeException();
            }

            try
            {
                var serializedValue = JsonSerializer.Serialize<T>(value);

                _cache.SetStringAsync(key, serializedValue, new DistributedCacheEntryOptions
                {
                    AbsoluteExpirationRelativeToNow = TimeSpan.FromSeconds(seconds)
                });

                return true;
            }
            catch
            {
                return false;
            }

        }
    }
}
