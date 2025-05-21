using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using LiveLib.Application.Features.Auth.Command;
using LiveLib.Application.Interfaces;
using LiveLib.Domain.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace LiveLib.Application.Features
{
    public class UserRegisterHandler : HandlerBase, IRequestHandler<UserRegisterCommand, User?>
    {
        private readonly IPassowrdHasher _passwordHasher;

        public UserRegisterHandler(IMapper mapper, IDatabaseContext context, IPassowrdHasher passwordHasher) : base(mapper, context)
        {
            _passwordHasher = passwordHasher;
        }

        public async Task<User?> Handle(UserRegisterCommand request, CancellationToken cancellationToken)
        {
            var user = new User
            {
                Email = request.Email,
                Name = request.Name,
                PasswordHash = _passwordHasher.Hash(request.Password),
                Role = request.Role,
            };
            var entity = await _context.Users.AddAsync(user, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);
            return entity.Entity;
        }
    }
}
