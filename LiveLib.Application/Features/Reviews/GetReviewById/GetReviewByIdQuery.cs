using LiveLib.Application.Commom.Result;
using LiveLib.Application.Models.Reviews;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LiveLib.Application.Features.Reviews.GetReviewById
{
	public record GetReviewByIdQuery(Guid Id) : IRequest<Result<ReviewDetatilDto>>;
}
