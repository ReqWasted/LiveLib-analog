using LiveLib.Application.Commom.Result;
using MediatR;

namespace LiveLib.Application.Features.Users.UpdateUser
{
    public record UpdateUserCommand(Guid Id, UserUpdateDto user) : IRequest<Result>;
}
