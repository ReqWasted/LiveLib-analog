using LiveLib.Application.Commom.Result;
using LiveLib.Application.Models.Books;
using MediatR;

namespace LiveLib.Application.Features.Books.GetBookById
{
    public record GetBookByIdQuery(Guid Id) : IRequest<Result<BookDetailDto>>;
}
