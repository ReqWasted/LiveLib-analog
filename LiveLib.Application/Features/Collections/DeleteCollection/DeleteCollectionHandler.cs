using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using LiveLib.Application.Commom.Result;
using LiveLib.Application.Features.Books.DeleteBook;
using LiveLib.Application.Interfaces;
using LiveLib.Application.Models.Books;
using LiveLib.Application.Models.Collections;
using LiveLib.Domain.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace LiveLib.Application.Features.Collections.DeleteCollection
{
    public class DeleteCollectionHandler : HandlerBase, IRequestHandler<DeleteCollectionCommand, Result<CollectionDto>>
    {
        public DeleteCollectionHandler(IMapper mapper, IDatabaseContext context) : base(mapper, context)
        {
        }

        public async Task<Result<CollectionDto>> Handle(DeleteCollectionCommand request, CancellationToken cancellationToken)
        {
            var collection = await _context.Collections.FirstOrDefaultAsync(b => b.Id == request.Id, cancellationToken);
            if (collection == null)
            {
                return Result<CollectionDto>.NotFound($"Collection {request.Id} not found");
            }

            _context.Collections.Remove(collection);
            var removed = await _context.SaveChangesAsync(cancellationToken);

            return removed == 0 ? Result<CollectionDto>.NotFound($"Collection {request.Id} not deleted")
                : Result.Success(_mapper.Map<CollectionDto>(collection));
        }
    }
}
