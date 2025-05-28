using System.Text.Json;
using LiveLib.Application.Interfaces;
using StackExchange.Redis;

namespace LiveLib.CacheProvider
{
    public class RedisCacheProvider : ICacheProvider
    {
        private readonly IDatabase _redis;

        public RedisCacheProvider(IConnectionMultiplexer connectionMultiplexer)
        {
            _redis = connectionMultiplexer.GetDatabase();
        }

        public async Task<string?> StringGetAsync(string key, CancellationToken ct)
        {
            return await _redis.StringGetAsync(key).WaitAsync(ct);
        }

        public async Task StringSetAsync(string key, string value, CancellationToken ct, TimeSpan? expiry = null)
        {
            await _redis.StringSetAsync(key, value, expiry).WaitAsync(ct);
        }
        public async Task BytesSetAsync(string key, byte[] value, CancellationToken ct, TimeSpan? expiry = null)
        {
            await _redis.StringSetAsync(key, value, expiry).WaitAsync(ct);
        }

        public async Task<byte[]?> BytesGetAsync(string key, CancellationToken ct)
        {
            return await _redis.StringGetAsync(key).WaitAsync(ct);
        }


        public async Task RemoveAsync(string key, CancellationToken ct)
        {
            await _redis.KeyDeleteAsync(key).WaitAsync(ct);
        }

        public async Task<string[]> SetGetAsync(string setKey, CancellationToken ct)
        {
            var data = await _redis.SetMembersAsync(setKey).WaitAsync(ct);
            return data.Select(d => d.ToString()).ToArray();
        }

        public async Task SetAddAsync(string setKey, string value, CancellationToken ct, TimeSpan? expiry = null)
        {
            await _redis.SetAddAsync(setKey, value).WaitAsync(ct);
            await _redis.KeyExpireAsync(setKey, expiry).WaitAsync(ct);
        }

        public async Task SetRemoveAsync(string setKey, string value, CancellationToken ct)
        {
            await _redis.SetRemoveAsync(setKey, value).WaitAsync(ct);
        }

        public async Task ObjectSetAsync<T>(string key, T value, CancellationToken ct, TimeSpan? expiry = null)
        {
            var json = JsonSerializer.Serialize(value);
            await _redis.StringSetAsync(key, json, expiry).WaitAsync(ct);
        }

        public async Task<T?> ObjectGetAsync<T>(string key, CancellationToken ct)
        {
            var value = await _redis.StringGetAsync(key).WaitAsync(ct);
            if (value.IsNullOrEmpty) return default;
            return JsonSerializer.Deserialize<T>(value);
        }

    }
}
