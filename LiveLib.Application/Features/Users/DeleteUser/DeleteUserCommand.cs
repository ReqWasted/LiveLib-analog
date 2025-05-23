using LiveLib.Application.Commom.Result;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LiveLib.Application.Features.Users.DeleteUser
{
	public record DeleteUserCommand(Guid UserId) : IRequest<Result>;
}
