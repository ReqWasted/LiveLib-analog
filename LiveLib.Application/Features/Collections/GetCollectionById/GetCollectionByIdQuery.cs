using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LiveLib.Application.Commom.Result;
using LiveLib.Application.Models.Collections;
using MediatR;

namespace LiveLib.Application.Features.Collections.GetCollectionById
{
    public record GetCollectionByIdQuery(Guid Id) : IRequest<Result<CollectionDetailDto>>;
}
