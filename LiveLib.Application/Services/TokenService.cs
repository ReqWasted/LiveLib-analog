using System.Runtime.CompilerServices;
using System.Text.Json;
using LiveLib.Application.Interfaces;
using LiveLib.Domain.Models;

namespace LiveLib.Application.Services
{
    public class TokenService : ITokenService
    {
        private readonly ICacheProvider _cache;

        public TokenService(ICacheProvider cache)
        {
            _cache = cache;
        }

        public async Task AddRefreshTokenAsync(RefreshToken refreshToken, CancellationToken ct)
        {
            var expiration = refreshToken.ExpiresAt - DateTime.UtcNow;
            await _cache.ObjectSetAsync($"token:{refreshToken.Token}", refreshToken, ct, expiration);
            await _cache.SetAddAsync($"user:{refreshToken.UserId}:tokens", refreshToken.Token, ct, expiration);
        }

        public async Task<RefreshToken?> GetActiveTokenAsync(string userRefreshToken, CancellationToken ct)
        {
            var tokenString = await _cache.StringGetAsync($"token:{userRefreshToken}", ct);
            if (string.IsNullOrEmpty(tokenString)) return null;
            return JsonSerializer.Deserialize<RefreshToken>(tokenString);
        }

        public async Task RevokeTokenAsync(RefreshToken refreshToken, CancellationToken ct)
        {
            await _cache.RemoveAsync($"token:{refreshToken.Token}", ct);
            await _cache.SetRemoveAsync($"user:{refreshToken.UserId}:tokens", refreshToken.Token, ct);
        }

        public async IAsyncEnumerable<RefreshToken> GetActiveTokensByUserIdAsync(Guid userId, [EnumeratorCancellation] CancellationToken ct)
        {
            var tokens = await _cache.SetGetAsync($"user:{userId}:tokens", ct);
            foreach (var token in tokens)
            {
                ct.ThrowIfCancellationRequested();
                var tokenString = await _cache.StringGetAsync($"token:{token}", ct);
                if (string.IsNullOrEmpty(tokenString)) continue;
                var refreshToken = JsonSerializer.Deserialize<RefreshToken>(tokenString);
                if (refreshToken is null) continue;
                yield return refreshToken;
            }
        }
    }
}
