using LiveLib.Domain.Models;

namespace LiveLib.Application.Interfaces
{
    public interface IJwtProvider
    {
        string CookieName { get; }
        TimeSpan RefreshTokenExpiresDays { get; }
        TimeSpan AccessTokenExpiresMinutes { get; }
        string Issuer { get; }
        string Audience { get; }

        /// <summary>
        /// Генерирует и сохраняет пару токенов (access и refresh) для пользователя
        /// </summary>
        /// <param name="user">Пользователь, для которого генерируются токены</param>
        /// <param name="cancellationToken">Токен отмены</param>
        /// <returns>Пара токенов (accessToken, refreshToken)</returns>
        Task<(string accessToken, string refreshToken)> GenerateTokensAsync(User user, CancellationToken cancellationToken = default);

        /// <summary>
        /// Извлекает информацию о пользователе из токена доступа
        /// </summary>
        /// <param name="token">Access token</param>
        /// <returns>Информация о пользователе из токена</returns>
        System.Security.Claims.ClaimsPrincipal GetPrincipalFromExpiredToken(string token);

        /// <summary>
        /// Обновляет пару токенов на основе существующего refresh токена
        /// </summary>
        /// <param name="expiredAccessToken">Истекший access token</param>
        /// <param name="refreshToken">Refresh token</param>
        /// <param name="cancellationToken">Токен отмены</param>
        /// <returns>Новая пара токенов (accessToken, refreshToken)</returns>
        Task<(string accessToken, string refreshToken)> RefreshTokensAsync(
            string expiredAccessToken,
            string refreshToken,
            CancellationToken cancellationToken = default);

        /// <summary>
        /// Отзывает все refresh токены пользователя
        /// </summary>
        /// <param name="userId">Идентификатор пользователя</param>
        /// <param name="cancellationToken">Токен отмены</param>
        Task RevokeAllUserTokensAsync(Guid userId, CancellationToken cancellationToken = default);

    }
}
