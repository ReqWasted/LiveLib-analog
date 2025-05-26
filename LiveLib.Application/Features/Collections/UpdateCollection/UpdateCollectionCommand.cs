using LiveLib.Application.Commom.Result;
using LiveLib.Application.Models.Collections;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LiveLib.Application.Features.Collections.UpdateCollection
{
	public record UpdateCollectionCommand(Guid Id, UpdateCollectionDto Collection ) : IRequest<Result<CollectionDto>>;
}
