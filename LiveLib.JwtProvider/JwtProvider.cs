using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using LiveLib.Application.Interfaces;
using LiveLib.Domain.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace LiveLib.JwtProvider
{
	public class JwtProvider : IJwtProvider
	{
		private readonly IConfiguration _configuration;
		private readonly ITokenService _tokenService;
		public string CookieName { get; private set; }
		public TimeSpan RefreshTokenExpiresDays { get; private set; }
		public TimeSpan AccessTokenExpiresMinutes { get; private set; }
		public string Issuer { get; private set; }
		public string Audience { get; private set; }

		private string? SecretKey;

		public JwtProvider(IConfiguration configuration, ITokenService tokenService)
		{
			_configuration = configuration;
			_tokenService = tokenService;
			CookieName = configuration["JwtOptions:CookieName"] ?? "RefreshToken";
			RefreshTokenExpiresDays = TimeSpan.FromDays(int.Parse(configuration["JwtOptions:RefreshTokenExpiresDays"] ?? "15"));
			AccessTokenExpiresMinutes = TimeSpan.FromMinutes(int.Parse(configuration["JwtOptions:AccessTokenExpiresMinutes"] ?? "5"));
			Issuer = configuration["JwtOptions:Issuer"] ?? "DefautlIssuer";
			Audience = configuration["JwtOptions:Audience"] ?? "DefaultAudience";
			SecretKey = configuration["JwtOptions:SecretKey"];
		}

		public async Task<(string accessToken, string refreshToken)> GenerateTokensAsync(User user, CancellationToken cancellationToken = default)
		{
			var accessToken = GenerateAccessToken(user);
			var refreshToken = GenerateRefreshToken();

			await SaveRefreshTokenAsync(user.Id, refreshToken, cancellationToken);
			return (accessToken, refreshToken);
		}

		public async Task<(string accessToken, string refreshToken)> RefreshTokensAsync(string expiredAccessToken, string refreshToken, CancellationToken cancellationToken = default)
		{
			var principal = GetPrincipalFromExpiredToken(expiredAccessToken);

			var userIdClaim = principal.FindFirst(ClaimTypes.NameIdentifier);
			if (userIdClaim == null)
			{
				throw new SecurityTokenException("Недопустимый токен доступа: отсутствует идентификатор пользователя");
			}

			if (!Guid.TryParse(userIdClaim.Value, out var userId))
			{
				throw new SecurityTokenException("Недопустимый идентификатор пользователя в токене");
			}

			var storedRefreshToken = await _tokenService.GetActiveTokenAsync(userId, refreshToken, cancellationToken);

			if (storedRefreshToken == null)
			{
				throw new SecurityTokenException("Недопустимый токен обновления");
			}

			if (!storedRefreshToken.IsActive || storedRefreshToken.ExpiresAt <= DateTime.UtcNow)
			{
				await _tokenService.RevokeTokenAsync(storedRefreshToken.Id, cancellationToken);
				throw new SecurityTokenException("Срок действия токена обновления истек или он отозван");
			}

			await _tokenService.RevokeTokenAsync(storedRefreshToken.Id, cancellationToken);


			var user = new User
			{
				Id = userId,
				Role = principal.FindFirst(ClaimTypes.Role)?.Value
			};

			return await GenerateTokensAsync(user, cancellationToken);
		}

		public async Task RevokeAllUserTokensAsync(Guid userId, CancellationToken cancellationToken = default)
		{
			var userTokens = await _tokenService.GetActiveTokensByUserIdAsync(userId, cancellationToken);

			foreach (var token in userTokens)
			{
				await _tokenService.RevokeTokenAsync(token.Id, cancellationToken);
			}
		}

		public ClaimsPrincipal GetPrincipalFromExpiredToken(string token)
		{
			var secretKey = SecretKey;
			if (string.IsNullOrEmpty(secretKey))
			{
				throw new InvalidOperationException("Отсутсвует секретный ключ для подписи токена");
			}

			var tokenValidationParameters = new TokenValidationParameters
			{
				ValidateIssuer = true,
				ValidIssuer = Issuer,
				ValidateAudience = true,
				ValidAudience = Audience,
				ValidateIssuerSigningKey = true,
				IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey)),
				ValidateLifetime = false
			};

			var tokenHandler = new JwtSecurityTokenHandler();
			var principal = tokenHandler.ValidateToken(token, tokenValidationParameters, out SecurityToken securityToken);

			if (securityToken is not JwtSecurityToken jwtSecurityToken ||
				!jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256, StringComparison.InvariantCultureIgnoreCase))
			{
				throw new SecurityTokenException("Invalid token");
			}

			return principal;
		}

		private string GenerateAccessToken(User user)
		{
			var secretKey = SecretKey;
			if (string.IsNullOrEmpty(secretKey))
			{
				throw new InvalidOperationException("Отсутсвует секретный ключ для подписи токена");
			}

			var signingKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey));
			var signingCredentials = new SigningCredentials(signingKey, SecurityAlgorithms.HmacSha256);

			var claims = new List<Claim>
			{
				new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
				new Claim(ClaimTypes.Name, user.Name),
				new Claim(ClaimTypes.Role, user.Role)
			};

			if (!string.IsNullOrEmpty(user.Role))
			{
				claims.Add(new Claim(ClaimTypes.Role, user.Role));
			}

			var token = new JwtSecurityToken(
				issuer: Issuer,
				audience: Audience,
				claims: claims,
				expires: DateTime.UtcNow.Add(AccessTokenExpiresMinutes),
				signingCredentials: signingCredentials
			);

			return new JwtSecurityTokenHandler().WriteToken(token);
		}

		private string GenerateRefreshToken()
		{
			var randomNumber = new byte[64];
			using var rng = RandomNumberGenerator.Create();
			rng.GetBytes(randomNumber);

			return Convert.ToBase64String(randomNumber);
		}

		private async Task SaveRefreshTokenAsync(Guid userId, string token, CancellationToken cancellationToken = default)
		{
			var refreshToken = new RefreshToken
			{
				UserId = userId,
				Token = token,
				CreatedAt = DateTime.UtcNow,
				ExpiresAt = DateTime.UtcNow.Add(RefreshTokenExpiresDays),
			};

			await _tokenService.AddRefreshTokenAsync(refreshToken, cancellationToken);
		}
	}
}
