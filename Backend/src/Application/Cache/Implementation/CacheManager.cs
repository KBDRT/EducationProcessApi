using Application.Cache.Definition;
using Microsoft.Extensions.Logging;

namespace Application.Cache.Implementation
{
    // pattern fasade
    public class CacheManager : ICacheManager
    {
        private CacheManagerTypes _cacheType;
        private readonly ICacheRead _cacheReader;
        private readonly ICacheWrite _cacheWritter;
        private readonly ILogger _logger;

        public CacheManager(ICacheRead cacheReader, ICacheWrite cacheWritter, ILogger logger)
        {
            _cacheReader = cacheReader;
            _cacheWritter = cacheWritter;
            _logger = logger;
        }

        public void SetManagerType(CacheManagerTypes cacheType) => _cacheType = cacheType;

        public T? Get<T>(string key)
        {
            WriteToLog("GET FROM CACHE");
            return _cacheReader.Get<T>(key);
        }

        public bool TryGetValue<T>(string key, out T? value)
        {
            var result = _cacheReader.TryGetValue(key, out value);

            if (result)
            {
                WriteToLog("DATA GOT FROM CACHE");
            }

            return result;
        }


        public bool Set<T>(string key, T value, double seconds = 60)
        {
            bool result = _cacheWritter.Set(key, value, seconds);
            if (result)
            {
                WriteToLog("SUCCESS SET TO CACHE");
            }
            return result;
        }

        public void Remove(string key)
        {
            _cacheWritter.Remove(key);
            WriteToLog("REMOVE");
        }

        private void WriteToLog(string operationName)
        {
            _logger.LogInformation($"   * CACHE: {_cacheType}, ACTION: {operationName}");
        }

    }
}
