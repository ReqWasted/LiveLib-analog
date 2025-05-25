using AutoMapper;
using LiveLib.Application.Commom.Result;
using LiveLib.Application.Interfaces;
using LiveLib.Application.Models.Genres;
using LiveLib.Domain.Models;
using MediatR;

namespace LiveLib.Application.Features.Genres.CreateGenre
{
    public class CreateGenreHandler : HandlerBase, IRequestHandler<CreateGenreCommand, Result<GenreDetailDto>>
    {
        public CreateGenreHandler(IMapper mapper, IDatabaseContext context) : base(mapper, context)
        {

        }

        public async Task<Result<GenreDetailDto>> Handle(CreateGenreCommand request, CancellationToken cancellationToken)
        {
            var genre = _context.Genres.Add(_mapper.Map<Genre>(request));
            var saved = await _context.SaveChangesAsync(cancellationToken);

            return saved == 0 ? Result<GenreDetailDto>.ServerError() : Result<GenreDetailDto>.NoContent();
        }
    }
}
