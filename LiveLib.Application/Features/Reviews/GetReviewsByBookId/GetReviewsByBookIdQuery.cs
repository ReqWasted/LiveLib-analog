using LiveLib.Application.Models.Reviews;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LiveLib.Application.Features.Reviews.GetReviewsByBookId
{
	public record GetReviewsByBookIdQuery(Guid Id) : IRequest<ICollection<ReviewDto>>;
}
