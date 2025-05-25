using AutoMapper;
using LiveLib.Application.Commom.Result;
using LiveLib.Application.Interfaces;
using LiveLib.Application.Models.Books;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace LiveLib.Application.Features.Books.UpdateBook
{
    public class UpdateBookHandler : HandlerBase, IRequestHandler<UpdateBookCommand, Result<BookDetailDto>>
    {
        public UpdateBookHandler(IMapper mapper, IDatabaseContext context) : base(mapper, context)
        {
        }

        public async Task<Result<BookDetailDto>> Handle(UpdateBookCommand request, CancellationToken cancellationToken)
        {
            var book = await _context.Books
                .Include(b => b.Author)
                .Include(b => b.BookPublisher)
                .Include(b => b.Genre)
                .FirstOrDefaultAsync(g => g.Id == request.Id, cancellationToken);

            var isNull = request.Book.PageCount == null;

            if (book == null)
            {
                return Result<BookDetailDto>.NotFound($"Book {request.Id} not found");
            }

            _mapper.Map(request.Book, book);
            var updated = await _context.SaveChangesAsync(cancellationToken);

            return updated == 0 ? Result<BookDetailDto>.ServerError($"Book {request.Id} not updated")
                : Result.Success(_mapper.Map<BookDetailDto>(book));
        }
    }
}
