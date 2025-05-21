using LiveLib.Application.Interfaces;
using LiveLib.Domain.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LiveLib.Database
{
	public class PostgresDatabaseContext(DbContextOptions<PostgresDatabaseContext> options) : DbContext(options), IDatabaseContext
	{
		// Identity
		public DbSet<User> Users { get; set; }

		public DbSet<RefreshToken> RefreshTokens { get; set; }

		protected override void OnModelCreating(ModelBuilder models)
		{
			models.ApplyConfigurationsFromAssembly(typeof(PostgresDatabaseContext).Assembly);
		}
	}
}
