using AutoMapper;
using LiveLib.Application.Commom.Result;
using LiveLib.Application.Interfaces;
using LiveLib.Application.Models.Books;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace LiveLib.Application.Features.Books.GetBookById
{
    public class GetBookByIdHandler : HandlerBase, IRequestHandler<GetBookByIdQuery, Result<BookDetailDto>>
    {
        public GetBookByIdHandler(IMapper mapper, IDatabaseContext context) : base(mapper, context)
        {
        }

        public async Task<Result<BookDetailDto>> Handle(GetBookByIdQuery request, CancellationToken cancellationToken)
        {
            var book = await _context.Books
                .Include(b => b.Author)
                .Include(b => b.BookPublisher)
                .Include(b => b.Genre)
                .FirstOrDefaultAsync(g => g.Id == request.Id, cancellationToken);

            return book is null ? Result<BookDetailDto>.NotFound($"Book {request.Id} not found") :
                Result.Success(_mapper.Map<BookDetailDto>(book));
        }
    }
}
