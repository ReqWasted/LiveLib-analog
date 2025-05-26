using AutoMapper;
using LiveLib.Application.Commom.Mapping;
using LiveLib.Application.Commom.Result;
using LiveLib.Application.Models.Reviews;
using LiveLib.Domain.Models;
using MediatR;
using System.Text.Json.Serialization;

namespace LiveLib.Application.Features.Reviews.CreateReview
{
	public class CreateReviewCommand : IRequest<Result<ReviewDto>>, IMapWith<Review>
	{
		[JsonIgnore]
		public Guid? UserId { get; set; }
		public Guid BookId { get; set; }
		public double Rate { get; set; }
		public string Comment { get; set; } = string.Empty;
		public bool IsRecommended { get; set; }

	}
}
