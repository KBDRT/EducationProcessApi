using Application.Cache.Implementation;

namespace Application.Cache.Definition
{
    public interface ICacheManagerFactory
    {
        CacheManager Create(CacheManagerTypes type);
    }
}