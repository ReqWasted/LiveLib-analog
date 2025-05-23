using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LiveLib.Domain.Models
{
	public class Review
	{
		public Guid Id { get; set; }

		public Guid UserId { get; set; }

		public User User { get; set; } = null!;
		public Guid BookId { get; set; }

		public Book Book { get; set; } = null!;
		public double Rate { get; set; }
		public string Comment { get; set; } = string.Empty;
		public bool IsRecommended	{ get; set; }
	}
}
