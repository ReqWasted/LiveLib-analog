using AutoMapper;
using LiveLib.Application.Commom.Result;
using LiveLib.Application.Interfaces;
using LiveLib.Application.Models.Books;
using LiveLib.Domain.Models;
using MediatR;

namespace LiveLib.Application.Features.Books.CreateBook
{
	public class CreateBookHandler : HandlerBase, IRequestHandler<CreateBookCommand, Result<BookDetailDto>>
	{

		public CreateBookHandler(IMapper mapper, IDatabaseContext context) : base(mapper, context)
		{
		}

		public async Task<Result<BookDetailDto>> Handle(CreateBookCommand request, CancellationToken cancellationToken)
		{
			var book = _mapper.Map<Book>(request);

			_context.Books.Add(book);
			await _context.SaveChangesAsync(cancellationToken);

			return Result<BookDetailDto>.NoContent();
		}

	}
}
