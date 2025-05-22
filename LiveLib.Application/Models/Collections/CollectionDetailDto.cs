using AutoMapper;
using LiveLib.Application.Commom.Mapping;
using LiveLib.Application.Models.Books;
using LiveLib.Application.Models.Users;
using LiveLib.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LiveLib.Application.Models.Collections
{
	public class CollectionDetailDto : IMapWith<Collection>
	{
		public string Title { get; set; } = string.Empty;

		public List<UserDto> Users { get; set; } = [];

		public List<BookDto> Books { get; set; } = [];

		public void Mapping(Profile profile)
		{
			profile.CreateMap<Collection, CollectionDetailDto>()
				.ForMember(dest => dest.Users, opt => opt.MapFrom(src => src.Users))
				.ForMember(dest => dest.Books, opt => opt.MapFrom(src => src.Books));
		}
	}
}
