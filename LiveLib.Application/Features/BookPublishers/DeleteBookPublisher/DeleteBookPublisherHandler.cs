using AutoMapper;
using LiveLib.Application.Commom.Result;
using LiveLib.Application.Interfaces;
using LiveLib.Application.Models.BookPublishers;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace LiveLib.Application.Features.BookPublishers.DeleteBookPublisher
{
    public class DeleteBookPublisherHandler : HandlerBase, IRequestHandler<DeleteBookPublisherCommand, Result<BookPublisherDetailDto>>
    {
        public DeleteBookPublisherHandler(IMapper mapper, IDatabaseContext context) : base(mapper, context)
        {
        }

        public async Task<Result<BookPublisherDetailDto>> Handle(DeleteBookPublisherCommand request, CancellationToken cancellationToken)
        {
            var book = await _context.BookPublishers.FirstOrDefaultAsync(b => b.Id == request.Id);
            if (book == null)
            {
                return Result<BookPublisherDetailDto>.NotFound($"Book {request.Id} not found");
            }

            _context.BookPublishers.Remove(book);
            var removed = await _context.SaveChangesAsync(cancellationToken);

            return removed == 0 ? Result<BookPublisherDetailDto>.NotFound($"Book {request.Id} not deleted")
                : Result.Success(_mapper.Map<BookPublisherDetailDto>(book));
        }
    }
}
