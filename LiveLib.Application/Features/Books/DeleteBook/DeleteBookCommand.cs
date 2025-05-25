using LiveLib.Application.Commom.Result;
using LiveLib.Application.Models.Books;
using MediatR;

namespace LiveLib.Application.Features.Books.DeleteBook
{
    public record DeleteBookCommand(Guid Id) : IRequest<Result<BookDetailDto>>;
}
