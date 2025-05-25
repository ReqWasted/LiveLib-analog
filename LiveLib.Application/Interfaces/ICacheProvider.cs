namespace LiveLib.Application.Interfaces
{
    public interface ICacheProvider
    {
        Task<T?> ObjectGetAsync<T>(string key);
        Task SetAddAsync(string setKey, string value, TimeSpan? expiry = null);
        Task ObjectSetAsync<T>(string key, T value, TimeSpan? expiry = null);
        Task<string[]> SetGetAsync(string setKey);
        Task SetRemoveAsync(string setKey, string value);
        Task<string?> StringGetAsync(string key);
        Task RemoveAsync(string key);
        Task StringSetAsync(string key, string value, TimeSpan? expiry = null);
    }
}