using AutoMapper;
using LiveLib.Application.Commom.Mapping;
using LiveLib.Application.Commom.Result;
using LiveLib.Application.Models.Authors;
using LiveLib.Domain.Models;
using MediatR;

namespace LiveLib.Application.Features.Authors.CreateAuthor
{
    public class CreateAuthorCommand : IRequest<Result<AuthorDetailDto>>, IMapWith<Author>
    {
        public string FirstName { get; set; } = string.Empty;

        public string SecondName { get; set; } = string.Empty;

        public string ThirdName { get; set; } = string.Empty;

        public string Biography { get; set; } = string.Empty;

        public void Mapping(Profile profile)
        {
            profile.CreateMap<CreateAuthorCommand, Author>();
        }
    }
}
