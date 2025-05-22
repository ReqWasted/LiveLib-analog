using AutoMapper;
using LiveLib.Application.Commom.Mapping;
using LiveLib.Application.Models.Collections;
using LiveLib.Application.Models.Reviews;
using LiveLib.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LiveLib.Application.Models.Users
{
	public class UserDetailDto : IMapWith<User>
	{
		public string Name { get; set; } = string.Empty;

		public string Email { get; set; } = string.Empty;

		public string Role { get; set; } = string.Empty;

		public List<ReviewDto> Reviews { get; set; } = [];

		public List<CollectionDto> Collections { get; set; } = [];

		public void Mapping(Profile profile)
		{
			profile.CreateMap<User, UserDetailDto>()
				.ForMember(dest => dest.Reviews, opt => opt.MapFrom(src => src.Reviews))
				.ForMember(dest => dest.Collections, opt => opt.MapFrom(src => src.Collections));
		}
	}
}
