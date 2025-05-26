using LiveLib.Api.Common;
using LiveLib.Api.Extentions;
using LiveLib.Application.Features.Collections.GetCollectionsByUserId;
using LiveLib.Application.Features.Reviews.GetReviewsByUserId;
using LiveLib.Application.Features.Users.DeleteUser;
using LiveLib.Application.Features.Users.GetUserById;
using LiveLib.Application.Features.Users.GetUsers;
using LiveLib.Application.Features.Users.UpdateUser;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LiveLib.Api.Controllers
{
	[ApiController]
	[Authorize]
	[Route("/api/[controller]")]
	public class UsersController : ControllerApiBase
	{
		private readonly IMediator _mediator;
		public UsersController(IMediator mediator)
		{
			_mediator = mediator;
		}

		[HttpGet("profile")]
		public async Task<IActionResult> GetProfile(CancellationToken ct)
		{
			var result = await _mediator.Send(new GetUserByIdQuery(User.Id()));
			return ToActionResult(result);
			//return result.IsSuccess ? Ok(result.Value) : NotFound(result.ErrorMessage);
		}

		[HttpGet("profile/reviews")]
		public async Task<IActionResult> GetUserReviews(CancellationToken ct)
		{
			var reviews = await _mediator.Send(new GetReviewsByUserIdQuery(User.Id()), ct);
			return Ok(reviews);
		}

		[HttpGet("profile/collections")]
		public async Task<IActionResult> GetUserCollections(CancellationToken ct)
		{
			var reviews = await _mediator.Send(new GetUserCollectionsByUserIdQuery(User.Id()), ct);
			return Ok(reviews);
		}

		[HttpGet]
		[Authorize(Roles = "Admin")]
		public async Task<IActionResult> GetUsers(CancellationToken ct)
		{
			return Ok(await _mediator.Send(new GetUsersQuery(), ct));
		}

		[HttpGet("{id}")]
		[Authorize(Roles = "Admin")]
		public async Task<IActionResult> GetUserById([FromRoute] Guid id, CancellationToken ct)
		{
			var result = await _mediator.Send(new GetUserByIdQuery(id), ct);
			return ToActionResult(result);
			//return result.IsSuccess ? Ok(result.Value) : NotFound(result.ErrorMessage);
		}

		[HttpDelete("{id}")]
		[Authorize(Roles = "Admin")]
		public async Task<IActionResult> Delete([FromRoute] Guid id, CancellationToken ct)
		{
			var result = await _mediator.Send(new DeleteUserCommand(id), ct);
			return ToActionResult(result);
			//return result.IsSuccess ? Ok("User successfully deleted") : Problem(result.ErrorMessage);
		}

		[HttpPut("{id}")]
		[Authorize(Roles = "Admin")]
		public async Task<IActionResult> Update(Guid id, [FromBody] UserUpdateDto updatedUser, CancellationToken ct)
		{
			var result = await _mediator.Send(new UpdateUserCommand(id, updatedUser), ct);
			return ToActionResult(result);
			//return result.IsSuccess ? Ok("User successfully updated") : Problem(result.ErrorMessage);
		}


	}
}
