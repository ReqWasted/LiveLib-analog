namespace LiveLib.Application.Interfaces
{
    public interface ICacheProvider
    {
        Task<T?> ObjectGetAsync<T>(string key, CancellationToken ct);
        Task SetAddAsync(string setKey, string value, CancellationToken ct, TimeSpan? expiry = null);
        Task ObjectSetAsync<T>(string key, T value, CancellationToken ct, TimeSpan? expiry = null);
        Task<string[]> SetGetAsync(string setKey, CancellationToken ct);
        Task SetRemoveAsync(string setKey, string value, CancellationToken ct);
        Task<string?> StringGetAsync(string key, CancellationToken ct);
        Task RemoveAsync(string key, CancellationToken ct);
        Task StringSetAsync(string key, string value, CancellationToken ct, TimeSpan? expiry = null);
        Task BytesSetAsync(string key, byte[] value, CancellationToken ct, TimeSpan? expiry = null);
        Task<byte[]?> BytesGetAsync(string key, CancellationToken ct);
    }
}