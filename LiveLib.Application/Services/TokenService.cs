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
            await _cache.ObjectSetAsync($"token:{refreshToken.Id}", refreshToken, ct, expiration);
            await _cache.StringSetAsync($"tokenId:{refreshToken.Token}", refreshToken.Id.ToString(), ct, expiration);
            await _cache.SetAddAsync($"user:{refreshToken.UserId}:tokens", refreshToken.Token, ct, expiration);
        }

        public async Task<RefreshToken?> GetActiveTokenAsync(string userRefreshToken, CancellationToken ct)
        {
            var tokenId = await _cache.StringGetAsync($"tokenId:{userRefreshToken}", ct);
            var tokenString = await _cache.StringGetAsync($"token:{tokenId}", ct);
            if (string.IsNullOrEmpty(tokenString)) return null;

            return JsonSerializer.Deserialize<RefreshToken>(tokenString);
        }

        public async Task<RefreshToken?> GetActiveTokenByIdAsync(Guid tokenId, CancellationToken ct)
        {
            var tokenString = await _cache.StringGetAsync($"token:{tokenId}", ct);
            if (string.IsNullOrEmpty(tokenString)) return null;
            return JsonSerializer.Deserialize<RefreshToken>(tokenString);
        }

        public async Task RevokeTokenAsync(RefreshToken refreshToken, CancellationToken ct)
        {
            await _cache.RemoveAsync($"token:{refreshToken.Id}", ct);
            await _cache.RemoveAsync($"tokenId:{refreshToken.Token}", ct);
            await _cache.SetRemoveAsync($"user:{refreshToken.UserId}:tokens", refreshToken.Token, ct);
        }

        public async IAsyncEnumerable<RefreshToken> GetActiveTokensByUserIdAsync(Guid userId, [EnumeratorCancellation] CancellationToken ct)
        {
            var tokens = await _cache.SetGetAsync($"user:{userId}:tokens", ct);
            foreach (var token in tokens)
            {
                ct.ThrowIfCancellationRequested();
                var tokenId = await _cache.StringGetAsync($"tokenId:{token}", ct);
                var tokenString = await _cache.StringGetAsync($"token:{tokenId}", ct);
                if (string.IsNullOrEmpty(tokenString)) continue;
                var refreshToken = JsonSerializer.Deserialize<RefreshToken>(tokenString);
                if (refreshToken is null) continue;
                yield return refreshToken;
            }
        }


    }
}
