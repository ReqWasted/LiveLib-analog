using AutoMapper;
using LiveLib.Application.Commom.Mapping;
using LiveLib.Application.Models.Books;
using LiveLib.Application.Models.Collections;
using LiveLib.Application.Models.Users;
using LiveLib.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LiveLib.Application.Models.Reviews
{
	public class ReviewDto : IMapWith<Review>
	{
		public Guid Id { get; set; }

		public UserDto User { get; set; } = null!;
		public BookDto Book { get; set; } = null!;
		public double Rate { get; set; }
		public bool IsRecommended { get; set; }

		public void Mapping(Profile profile)
		{
			profile.CreateMap<Review, ReviewDto>()
				.ForMember(dest => dest.User, opt => opt.MapFrom(src => src.User))
				.ForMember(dest => dest.Book, opt => opt.MapFrom(src => src.Book));
		}
	}
}
