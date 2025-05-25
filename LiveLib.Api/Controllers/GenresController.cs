using LiveLib.Api.Common;
using LiveLib.Application.Features.Genres.CreateGenre;
using LiveLib.Application.Features.Genres.DeleteGenre;
using LiveLib.Application.Features.Genres.GetGenreById;
using LiveLib.Application.Features.Genres.GetGenres;
using LiveLib.Application.Features.Genres.UpdateGenre;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LiveLib.Api.Controllers
{
    [Authorize]
    [ApiController]
    [Route("/api/[controller]")]
    public class GenresController : ControllerApiBase
    {
        private readonly IMediator _mediator;

        public GenresController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllGenres(CancellationToken ct)
        {
            var genres = await _mediator.Send(new GetGenresQuery(), ct);
            return Ok(genres);

        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetDetailGenre([FromRoute] Guid id, CancellationToken ct)
        {
            var get = await _mediator.Send(new GetGenreByIdQuery(id), ct);
            return ToActionResult(get);
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> AddGenre([FromBody] CreateGenreCommand reqwest, CancellationToken ct)
        {
            var create = await _mediator.Send(reqwest, ct);
            return ToActionResult(create);
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Update(Guid id, [FromBody] UpdateGenreDto updatedGenre, CancellationToken ct)
        {
            var result = await _mediator.Send(new UpdateGenreCommand(id, updatedGenre), ct);
            return ToActionResult(result);
        }

        //[HttpPatch("{id}")]
        //[Authorize(Roles = "Admin")]
        //public async Task<IActionResult> Update(Guid id, [FromBody] JsonPatchDocument<Genre> patch, CancellationToken ct)
        //{
        //    var result = await _mediator.Send(new PatchGenreCommand(id, patch), ct);
        //    return ToActionResult(result);
        //}

        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete([FromRoute] Guid id, CancellationToken ct)
        {
            var result = await _mediator.Send(new DeleteGenreCommand(id), ct);
            return ToActionResult(result);
        }
    }
}
