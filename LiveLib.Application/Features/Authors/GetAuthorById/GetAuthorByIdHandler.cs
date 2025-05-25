using AutoMapper;
using LiveLib.Application.Commom.Result;
using LiveLib.Application.Interfaces;
using LiveLib.Application.Models.Authors;
using MediatR;

namespace LiveLib.Application.Features.Authors.GetAuthorById
{
    public class GetAuthorByIdHandler : HandlerBase, IRequestHandler<GetAuthorByIdQuery, Result<AuthorDetailDto>>
    {
        public GetAuthorByIdHandler(IMapper mapper, IDatabaseContext context) : base(mapper, context)
        {
        }

        public async Task<Result<AuthorDetailDto>> Handle(GetAuthorByIdQuery request, CancellationToken cancellationToken)
        {
            var author = await _context.Authors.FindAsync(request.Id, cancellationToken);

            return author is null ? Result<AuthorDetailDto>.NotFound($"Genre {request.Id} not found") :
                Result.Success(_mapper.Map<AuthorDetailDto>(author));
        }
    }
}
