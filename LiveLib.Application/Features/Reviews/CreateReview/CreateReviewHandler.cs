using AutoMapper;
using LiveLib.Application.Commom.Result;
using LiveLib.Application.Interfaces;
using LiveLib.Application.Models.Collections;
using LiveLib.Application.Models.Reviews;
using LiveLib.Domain.Models;
using MediatR;

namespace LiveLib.Application.Features.Reviews.CreateReview
{
	public class CreateReviewHandler : HandlerBase, IRequestHandler<CreateReviewCommand, Result<ReviewDto>>
	{
		public CreateReviewHandler(IMapper mapper, IDatabaseContext context) : base(mapper, context)
		{
		}

		public async Task<Result<ReviewDto>> Handle(CreateReviewCommand request, CancellationToken cancellationToken)
		{
			var collection = _context.Reviews.Add(_mapper.Map<Review>(request));
			var saved = await _context.SaveChangesAsync(cancellationToken);

			return saved == 0 ? Result<ReviewDto>.ServerError() : Result<ReviewDto>.NoContent();
		}
	}
}
