using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using LiveLib.Application.Commom.Mapping;
using LiveLib.Application.Commom.Result;
using LiveLib.Application.Models.Reviews;
using LiveLib.Domain.Models;
using MediatR;

namespace LiveLib.Application.Features.Reviews.CreateReview
{
    public class CreateReviewCommand : IRequest<Result<Review>>, IMapWith<Review>
    {
        public Guid? UserId { get; set; }
        public Guid BookId { get; set; }
        public double Rate { get; set; }
        public string Comment { get; set; } = string.Empty;
        public bool IsRecommended { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Review, CreateReviewCommand>();
        }
    }
}
