using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using LiveLib.Application.Interfaces;
using LiveLib.Domain.Models;
using LiveLib.Application.Commom.Result;
using LiveLib.Application.Models.Users;

namespace LiveLib.Application.Features.Users.GetUserByUsername
{
	public class GetUserByUsernameHandler : HandlerBase, IRequestHandler<GetUserByUsernameQuery, Result<User>>
	{
		public GetUserByUsernameHandler(IMapper mapper, IDatabaseContext context) : base(mapper, context)
		{
		}

		public async Task<Result<User>> Handle(GetUserByUsernameQuery request, CancellationToken cancellationToken)
		{
			var user = await _context.Users.FirstOrDefaultAsync(u => u.Name == request.Username, cancellationToken);

			if (user == null)
				return Result<User>.Failure("User not found");

			return Result<User>.Success(user);
		}
	}
}
