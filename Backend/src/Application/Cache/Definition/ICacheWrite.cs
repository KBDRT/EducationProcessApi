namespace Application.Cache.Definition
{
    public interface ICacheWrite
    {
        bool Set<T>(string key, T value, double seconds = 60);
        void Remove(string key);
    }
}
