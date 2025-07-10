using Application.Cache.Implementation;

namespace Application.Cache.Definition
{
    public interface ICacheManagerFactory
    {
        ICacheManager Create(CacheManagerTypes type);
    }
}