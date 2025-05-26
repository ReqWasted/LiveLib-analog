using AutoMapper;
using LiveLib.Application.Commom.Mapping;
using LiveLib.Application.Commom.Result;
using LiveLib.Application.Models.Books;
using LiveLib.Domain.Models;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace LiveLib.Application.Features.Books.CreateBook
{
	public class CreateBookCommand : IRequest<Result<BookDetailDto>>, IMapWith<Book>
	{
		public string Name { get; set; } = null!;
		public DateOnly PublicatedAt { get; set; }
		public int PageCount { get; set; }
		public string Description { get; set; } = null!;
		public double AverageRating { get; set; }
		public string Isbn { get; set; } = null!;
		public Guid GenreId { get; set; }
		public Guid BookPublisherId { get; set; }
		public Guid AuthorId { get; set; }

		public void Mapping(Profile profile)
		{
			profile.CreateMap<CreateBookCommand, Book>();
		}
	}
}
