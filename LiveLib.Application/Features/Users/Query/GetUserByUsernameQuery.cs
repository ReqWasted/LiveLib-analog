using LiveLib.Domain.Models;
using MediatR;
namespace LiveLib.Application.Features.Users.Query
{
    public record GetUserByUsernameQuery(string Username) : IRequest<User?>;
}
