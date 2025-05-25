using LiveLib.Api.Common;
using LiveLib.Api.Extentions;
using LiveLib.Application.Features.BookPublishers.CreateBookPublisher;
using LiveLib.Application.Features.BookPublishers.DeleteBookPublisher;
using LiveLib.Application.Features.BookPublishers.GetBookPublisherById;
using LiveLib.Application.Features.BookPublishers.GetBookPublishers;
using LiveLib.Application.Features.BookPublishers.UpdateBookPublisher;
using LiveLib.Application.Features.Collections.AddBookToCollection;
using LiveLib.Application.Features.Collections.CreateCollection;
using LiveLib.Application.Features.Collections.DeleteCollection;
using LiveLib.Application.Features.Collections.GetCollectionById;
using LiveLib.Application.Features.Collections.RemoveBookFromCollection;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LiveLib.Api.Controllers
{
    [Authorize]
    [ApiController]
    [Route("/api/[controller]")]
    public class CollectionsController : ControllerApiBase
    {
        private readonly IMediator _mediator;

        public CollectionsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        //[HttpGet]
        //public async Task<IActionResult> GetAll(CancellationToken ct)
        //{
        //    var books = await _mediator.Send(new GetBookPublishersQuery(), ct);
        //    return Ok(books);
        //}

        [HttpGet("{id}")]
        public async Task<IActionResult> GetDetail([FromRoute] Guid id, CancellationToken ct)
        {
            var get = await _mediator.Send(new GetCollectionByIdQuery(id), ct);
            return ToActionResult(get);
        }

        [HttpPost]
        public async Task<IActionResult> Add([FromBody] string title, CancellationToken ct)
        {
            var create = await _mediator.Send(new CreateCollectionCommand { Title = title, OwnerUserId = User.Id() }, ct);
            return ToActionResult(create);
        }

        [HttpPost("{id}/books")]
        public async Task<IActionResult> AddBookToCollection([FromRoute] Guid id, [FromBody] Guid bookId, CancellationToken ct)
        {
            var create = await _mediator.Send(new AddBookToCollectionCommand(bookId, id), ct);
            return ToActionResult(create);
        }

        //[HttpPut("{id}")]
        //[Authorize(Roles = "Admin")]
        //public async Task<IActionResult> Update(Guid id, [FromBody] UpdateBookPublisherDto updated, CancellationToken ct)
        //{
        //    var result = await _mediator.Send(new UpdateBookPublisherCommand(id, updated), ct);
        //    return ToActionResult(result);
        //}

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete([FromRoute] Guid id, CancellationToken ct)
        {
            var result = await _mediator.Send(new DeleteCollectionCommand(id), ct);
            return ToActionResult(result);
        }

        [HttpDelete("{collectionId}/books/{bookId}")]
        public async Task<IActionResult> RemoveBookFromCollection([FromRoute] Guid collectionId, [FromRoute] Guid bookId, CancellationToken ct)
        {
            var result = await _mediator.Send(new RemoveBookFromCollectionCommand(bookId, collectionId), ct);
            return ToActionResult(result);
        }
    }
}
