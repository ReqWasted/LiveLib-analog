using AutoMapper;
using LiveLib.Application.Commom.Result;
using LiveLib.Application.Interfaces;
using LiveLib.Application.Models.Genres;
using MediatR;

namespace LiveLib.Application.Features.Genres.UpdateGenre
{
    public class UpdateGenreHandler : HandlerBase, IRequestHandler<UpdateGenreCommand, Result<GenreDetailDto>>
    {
        public UpdateGenreHandler(IMapper mapper, IDatabaseContext context) : base(mapper, context)
        {
        }

        public async Task<Result<GenreDetailDto>> Handle(UpdateGenreCommand request, CancellationToken cancellationToken)
        {
            var genre = await _context.Genres.FindAsync(request.Id, cancellationToken);

            if (genre == null)
            {
                return Result<GenreDetailDto>.NotFound($"Genre {request.Id} not found");
            }

            _mapper.Map(request.Genre, genre);
            //_context.Genres.Entry(genre).CurrentValues.SetValues(request.Genre);
            var updated = await _context.SaveChangesAsync(cancellationToken);

            return updated == 0 ? Result<GenreDetailDto>.ServerError($"Genre {request.Id} not updated") : Result.Success(_mapper.Map<GenreDetailDto>(genre));
        }
    }
}
