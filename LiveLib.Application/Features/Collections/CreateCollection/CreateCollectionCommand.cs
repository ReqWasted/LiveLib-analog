using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LiveLib.Application.Commom.Mapping;
using LiveLib.Application.Commom.Result;
using LiveLib.Application.Models.Collections;
using LiveLib.Domain.Models;
using MediatR;

namespace LiveLib.Application.Features.Collections.CreateCollection
{
    public class CreateCollectionCommand : IRequest<Result<CollectionDto>>, IMapWith<Collection>
    {
        public string Title { get; set; } = string.Empty;
        public Guid OwnerUserId { get; set; }
    }
}
