using LiveLib.Api.Common;
using LiveLib.Application.Features.Books.CreateBook;
using LiveLib.Application.Features.Books.DeleteBook;
using LiveLib.Application.Features.Books.GetBookById;
using LiveLib.Application.Features.Books.GetBooks;
using LiveLib.Application.Features.Books.UpdateBook;
using LiveLib.Application.Features.Books.UploadImage;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LiveLib.Api.Controllers
{
    [Authorize]
    [ApiController]
    [Route("/api/[controller]")]
    public class BooksController : ControllerApiBase
    {
        private readonly IMediator _mediator;

        public BooksController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll(CancellationToken ct)
        {
            var books = await _mediator.Send(new GetBooksQuery(), ct);
            return Ok(books);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetDetail([FromRoute] Guid id, CancellationToken ct)
        {
            var get = await _mediator.Send(new GetBookByIdQuery(id), ct);
            return ToActionResult(get);
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Add([FromBody] CreateBookCommand reqwest, CancellationToken ct)
        {
            var create = await _mediator.Send(reqwest, ct);
            return ToActionResult(create);
        }

        [HttpPost("cover/{id}")]
		[Authorize(Roles = "Admin")]
		[Consumes("multipart/form-data")]
		public async Task<IActionResult> UploadBookImage([FromRoute] Guid id, IFormFile image, CancellationToken ct)
		{
			var create = await _mediator.Send(new UploadImageCommand(id, image), ct);
			return ToActionResult(create);
		}

		[HttpPut("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Update(Guid id, [FromBody] UpdateBookDto updated, CancellationToken ct)
        {
            var result = await _mediator.Send(new UpdateBookCommand(id, updated), ct);
            return ToActionResult(result);
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete([FromRoute] Guid id, CancellationToken ct)
        {
            var result = await _mediator.Send(new DeleteBookCommand(id), ct);
            return ToActionResult(result);
        }
        
    }
}
