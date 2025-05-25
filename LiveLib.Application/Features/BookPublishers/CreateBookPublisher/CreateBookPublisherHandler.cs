using AutoMapper;
using LiveLib.Application.Commom.Result;
using LiveLib.Application.Interfaces;
using LiveLib.Application.Models.BookPublishers;
using LiveLib.Domain.Models;
using MediatR;

namespace LiveLib.Application.Features.BookPublishers.CreateBookPublisher
{
    public class CreateBookPublisherHandler : HandlerBase, IRequestHandler<CreateBookPublisherCommand, Result<BookPublisherDetailDto>>
    {
        public CreateBookPublisherHandler(IMapper mapper, IDatabaseContext context) : base(mapper, context)
        {
        }

        public async Task<Result<BookPublisherDetailDto>> Handle(CreateBookPublisherCommand request, CancellationToken cancellationToken)
        {
            var book = _context.BookPublishers.Add(_mapper.Map<BookPublisher>(request));
            var saved = await _context.SaveChangesAsync(cancellationToken);

            return saved == 0 ? Result<BookPublisherDetailDto>.ServerError() : Result<BookPublisherDetailDto>.NoContent();
        }
    }
}
