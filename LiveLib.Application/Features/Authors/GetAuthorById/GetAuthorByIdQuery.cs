using LiveLib.Application.Commom.Result;
using LiveLib.Application.Models.Authors;
using MediatR;

namespace LiveLib.Application.Features.Authors.GetAuthorById
{
    public record GetAuthorByIdQuery(Guid Id) : IRequest<Result<AuthorDetailDto>>;
}
