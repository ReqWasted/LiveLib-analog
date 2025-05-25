using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using LiveLib.Application.Commom.Result;
using LiveLib.Application.Interfaces;
using LiveLib.Application.Models.Books;
using LiveLib.Application.Models.Collections;
using LiveLib.Domain.Models;
using MediatR;

namespace LiveLib.Application.Features.Collections.CreateCollection
{
    public class CreateCollectionHandler : HandlerBase, IRequestHandler<CreateCollectionCommand, Result<CollectionDto>>
    {
        public CreateCollectionHandler(IMapper mapper, IDatabaseContext context) : base(mapper, context)
        {
        }

        public async Task<Result<CollectionDto>> Handle(CreateCollectionCommand request, CancellationToken cancellationToken)
        {
            var collection = _context.Collections.Add(_mapper.Map<Collection>(request));
            var saved = await _context.SaveChangesAsync(cancellationToken);

            return saved == 0 ? Result<CollectionDto>.ServerError() : Result<CollectionDto>.NoContent();
        }
    }
}
