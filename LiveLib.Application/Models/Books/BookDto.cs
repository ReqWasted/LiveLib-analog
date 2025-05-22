﻿using AutoMapper;
using LiveLib.Application.Commom.Mapping;
using LiveLib.Application.Models.Collections;
using LiveLib.Application.Models.Genres;
using LiveLib.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LiveLib.Application.Models.Books
{
	public class BookDto : IMapWith<Book>
	{
		public Guid Id { get; set; }
		public string Name { get; set; } = string.Empty;
		public double AverageRating { get; set; }
		public GenreDto Genre { get; set; } = null!;

		public void Mapping(Profile profile)
		{
			profile.CreateMap<Book, BookDto>()
				.ForMember(dest => dest.Genre, opt => opt.MapFrom(src => src.Genre));
		}
	}
}
