using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LiveLib.Application.Commom.Result;
using LiveLib.Application.Models.Books;
using MediatR;

namespace LiveLib.Application.Features.Collections.AddBookToCollection
{
    public record AddBookToCollectionCommand(Guid BookId, Guid CollectionId) : IRequest<Result<BookDto>>;
}
