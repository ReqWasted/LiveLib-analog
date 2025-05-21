using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using LiveLib.Application.Interfaces;
using LiveLib.Domain.Models;

namespace LiveLib.Application.Features.Users.Query
{
    public class GetUserByUsernameHandler : HandlerBase, IRequestHandler<GetUserByUsernameQuery, User?>
    {
        public GetUserByUsernameHandler(IMapper mapper, IDatabaseContext context) : base(mapper, context)
        {
        }

        public async Task<User?> Handle(GetUserByUsernameQuery request, CancellationToken cancellationToken)
        {
            return await _context.Users.FirstOrDefaultAsync(u => u.Name == request.Username, cancellationToken);
        }
    }
}
