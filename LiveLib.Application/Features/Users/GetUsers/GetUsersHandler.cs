using AutoMapper;
using LiveLib.Application.Interfaces;
using LiveLib.Domain.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LiveLib.Application.Features.Users.GetUsers
{
	public class GetUsersHandler : HandlerBase, IRequestHandler<GetUsersQuery, ICollection<User>>
	{
		public GetUsersHandler(IMapper mapper, IDatabaseContext context) : base(mapper, context)
		{
		}

		public async Task<ICollection<User>> Handle(GetUsersQuery request, CancellationToken cancellationToken)
		{
			return await _context.Users.AsNoTracking().ToListAsync(cancellationToken);
		}
	}
}
