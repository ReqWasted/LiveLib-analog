using AutoMapper;
using LiveLib.Application.Commom.Result;
using LiveLib.Application.Interfaces;
using LiveLib.Domain.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace LiveLib.Application.Features.Users.GetUserById
{
    public class GetUserByIdHandler : HandlerBase, IRequestHandler<GetUserByIdQuery, Result<User>>
    {
        public GetUserByIdHandler(IMapper mapper, IDatabaseContext context) : base(mapper, context)
        {
        }

        public async Task<Result<User>> Handle(GetUserByIdQuery request, CancellationToken cancellationToken)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Id == request.userId, cancellationToken);

            if (user == null)
                return Result<User>.Failure("User not found");

            return Result.Success(user);
        }
    }
}
