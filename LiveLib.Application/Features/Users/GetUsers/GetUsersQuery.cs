using LiveLib.Application.Commom.Result;
using LiveLib.Domain.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LiveLib.Application.Features.Users.GetUsers
{
	public class GetUsersQuery : IRequest<ICollection<User>>
	{
	}
}
