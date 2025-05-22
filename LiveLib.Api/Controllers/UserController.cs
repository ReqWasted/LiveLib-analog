using LiveLib.Api.Models;
using LiveLib.Application.Features.Users.DeleteUser;
using LiveLib.Application.Features.Users.GetUserById;
using LiveLib.Application.Features.Users.GetUsers;
using LiveLib.Application.Features.Users.UpdateUser;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace LiveLib.Api.Controllers
{
	[ApiController]
	[Authorize]
	[Route("/api/[controller]")]
	public class UserController : ControllerBase
	{
		private readonly IMediator _mediator;

		public UserController(IMediator mediator)
		{
			_mediator = mediator;
		}

		[HttpGet("me")]
		public async Task<IActionResult> GetProfile(CancellationToken ct)
		{
			var userId = !User.Identity.IsAuthenticated
			? Guid.Empty
			: Guid.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);

			var user = await _mediator.Send(new GetUserByIdQuery(userId));

			if (user == null)
			{
				NotFound(userId);
			}

			return Ok(user);
		}

		[HttpGet]
		[Authorize(Roles = "Admin")]
		public async Task<IActionResult> GetUsers(CancellationToken ct)
		{
			return Ok(await _mediator.Send(new GetUsersQuery(), ct));
		}

		[HttpDelete("{id}")]
		[Authorize(Roles = "Admin")]
		public async Task<IActionResult> Delete([FromRoute] Guid id, CancellationToken ct)
		{
			var result = await _mediator.Send(new DeleteUserCommand(id), ct);
			return result.IsSuccess ? Ok("User successfully deleted") : Problem(result.Error);
		}

		[HttpPut("{id}")]
		[Authorize(Roles = "Admin")]
		public async Task<IActionResult> Update(Guid id, [FromBody] UserUpdateDto updatedUser, CancellationToken ct)
		{
			var result = await _mediator.Send(new UpdateUserCommand(id, updatedUser), ct);
			return result.IsSuccess ? Ok("User successfully updated") : Problem(result.Error);
		}


	}
}
