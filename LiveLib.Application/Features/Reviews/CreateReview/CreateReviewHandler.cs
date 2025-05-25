using AutoMapper;
using LiveLib.Application.Commom.Result;
using LiveLib.Application.Interfaces;
using LiveLib.Domain.Models;
using MediatR;

namespace LiveLib.Application.Features.Reviews.CreateReview
{
    public class CreateReviewHandler : HandlerBase, IRequestHandler<CreateReviewCommand, Result<Review>>
    {
        public CreateReviewHandler(IMapper mapper, IDatabaseContext context) : base(mapper, context)
        {
        }

        public async Task<Result<Review>> Handle(CreateReviewCommand request, CancellationToken cancellationToken)
        {
            var review = _mapper.Map<Review>(request);
            var entity = _context.Reviews.Add(review);
            await _context.SaveChangesAsync(cancellationToken);

            if (!entity.IsKeySet)
            {
                return Result<Review>.Failure("Review not created");
            }

            return Result.Success(entity.Entity);
        }
    }
}
