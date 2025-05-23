using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LiveLib.Application.Models.Collections;
using MediatR;

namespace LiveLib.Application.Features.Collections.GetCollectionsByUserId
{
    public record GetUserCollectionsByUserIdQuery(Guid UserId) : IRequest<ICollection<CollectionDto>>;
}
