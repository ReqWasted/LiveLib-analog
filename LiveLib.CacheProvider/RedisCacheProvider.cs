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

		public async Task<string?> StringGetAsync(string key)
		{
			return await _redis.StringGetAsync(key);
		}

		public async Task StringSetAsync(string key, string value, TimeSpan? expiry = null)
		{
			await _redis.StringSetAsync(key, value, expiry);
		}
		public async Task BytesSetAsync(string key, byte[] value, TimeSpan? expiry = null)
		{
			await _redis.StringSetAsync(key, value, expiry);
		}

		public async Task<byte[]> BytesGetAsync(string key)
		{
			return await _redis.StringGetAsync(key);
		}


		public async Task RemoveAsync(string key)
		{
			await _redis.KeyDeleteAsync(key);
		}

		public async Task<string[]> SetGetAsync(string setKey)
		{
			var data = await _redis.SetMembersAsync(setKey);
			return data.Select(d => d.ToString()).ToArray();
		}

		public async Task SetAddAsync(string setKey, string value, TimeSpan? expiry = null)
		{
			await _redis.SetAddAsync(setKey, value);
			await _redis.KeyExpireAsync(setKey, expiry);
		}

		public async Task SetRemoveAsync(string setKey, string value)
		{
			await _redis.SetRemoveAsync(setKey, value);
		}

		public async Task ObjectSetAsync<T>(string key, T value, TimeSpan? expiry = null)
		{
			var json = JsonSerializer.Serialize(value);
			await _redis.StringSetAsync(key, json, expiry);
		}

		public async Task<T?> ObjectGetAsync<T>(string key)
		{
			var value = await _redis.StringGetAsync(key);
			if (value.IsNullOrEmpty) return default;
			return JsonSerializer.Deserialize<T>(value);
		}

	}
}
