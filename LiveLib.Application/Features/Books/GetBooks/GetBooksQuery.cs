using LiveLib.Application.Models.Books;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LiveLib.Application.Features.Books.GetBooks
{
	public class GetBooksQuery : IRequest<ICollection<BookDto>>;
}
