using LiveLib.Application.Interfaces;
using LiveLib.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace LiveLib.Application.Services
{
	public class TokenService : ITokenService
	{
		private readonly IDatabaseContext _context;

		public TokenService(IDatabaseContext databaseContext)
		{
			_context = databaseContext;
		}

		public async Task AddRefreshTokenAsync(RefreshToken token, CancellationToken ct)
		{
			await _context.RefreshTokens.AddAsync(token, ct);
		}

		public async Task<RefreshToken?> GetActiveTokenAsync(Guid userId, string userRefreshToken, CancellationToken ct)
		{
			return await _context.RefreshTokens
				.Where(t => t.UserId == userId && t.Token == userRefreshToken && t.RevokedAt == null)
				.FirstOrDefaultAsync(ct);
		}

		public async Task RevokeTokenAsync(Guid tokenId, CancellationToken ct)
		{
			var token = await _context.RefreshTokens
				.Where(t => t.Id == tokenId)
				.FirstOrDefaultAsync(ct);

			if (token is null) return;

			token.RevokedAt = DateTime.UtcNow;
			await _context.SaveChangesAsync(ct);
		}

		public async Task<ICollection<RefreshToken>> GetActiveTokensByUserIdAsync(Guid userId, CancellationToken ct)
		{
			return await _context.RefreshTokens.Where(t => t.UserId == userId).ToListAsync(ct);
		}
	}
}
