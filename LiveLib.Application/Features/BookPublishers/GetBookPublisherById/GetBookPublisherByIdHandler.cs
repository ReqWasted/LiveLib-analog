using AutoMapper;
using LiveLib.Application.Commom.Result;
using LiveLib.Application.Interfaces;
using LiveLib.Application.Models.BookPublishers;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace LiveLib.Application.Features.BookPublishers.GetBookPublisherById
{
    public class GetBookPublisherByIdHandler : HandlerBase, IRequestHandler<GetBookPublisherByIdQuery, Result<BookPublisherDetailDto>>
    {
        public GetBookPublisherByIdHandler(IMapper mapper, IDatabaseContext context) : base(mapper, context)
        {
        }

        public async Task<Result<BookPublisherDetailDto>> Handle(GetBookPublisherByIdQuery request, CancellationToken cancellationToken)
        {
            var genre = await _context.BookPublishers.FirstOrDefaultAsync(g => g.Id == request.Id, cancellationToken);

            return genre is null ? Result<BookPublisherDetailDto>.NotFound($"BookPublisher {request.Id} not found") :
                Result.Success(_mapper.Map<BookPublisherDetailDto>(genre));
        }
    }
}
