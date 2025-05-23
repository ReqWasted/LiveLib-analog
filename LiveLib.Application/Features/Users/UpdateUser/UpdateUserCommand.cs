using LiveLib.Application.Commom.Result;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LiveLib.Application.Features.Users.UpdateUser
{
	public record UpdateUserCommand(Guid Id, UserUpdateDto user) : IRequest<Result>;
}
