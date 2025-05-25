using LiveLib.Application.Commom.Mapping;
using LiveLib.Application.Commom.Result;
using LiveLib.Application.Models.Genres;
using LiveLib.Domain.Models;
using MediatR;

namespace LiveLib.Application.Features.Genres.CreateGenre
{
    public class CreateGenreCommand : IRequest<Result<GenreDetailDto>>, IMapWith<Genre>
    {
        public string Name { get; set; } = null!;
        public int AgeRestriction { get; set; }
    }
}
