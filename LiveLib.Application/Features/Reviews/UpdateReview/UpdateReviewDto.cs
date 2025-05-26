using AutoMapper;
using LiveLib.Application.Commom.Mapping;
using LiveLib.Application.Features.Books.UpdateBook;
using LiveLib.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace LiveLib.Application.Features.Reviews.UpdateReview
{
	public class UpdateReviewDto : IMapWith<Review>
	{
		public double? Rate { get; set; }
		public string? Comment { get; set; }
		public bool? IsRecommended { get; set; }

		public void Mapping(Profile profile)
		{
			profile.CreateMap<UpdateReviewDto, Review>()
				.ForAllMembers(opts =>
				{
					opts.Condition((src, dest, srcMember, destMember, context) =>
					{
						if (srcMember == null) return false;

						var memberType = srcMember.GetType();
						if (memberType.IsValueType && !memberType.IsEnum)
						{
							var defaultValue = RuntimeHelpers.GetUninitializedObject(memberType);
							return !srcMember.Equals(defaultValue);
						}

						return true;
					}
					   );
				});

		}
	}
}
