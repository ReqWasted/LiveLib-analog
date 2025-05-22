using AutoMapper;
using LiveLib.Application.Commom.Mapping;
using LiveLib.Application.Models.Users;
using LiveLib.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LiveLib.Application.Models.Collections
{
	public class CollectionDto : IMapWith<Collection>
	{
		public Guid Id { get; set; }
		public string Title { get; set; } = string.Empty;

		public void Mapping(Profile profile)
		{
			profile.CreateMap<Collection, CollectionDto>();
		}
	}
}
