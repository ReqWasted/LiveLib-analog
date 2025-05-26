using AutoMapper;
using AutoMapper.QueryableExtensions;
using LiveLib.Application.Commom.Result;
using LiveLib.Application.Interfaces;
using LiveLib.Application.Models.Genres;
using LiveLib.Application.Models.Reviews;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LiveLib.Application.Features.Reviews.GetReviewById
{
	public class GetReviewByIdHandler : HandlerBase, IRequestHandler<GetReviewByIdQuery, Result<ReviewDetatilDto>>
	{
		public GetReviewByIdHandler(IMapper mapper, IDatabaseContext context) : base(mapper, context)
		{
		}

		public async Task<Result<ReviewDetatilDto>> Handle(GetReviewByIdQuery request, CancellationToken cancellationToken)
		{
			var genre = await _context.Reviews
				.Where(r => r.Id == request.Id)
				.ProjectTo<ReviewDetatilDto>(_mapper.ConfigurationProvider)
				.FirstOrDefaultAsync(cancellationToken);

			return genre is null ? Result<ReviewDetatilDto>.NotFound($"Review {request.Id} not found") :
				Result.Success(_mapper.Map<ReviewDetatilDto>(genre));
		}
	}
}
