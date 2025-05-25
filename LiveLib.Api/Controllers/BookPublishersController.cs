using LiveLib.Api.Common;
using LiveLib.Application.Features.BookPublishers.CreateBookPublisher;
using LiveLib.Application.Features.BookPublishers.DeleteBookPublisher;
using LiveLib.Application.Features.BookPublishers.GetBookPublisherById;
using LiveLib.Application.Features.BookPublishers.GetBookPublishers;
using LiveLib.Application.Features.BookPublishers.UpdateBookPublisher;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LiveLib.Api.Controllers
{
    [Authorize]
    [ApiController]
    [Route("/api/[controller]")]
    public class BookPublishersController : ControllerApiBase
    {
        private readonly IMediator _mediator;

        public BookPublishersController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll(CancellationToken ct)
        {
            var books = await _mediator.Send(new GetBookPublishersQuery(), ct);
            return Ok(books);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetDetail([FromRoute] Guid id, CancellationToken ct)
        {
            var get = await _mediator.Send(new GetBookPublisherByIdQuery(id), ct);
            return ToActionResult(get);
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Add([FromBody] CreateBookPublisherCommand reqwest, CancellationToken ct)
        {
            var create = await _mediator.Send(reqwest, ct);
            return ToActionResult(create);
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Update(Guid id, [FromBody] UpdateBookPublisherDto updated, CancellationToken ct)
        {
            var result = await _mediator.Send(new UpdateBookPublisherCommand(id, updated), ct);
            return ToActionResult(result);
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete([FromRoute] Guid id, CancellationToken ct)
        {
            var result = await _mediator.Send(new DeleteBookPublisherCommand(id), ct);
            return ToActionResult(result);
        }
    }
}
