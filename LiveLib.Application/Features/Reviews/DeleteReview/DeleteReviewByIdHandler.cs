using AutoMapper;
using LiveLib.Application.Commom.Result;
using LiveLib.Application.Interfaces;
using LiveLib.Application.Models.Collections;
using LiveLib.Application.Models.Reviews;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LiveLib.Application.Features.Reviews.DeleteReview
{
	public class DeleteReviewByIdHandler : HandlerBase, IRequestHandler<DeleteReviewByIdCommand, Result<ReviewDto>>
	{
		public DeleteReviewByIdHandler(IMapper mapper, IDatabaseContext context) : base(mapper, context)
		{
		}

		public async Task<Result<ReviewDto>> Handle(DeleteReviewByIdCommand request, CancellationToken cancellationToken)
		{
			var collection = await _context.Reviews.FirstOrDefaultAsync(b => b.Id == request.Id, cancellationToken);
			if (collection == null)
			{
				return Result<ReviewDto>.NotFound($"Review {request.Id} not found");
			}

			_context.Reviews.Remove(collection);
			var removed = await _context.SaveChangesAsync(cancellationToken);

			return removed == 0 ? Result<ReviewDto>.NotFound($"Review {request.Id} not deleted")
				: Result.Success(_mapper.Map<ReviewDto>(collection));
		}
	}
}
