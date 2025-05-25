using AutoMapper;
using LiveLib.Application.Commom.Result;
using LiveLib.Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace LiveLib.Application.Features.Users.DeleteUser
{
    public class DeleteUserHandler : HandlerBase, IRequestHandler<DeleteUserCommand, Result>
    {
        public DeleteUserHandler(IMapper mapper, IDatabaseContext context) : base(mapper, context)
        {
        }

        public async Task<Result> Handle(DeleteUserCommand request, CancellationToken cancellationToken)
        {
            //var deleted = await _context.Users.Where(u => u.Id == request.UserId).ExecuteDeleteAsync(cancellationToken);
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Id == request.UserId, cancellationToken);

            if (user == null)
            {
                return Result.Failure("User not found");
            }

            var entity = _context.Users.Remove(user);
            await _context.SaveChangesAsync(cancellationToken);

            if (entity.Entity == null)
            {
                return Result.Failure("User not deleted");
            }
            await _context.SaveChangesAsync(cancellationToken);

            //if (deleted == 0)
            //{
            //	return Result<int>.Failure("User not deleted");
            //}
            return Result.Success();
        }
    }
}
