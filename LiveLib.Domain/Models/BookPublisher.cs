using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LiveLib.Domain.Models
{
	public class BookPublisher
	{
		public Guid Id { get; set; }
		public string Name {  get; set; } = string.Empty;
		public string Description { get; set; } = string.Empty;

		public List<Book> Books { get; set; } = [];
	}
}
