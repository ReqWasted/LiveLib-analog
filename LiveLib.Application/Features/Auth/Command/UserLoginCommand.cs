using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;

namespace LiveLib.Application.Features.Auth.Command
{
    public record UserLoginCommand(string Username, string Password) : IRequest;
}
