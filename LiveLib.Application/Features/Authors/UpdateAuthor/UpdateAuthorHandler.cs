using AutoMapper;
using LiveLib.Application.Commom.Result;
using LiveLib.Application.Interfaces;
using LiveLib.Application.Models.Authors;
using MediatR;

namespace LiveLib.Application.Features.Authors.UpdateAuthor
{
    public class UpdateAuthorHandler : HandlerBase, IRequestHandler<UpdateAuthorCommand, Result<AuthorDetailDto>>
    {
        public UpdateAuthorHandler(IMapper mapper, IDatabaseContext context) : base(mapper, context)
        {
        }

        public async Task<Result<AuthorDetailDto>> Handle(UpdateAuthorCommand request, CancellationToken cancellationToken)
        {
            var genre = await _context.Authors.FindAsync(request.Id, cancellationToken);

            if (genre == null)
            {
                return Result<AuthorDetailDto>.NotFound($"Author {request.Id} not found");
            }

            _mapper.Map(request.Author, genre);
            var updated = await _context.SaveChangesAsync(cancellationToken);

            return updated == 0 ? Result<AuthorDetailDto>.ServerError($"Author {request.Id} not updated")
                : Result.Success(_mapper.Map<AuthorDetailDto>(genre));
        }
    }
}
