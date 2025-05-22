using AutoMapper;
using LiveLib.Application.Commom.Mapping;
using LiveLib.Application.Models.Books;
using LiveLib.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LiveLib.Application.Models.BookPublishers
{
	public class BookPublisherDto : IMapWith<BookPublisher>
	{
		public Guid Id { get; set; }
		public string Name { get; set; } = string.Empty;

		public void Mapping(Profile profile)
		{
			profile.CreateMap<BookPublisher, BookPublisherDto>();
		}
	}
}
