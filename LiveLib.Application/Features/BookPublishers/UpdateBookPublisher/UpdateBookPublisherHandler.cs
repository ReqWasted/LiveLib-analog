using AutoMapper;
using LiveLib.Application.Commom.Result;
using LiveLib.Application.Interfaces;
using LiveLib.Application.Models.BookPublishers;
using MediatR;

namespace LiveLib.Application.Features.BookPublishers.UpdateBookPublisher
{
    public class UpdateBookPublisherHandler : HandlerBase, IRequestHandler<UpdateBookPublisherCommand, Result<BookPublisherDetailDto>>
    {
        public UpdateBookPublisherHandler(IMapper mapper, IDatabaseContext context) : base(mapper, context)
        {
        }

        public async Task<Result<BookPublisherDetailDto>> Handle(UpdateBookPublisherCommand request, CancellationToken cancellationToken)
        {
            var bookPublisher = await _context.BookPublishers.FindAsync(request.Id, cancellationToken);

            if (bookPublisher == null)
            {
                return Result<BookPublisherDetailDto>.NotFound($"Book {request.Id} not found");
            }

            _mapper.Map(request.BookPublisher, bookPublisher);
            var updated = await _context.SaveChangesAsync(cancellationToken);

            return updated == 0 ? Result<BookPublisherDetailDto>.ServerError($"Book {request.Id} not updated")
                : Result.Success(_mapper.Map<BookPublisherDetailDto>(bookPublisher));
        }
    }
}
