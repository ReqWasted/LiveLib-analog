using LiveLib.Domain.Models;

namespace LiveLib.Application.Interfaces
{
    public interface ITokenService
    {
        Task AddRefreshTokenAsync(RefreshToken token, CancellationToken ct);
        Task<RefreshToken?> GetActiveTokenAsync(Guid userId, string userRefreshToken, CancellationToken ct);
        Task<ICollection<RefreshToken>> GetActiveTokensByUserIdAsync(Guid userId, CancellationToken ct);
        Task RevokeTokenAsync(Guid tokenId, CancellationToken ct);
    }
}