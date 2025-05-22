using LiveLib.Application.Commom.Result;
using LiveLib.Application.Models.Users;
using LiveLib.Domain.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LiveLib.Application.Features.Users.GetUserById
{
	public record GetUserByIdQuery(Guid userId) : IRequest<Result<UserDetailDto>>;
}
