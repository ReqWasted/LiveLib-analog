using LiveLib.Api.Common;
using LiveLib.Api.Extentions;
using LiveLib.Application.Features.Books.CreateBook;
using LiveLib.Application.Features.Books.DeleteBook;
using LiveLib.Application.Features.Books.GetBookById;
using LiveLib.Application.Features.Books.GetBooks;
using LiveLib.Application.Features.Books.UpdateBook;
using LiveLib.Application.Features.Reviews.CreateReview;
using LiveLib.Application.Features.Reviews.DeleteReview;
using LiveLib.Application.Features.Reviews.GetReviewById;
using LiveLib.Application.Features.Reviews.GetReviewsByBookId;
using LiveLib.Application.Features.Reviews.GetReviewsByUserId;
using LiveLib.Application.Features.Reviews.UpdateReview;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LiveLib.Api.Controllers
{
	[ApiController]
	//[Authorize]
	[Route("/api/[controller]")]
	public class ReviewsController : ControllerApiBase
	{
		private readonly IMediator _mediator;
		public ReviewsController(IMediator mediator)
		{
			_mediator = mediator;
		}

		[HttpGet("book/{bookId}")]
		public async Task<IActionResult> GetByBookId([FromRoute] Guid bookId, CancellationToken ct)
		{
			var result = await _mediator.Send(new GetReviewsByBookIdQuery(bookId), ct);
			return Ok(result);
		}

		[HttpGet("{reviewId}")]
		public async Task<IActionResult> GetDetail([FromRoute] Guid reviewId, CancellationToken ct)
		{
			var get = await _mediator.Send(new GetReviewByIdQuery(reviewId), ct);
			return ToActionResult(get);
		}

		[HttpPost]
		public async Task<IActionResult> Add([FromBody] CreateReviewCommand reqwest, CancellationToken ct)
		{
			reqwest.UserId = User.Id();
			var create = await _mediator.Send(reqwest, ct);
			return ToActionResult(create);
		}

		[HttpPut("{id}")]
		[Authorize(Roles = "Admin")]
		public async Task<IActionResult> Update(Guid id, [FromBody] UpdateReviewDto updated, CancellationToken ct)
		{
			var result = await _mediator.Send(new UpdateReviewCommand(id, updated), ct);
			return ToActionResult(result);
		}

		[HttpDelete("{id}")]
		[Authorize(Roles = "Admin")]
		public async Task<IActionResult> Delete([FromRoute] Guid id, CancellationToken ct)
		{
			var result = await _mediator.Send(new DeleteReviewByIdCommand(id), ct);
			return ToActionResult(result);
		}
	}
}
