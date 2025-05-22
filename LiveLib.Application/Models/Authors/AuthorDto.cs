using AutoMapper;
using LiveLib.Application.Commom.Mapping;
using LiveLib.Application.Models.Books;
using LiveLib.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LiveLib.Application.Models.Authors
{
	public class AuthorDto : IMapWith<Author>
	{
		public Guid Id { get; set; }

		public string FirstName { get; set; } = string.Empty;

		public string SecondName { get; set; } = string.Empty;

		public string ThirdName { get; set; } = string.Empty;
		public void Mapping(Profile profile)
		{
			profile.CreateMap<Author, AuthorDto>();
		}

	}
}
