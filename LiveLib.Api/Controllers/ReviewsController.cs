using System.Security.Claims;
using LiveLib.Api.Extentions;
using LiveLib.Application.Features.Reviews.CreateReview;
using LiveLib.Application.Features.Users.UpdateUser;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LiveLib.Api.Controllers
{
    [ApiController]
    [Authorize]
    [Route("/api/[controller]")]
    public class ReviewsController : ControllerBase
    {
        private readonly IMediator _mediator;
        public ReviewsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateReviewCommand reqwest, CancellationToken ct)
        {
            reqwest.UserId = User.Id();
            var result = await _mediator.Send(reqwest, ct);
            return result.IsSuccess ? Ok("Review successfully created") : Problem(result.Error);
        }

        //[HttpGet("{id}")]
        //public async Task<IActionResult> GetById([FromRoute] Guid id, CancellationToken ct)
        //{
        //    var result = await _mediator.Send(reqwest, ct);
        //    return result.IsSuccess ? Ok("Review successfully created") : Problem(result.Error);
        //}
    }
}
