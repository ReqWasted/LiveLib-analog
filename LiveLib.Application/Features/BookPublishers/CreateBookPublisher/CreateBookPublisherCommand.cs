using AutoMapper;
using LiveLib.Application.Commom.Mapping;
using LiveLib.Application.Commom.Result;
using LiveLib.Application.Models.BookPublishers;
using LiveLib.Domain.Models;
using MediatR;

namespace LiveLib.Application.Features.BookPublishers.CreateBookPublisher
{
    public class CreateBookPublisherCommand : IRequest<Result<BookPublisherDetailDto>>, IMapWith<BookPublisher>
    {
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public void Mapping(Profile profile)
        {
            profile.CreateMap<CreateBookPublisherCommand, BookPublisher>();
        }
    }
}
