using LiveLib.Application.Features.Books.CreateBook;
using LiveLib.Application.Features.Books.GetBooks;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LiveLib.Api.Controllers
{
    [ApiController]
    [Route("/api/[controller]")]
    public class BooksController : ControllerBase
    {
        private readonly IMediator _mediator;

        public BooksController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetAllBooks(CancellationToken ct)
        {
            return Ok(await _mediator.Send(new GetBooksQuery(), ct));
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> AddBook([FromBody] CreateBookCommand reqwest, CancellationToken ct)
        {
            return Ok(await _mediator.Send(new GetBooksQuery(), ct));
        }

    }
}
