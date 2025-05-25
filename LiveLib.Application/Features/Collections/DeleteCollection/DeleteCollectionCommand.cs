using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LiveLib.Application.Commom.Result;
using LiveLib.Application.Models.Books;
using LiveLib.Application.Models.Collections;
using MediatR;

namespace LiveLib.Application.Features.Collections.DeleteCollection
{
    public record DeleteCollectionCommand(Guid Id) : IRequest<Result<CollectionDto>>;
}
