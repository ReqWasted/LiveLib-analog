using AutoMapper;
using LiveLib.Application.Commom.Result;
using LiveLib.Application.Interfaces;
using LiveLib.Application.Models.Authors;
using LiveLib.Domain.Models;
using MediatR;

namespace LiveLib.Application.Features.Authors.CreateAuthor
{
    public class CreateAuthorHandler : HandlerBase, IRequestHandler<CreateAuthorCommand, Result<AuthorDetailDto>>
    {
        public CreateAuthorHandler(IMapper mapper, IDatabaseContext context) : base(mapper, context)
        {
        }

        public async Task<Result<AuthorDetailDto>> Handle(CreateAuthorCommand request, CancellationToken cancellationToken)
        {
            var author = _context.Authors.Add(_mapper.Map<Author>(request));
            var saved = await _context.SaveChangesAsync(cancellationToken);

            return saved == 0 ? Result<AuthorDetailDto>.ServerError() : Result<AuthorDetailDto>.NoContent();
        }
    }
}
