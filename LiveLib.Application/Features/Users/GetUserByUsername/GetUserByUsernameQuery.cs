using LiveLib.Application.Commom.Result;
using LiveLib.Application.Models.Users;
using LiveLib.Domain.Models;
using MediatR;
namespace LiveLib.Application.Features.Users.GetUserByUsername
{
    public record GetUserByUsernameQuery(string Username) : IRequest<Result<User>>;
}
