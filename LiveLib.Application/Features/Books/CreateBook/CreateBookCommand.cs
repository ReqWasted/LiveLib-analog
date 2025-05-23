using AutoMapper;
using LiveLib.Application.Commom.Mapping;
using LiveLib.Application.Commom.Result;
using LiveLib.Domain.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LiveLib.Application.Features.Books.CreateBook
{
    public class CreateBookCommand : IRequest<Result<int>>, IMapWith<Book>
    {
        public string Name { get; set; } = null!;
        public DateOnly PublicatedAt { get; set; }
        public int PageCount { get; set; }
        public string Description { get; set; } = null!;
        public double AverageRating { get; set; }
        public string Isbn { get; set; } = null!;
        public Guid GenreId { get; set; }
        public Guid BookPublisherId { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Book, CreateBookCommand>();
        }
    }
}
