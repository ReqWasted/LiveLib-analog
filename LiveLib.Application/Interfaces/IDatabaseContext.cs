using LiveLib.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace LiveLib.Application.Interfaces
{
	public interface IDatabaseContext
	{
		DbSet<User> Users { get; set; }
		DbSet<RefreshToken> RefreshTokens { get; set; }

		Task<int> SaveChangesAsync(CancellationToken cts);
	}
}
