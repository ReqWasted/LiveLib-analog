
using LiveLib.Application.Commom.Result;
using LiveLib.Application.Models.Authors;
using MediatR;

namespace LiveLib.Application.Features.Authors.UpdateAuthor
{
    public record UpdateAuthorCommand(Guid Id, UpdateAuthorDto Author) : IRequest<Result<AuthorDetailDto>>;
}
