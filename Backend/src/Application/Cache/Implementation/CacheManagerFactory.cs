
using Application.Cache.Definition;
using Application.Cache.Implementation.Distributed;
using Application.Cache.Implementation.Memory;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;

namespace Application.Cache.Implementation
{
    public enum CacheManagerTypes
    {
        Memory,
        DistibutedRedis
    }

    public static class CackePrefixKeyConstants
    {
        public const string LESSONS_FOR_GROUP = "LESSONS_FOR_GROUP";

        public const string TEACHERS_FOR_YEAR = "TEACHERS_FOR_YEAR";
    }



    public class CacheManagerFactory : ICacheManagerFactory
    {
        private readonly IMemoryCache _memoryCache;
        private readonly IDistributedCache _redisCache;
        private readonly ILogger _logger;

        public CacheManagerFactory(IMemoryCache memoryCache,
                                   IDistributedCache redisCache,
                                   ILogger<CacheManagerFactory> logger)
        {
            _memoryCache = memoryCache;
            _redisCache = redisCache;
            _logger = logger;
        }

        public ICacheManager Create(CacheManagerTypes type)
        {
            CacheManager? manager = null;
            switch (type)
            {
                case CacheManagerTypes.Memory:
                    manager = new (new MemoryCacheReader(_memoryCache),
                                   new MemoryCacheWriter(_memoryCache),
                                   _logger);
                    break;
                case CacheManagerTypes.DistibutedRedis:
                    manager = new (new DistributedCacheReader(_redisCache),
                                   new DistributedCacheWriter(_redisCache),
                                   _logger);
                    break;
                default:
                    throw new ArgumentException("Unsupported cache type");
            }

            manager.SetManagerType(type);

            return manager;
        }

    }
}
