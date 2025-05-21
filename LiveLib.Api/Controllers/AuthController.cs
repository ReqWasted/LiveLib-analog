using System.Security.Claims;
using LiveLib.Application.Features.Auth.Command;
using LiveLib.Application.Features.Users.Query;
using LiveLib.Application.Interfaces;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace LiveLib.Api.Controllers
{
    [ApiController]
    [Route("/auth")]
    public class AuthController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IPassowrdHasher _passwordHasher;
        private readonly IJwtProvider _jwtProvider;
        private readonly ITokenService _tokenService;

        public AuthController(IMediator mediator, IPassowrdHasher passwordHasher, IJwtProvider jwtProvider, ITokenService tokenService)
        {
            _mediator = mediator;
            _passwordHasher = passwordHasher;
            _jwtProvider = jwtProvider;
            _tokenService = tokenService;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] UserLoginCommand reqwest, CancellationToken ct)
        {
            var user = await _mediator.Send(new GetUserByUsernameQuery(reqwest.Username), ct);

            if (user == null)
            {
                return NotFound(reqwest.Username);
            }

            var isPasswordEqual = _passwordHasher.Verify(reqwest.Password, user.PasswordHash);
            if (!isPasswordEqual)
            {
                return Unauthorized(reqwest.Username);
            }

            var tokens = await _jwtProvider.GenerateTokensAsync(user, ct);

            Response.Cookies.Append(
                _jwtProvider.CookieName,
                tokens.refreshToken,
                new CookieOptions { 
                    Path = "/auth/refresh",
                    Expires = DateTime.UtcNow.Add(_jwtProvider.RefreshTokenExpiresDays),
                    HttpOnly = true,
                    SameSite = SameSiteMode.Strict,
                    Secure = false,
                });

            return Ok(new { AccessToken = tokens.accessToken });
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] UserRegisterCommand reqwest, CancellationToken ct)
        {
            var user = await _mediator.Send(new GetUserByUsernameQuery(reqwest.Name), ct);

            if (user != null)
            {
                return Conflict(reqwest.Name);
            }

            var newUser = await _mediator.Send(reqwest, ct);

            if (newUser == null)
            {
                return Problem(reqwest.Name);
            }

            return Ok();
        }

        [HttpPost("refresh")]
        public async Task<IActionResult> RefreshTokens(CancellationToken ct)
        {

            var refreshToken = Request.Cookies[_jwtProvider.CookieName];

            if (string.IsNullOrEmpty(refreshToken))
            {
                return Unauthorized("Отсутствует refresh токен");
            }

            var accessToken = HttpContext.Request.Headers["Authorization"].ToString()
                .Replace("Bearer ", "", StringComparison.OrdinalIgnoreCase);

            ClaimsPrincipal principal;
            try
            {
                principal = _jwtProvider.GetPrincipalFromExpiredToken(accessToken);
            }
            catch (Exception ex)
            {
                return Unauthorized(ex.Message);
            }

            var username = principal.FindFirst(ClaimTypes.Name)?.Value;
            if (username == null)
            {
                return Unauthorized("Отсутсвует имя пользователя");
            }

            var user = await _mediator.Send(new GetUserByUsernameQuery(username), ct);
            if (user == null)
            {
                return NotFound(username);
            }

            var tokens = await _jwtProvider.GenerateTokensAsync(user, ct);

            Response.Cookies.Delete(_jwtProvider.CookieName);
            Response.Cookies.Append(_jwtProvider.CookieName, tokens.refreshToken, new()
            {
                Path = "/auth/refresh",
                Expires = DateTime.UtcNow.Add(_jwtProvider.RefreshTokenExpiresDays),
                HttpOnly = true,
                SameSite = SameSiteMode.Strict,
                Secure = false,
            });

            return Ok(new { AccessToken = tokens.accessToken });
        }
    }
}