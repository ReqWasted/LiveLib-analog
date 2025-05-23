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
    public class UpdateUserHandler : HandlerBase, IRequestHandler<UpdateUserCommand, Result>
    {
        private readonly IPassowrdHasher _passowrdHasher;

        public UpdateUserHandler(IMapper mapper, IDatabaseContext context, IPassowrdHasher passowrdHasher) : base(mapper, context)
        {
            _passowrdHasher = passowrdHasher;
        }

        public async Task<Result> Handle(UpdateUserCommand reqwest, CancellationToken cancellationToken)
        {
            //var updated = await _context.Users.Where(u => u.Id == reqwest.Id).ExecuteUpdateAsync(u =>
            //u.SetProperty(p => p.Name, reqwest.user.Username)
            //.SetProperty(p => p.Role, reqwest.user.Role)
            //.SetProperty(p => p.PasswordHash, _passowrdHasher.Hash(reqwest.user.Password)), cancellationToken);

            var user = await _context.Users.FirstOrDefaultAsync(u => u.Id == reqwest.Id, cancellationToken);

            if (user == null)
            {
                return Result.Failure("User not found");
            }

            user.Name = reqwest.user.Username;
            user.Role = reqwest.user.Role;
            user.Email = reqwest.user.Email;
            user.PasswordHash = _passowrdHasher.Hash(reqwest.user.Password);

            await _context.SaveChangesAsync(cancellationToken);

            return Result.Success();

        }
    }
}
