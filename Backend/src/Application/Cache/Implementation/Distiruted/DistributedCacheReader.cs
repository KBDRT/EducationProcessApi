using Application.Cache.Definition;
using Microsoft.Extensions.Caching.Distributed;
using System.Text.Json;


namespace Application.Cache.Implementation.Distributed
{
    public class DistributedCacheReader : ICacheRead
    {
        private readonly IDistributedCache _cache;

        public DistributedCacheReader(IDistributedCache cache)
        {
            _cache = cache;
        }

        public T? Get<T>(string key)
        {
            var result = TryGetValue<T>(key, out T? value);

            if (result)
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
            try
            {
                var serializedValue = _cache.GetString(key);

                if (serializedValue == null)
                {
                    value = default(T);
                    return false;
                }

                value = JsonSerializer.Deserialize<T>(serializedValue);
                return true;
            }
            catch (Exception ex)
            {
                value = default(T);
                return false;
            }
        }
    }
}
