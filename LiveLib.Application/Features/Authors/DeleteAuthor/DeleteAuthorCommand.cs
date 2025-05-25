using LiveLib.Application.Commom.Result;
using LiveLib.Application.Models.Authors;
using MediatR;

namespace LiveLib.Application.Features.Authors.DeleteAuthor
{
    public record DeleteAuthorCommand(Guid Id) : IRequest<Result<AuthorDetailDto>>;
}
