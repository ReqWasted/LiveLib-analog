using LiveLib.Application.Commom.Result;
using LiveLib.Application.Models.Genres;
using MediatR;

namespace LiveLib.Application.Features.Genres.GetGenreById
{
    public record GetGenreByIdQuery(Guid Id) : IRequest<Result<GenreDetailDto>>;
}
