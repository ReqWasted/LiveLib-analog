using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LiveLib.Application.Commom.Result;
using LiveLib.Application.Models.Books;
using MediatR;

namespace LiveLib.Application.Features.Collections.RemoveBookFromCollection
{
    public record RemoveBookFromCollectionCommand(Guid BookId, Guid CollectionId) : IRequest<Result<BookDto>>;
}
