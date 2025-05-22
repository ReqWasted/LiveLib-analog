using LiveLib.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LiveLib.Application.Features.Books.CreateBook
{
	public record CreateBookDto(string Name, DateOnly PublicatedAt, int PageCount, string Description, double AverageRating, string Isbn, Guid GenreId, Guid BookPublisherId);

}
