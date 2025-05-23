using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LiveLib.Application.Models.Reviews;
using MediatR;

namespace LiveLib.Application.Features.Reviews.GetReviewsByUserId
{
    public record GetReviewsByUserIdQuery(Guid UserId) : IRequest<ICollection<ReviewDto>>;
}
