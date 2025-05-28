using LiveLib.Api.Common;
using LiveLib.Application.Features.Books.CreateBook;
using LiveLib.Application.Features.Books.DeleteBook;
using LiveLib.Application.Features.Books.GetBookById;
using LiveLib.Application.Features.Books.GetBooks;
using LiveLib.Application.Features.Books.GetCover;
using LiveLib.Application.Features.Books.UpdateBook;
using LiveLib.Application.Features.Books.UploadCover;
using LiveLib.Application.Models.Books;
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
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(BookDto))]
        public async Task<IActionResult> GetAll(CancellationToken ct)
        {
            var books = await _mediator.Send(new GetBooksQuery(), ct);
            return Ok(books);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(BookDetailDto))]

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

        [HttpGet("{bookId}/cover/{coverId}")]
        [Authorize(Roles = "Admin")]
        [Consumes("multipart/form-data")]
        public async Task<IActionResult> GetBookCover(Guid bookId, string coverId, CancellationToken ct)
        {
            var cover = await _mediator.Send(new GetCoverQuery(bookId, coverId), ct);

            if (cover.IsFailure)
            {
                return NotFound(cover.ErrorInfo!.Message);
            }

            return File(cover.Value!, "image/jpg");
        }

        [HttpPost("{bookId}/cover")]
        [Authorize(Roles = "Admin")]
        [Consumes("multipart/form-data")]
        public async Task<IActionResult> UploadBookImage(Guid bookId, IFormFile image, CancellationToken ct)
        {
            var create = await _mediator.Send(new UploadCoverCommand(bookId, image), ct);
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
