using AutoMapper;
using LiveLib.Application.Commom.Mapping;
using LiveLib.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LiveLib.Application.Models.Users
{
	public class UserDto : IMapWith<User>
	{
		public Guid Id { get; set; }

		public string Name { get; set; } = string.Empty;

		public string Email { get; set; } = string.Empty;

		public string Role { get; set; } = string.Empty;

		public void Mapping(Profile profile)
		{
			profile.CreateMap<User, UserDto>();
		}
	}
}
