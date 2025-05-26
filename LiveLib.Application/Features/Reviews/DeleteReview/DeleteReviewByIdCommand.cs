using LiveLib.Application.Commom.Result;
using LiveLib.Application.Models.Reviews;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LiveLib.Application.Features.Reviews.DeleteReview
{
	public record DeleteReviewByIdCommand(Guid Id) : IRequest<Result<ReviewDto>>;
}
