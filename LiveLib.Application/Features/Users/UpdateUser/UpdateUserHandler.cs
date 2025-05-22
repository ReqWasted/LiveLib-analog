using AutoMapper;
using LiveLib.Application.Commom.Result;
using LiveLib.Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LiveLib.Application.Features.Users.UpdateUser
{
	public class UpdateUserHandler : HandlerBase, IRequestHandler<UpdateUserCommand, Result<int>>
	{
		private readonly IPassowrdHasher _passowrdHasher;

		public UpdateUserHandler(IMapper mapper, IDatabaseContext context, IPassowrdHasher passowrdHasher) : base(mapper, context)
		{
			_passowrdHasher = passowrdHasher;
		}

		public async Task<Result<int>> Handle(UpdateUserCommand reqwest, CancellationToken cancellationToken)
		{
			var updated = await _context.Users.Where(u => u.Id == reqwest.Id).ExecuteUpdateAsync(u =>
			u.SetProperty(p => p.Name, reqwest.user.Username)
			.SetProperty(p => p.Role, reqwest.user.Role)
			.SetProperty(p => p.PasswordHash, _passowrdHasher.Hash(reqwest.user.Password)), cancellationToken);

			if (updated == 0)
			{
				return Result<int>.Failure("User not updated");
			}

			await _context.SaveChangesAsync(cancellationToken);

			return Result<int>.Success(updated);

		}
	}
}
