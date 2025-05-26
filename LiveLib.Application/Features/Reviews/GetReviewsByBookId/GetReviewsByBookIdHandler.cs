using AutoMapper;
using AutoMapper.QueryableExtensions;
using LiveLib.Application.Interfaces;
using LiveLib.Application.Models.Collections;
using LiveLib.Application.Models.Reviews;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LiveLib.Application.Features.Reviews.GetReviewsByBookId
{
	public class GetReviewsByBookIdHandler : HandlerBase, IRequestHandler<GetReviewsByBookIdQuery, ICollection<ReviewDto>>
	{
		public GetReviewsByBookIdHandler(IMapper mapper, IDatabaseContext context) : base(mapper, context)
		{
		}

		public async Task<ICollection<ReviewDto>> Handle(GetReviewsByBookIdQuery request, CancellationToken cancellationToken)
		{
			return await _context.Collections
				.AsNoTracking()
				.Where(c => c.OwnerUserId == request.Id)
				.ProjectTo<ReviewDto>(_mapper.ConfigurationProvider)
				.ToListAsync(cancellationToken);
		}
	}
}
