using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LiveLib.Domain.Models;
using MediatR;

namespace LiveLib.Application.Features.Auth.Command
{
    public record UserRegisterCommand(string Name, string Email, string Password, string Role) : IRequest<User?>;
}
