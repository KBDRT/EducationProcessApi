namespace Application.Cache.Definition
{
    public interface ICacheRead
    {
        T? Get<T>(string key);
        bool TryGetValue<T>(string key, out T? value);
    }
}
