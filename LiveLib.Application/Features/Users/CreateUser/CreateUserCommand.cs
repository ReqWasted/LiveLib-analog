using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LiveLib.Application.Commom.Result;
using LiveLib.Domain.Models;
using MediatR;

namespace LiveLib.Application.Features.Users.CreateUser
{
    public record CreateUserCommand(string Name, string Email, string Password, string Role) : IRequest<Result<User>>;
}
