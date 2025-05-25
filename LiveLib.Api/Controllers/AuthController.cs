using LiveLib.Api.Common;
using LiveLib.Api.Models;
using LiveLib.Application.Features.Users.CreateUser;
using LiveLib.Application.Features.Users.GetUserById;
using LiveLib.Application.Features.Users.GetUserByUsername;
using LiveLib.Application.Interfaces;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace LiveLib.Api.Controllers
{
    [ApiController]
    [Route("auth")]
    public class AuthController : ControllerApiBase
    {
        private readonly IMediator _mediator;
        private readonly IPassowrdHasher _passwordHasher;
        private readonly IJwtProvider _jwtProvider;

        private CookieOptions _cookieOptions => new()
        {
            Path = "/auth",
            Expires = DateTime.UtcNow.AddDays(15),
            HttpOnly = true,
            SameSite = SameSiteMode.Lax,
            Secure = false,
        };

        public AuthController(IMediator mediator, IPassowrdHasher passwordHasher, IJwtProvider jwtProvider)
        {
            _mediator = mediator;
            _passwordHasher = passwordHasher;
            _jwtProvider = jwtProvider;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] UserLoginDto reqwest, CancellationToken ct)
        {
            var refreshToken = Request.Cookies[_jwtProvider.CookieName];

            if (!string.IsNullOrEmpty(refreshToken))
            {
                var isTokenValid = await _jwtProvider.ValidateRefreshToken(refreshToken, ct);
                if (isTokenValid) return BadRequest("Токен ещё действует");
            }

            var result = await _mediator.Send(new GetUserByUsernameQuery(reqwest.Username), ct);

            if (result.IsFailure)
            {
                return ToActionResult(result);
            }

            var user = result.Value!;
            var isPasswordEqual = _passwordHasher.Verify(reqwest.Password, user.PasswordHash);
            if (!isPasswordEqual)
            {
                return Unauthorized(reqwest.Username);
            }

            var tokens = await _jwtProvider.GenerateTokensAsync(user, ct);
            Response.Cookies.Append(_jwtProvider.CookieName, tokens.refreshToken, _cookieOptions);

            return Ok(new { AccessToken = tokens.accessToken });
        }

        [HttpPost("logout")]
        public async Task<IActionResult> Logout(CancellationToken ct)
        {
            var refreshToken = Request.Cookies[_jwtProvider.CookieName];

            if (string.IsNullOrEmpty(refreshToken))
            {
                return Unauthorized("Отсутствует refresh токен");
            }

            await _jwtProvider.RevokeUserTokenAsync(refreshToken, ct);
            Response.Cookies.Delete(_jwtProvider.CookieName, _cookieOptions);

            return Ok();
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] CreateUserCommand reqwest, CancellationToken ct)
        {
            var result = await _mediator.Send(new GetUserByUsernameQuery(reqwest.Name), ct);

            if (result.IsSuccess)
            {
                return Conflict(reqwest.Name);
            }

            var newUser = await _mediator.Send(reqwest, ct);

            if (newUser.IsFailure)
            {
                return ToActionResult(newUser);
            }

            return Created();
        }

        [HttpPost("refresh")]
        public async Task<IActionResult> RefreshTokens(CancellationToken ct)
        {
            var refreshToken = Request.Cookies[_jwtProvider.CookieName];

            if (string.IsNullOrEmpty(refreshToken))
            {
                return Unauthorized("Отсутствует refresh токен");
            }

            var userId = await _jwtProvider.GetUserIdByRefreshTokenAsync(refreshToken, ct);
            var result = await _mediator.Send(new GetUserByIdQuery(userId), ct);

            if (result.IsFailure)
            {
                return NotFound(refreshToken);
            }

            await _jwtProvider.RevokeUserTokenAsync(refreshToken, ct);
            var tokens = await _jwtProvider.GenerateTokensAsync(result.Value!, ct);

            Response.Cookies.Delete(_jwtProvider.CookieName, _cookieOptions);
            Response.Cookies.Append(_jwtProvider.CookieName, tokens.refreshToken, _cookieOptions);

            return Ok(new { AccessToken = tokens.accessToken });
        }
    }
}