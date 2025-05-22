﻿using AutoMapper;
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
	public class AuthorDetailDto : IMapWith<Author>
	{
		public string FirstName { get; set; } = string.Empty;

		public string SecondName { get; set; } = string.Empty;

		public string ThirdName { get; set; } = string.Empty;

		public string Biography { get; set; } = string.Empty;

		public List<BookDto> Books { get; set; } = [];

		public void Mapping(Profile profile)
		{
			profile.CreateMap<Author, AuthorDetailDto>()
				.ForMember(dest => dest.Books, opt => opt.MapFrom(src => src.Books));
		}
	}
}
