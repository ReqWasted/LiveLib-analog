using AutoMapper;
using AutoMapper.QueryableExtensions;
using LiveLib.Application.Commom.Result;
using LiveLib.Application.Interfaces;
using LiveLib.Application.Models.Users;
using LiveLib.Domain.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LiveLib.Application.Features.Users.GetUserById
{
	public class GetUserByIdHandler : HandlerBase, IRequestHandler<GetUserByIdQuery, Result<UserDetailDto>>
	{
		public GetUserByIdHandler(IMapper mapper, IDatabaseContext context) : base(mapper, context)
		{
		}

		public async Task<Result<UserDetailDto>> Handle(GetUserByIdQuery request, CancellationToken cancellationToken)
		{
			var user = await _context.Users.FirstOrDefaultAsync(u => u.Id == request.userId, cancellationToken);

			if (user == null)
				Result<UserDetailDto>.Failure("User not found");

			return Result<UserDetailDto>.Success(_mapper.Map<UserDetailDto>(user));
		}
	}
}
