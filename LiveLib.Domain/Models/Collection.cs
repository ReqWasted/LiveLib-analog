using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LiveLib.Domain.Models
{
    public class Collection
    {
        public Guid Id { get; set; }
        public string Title { get; set; } = string.Empty;

        public List<User> Users { get; set; } = [];

        public List<Book> Books { get; set; } = [];
    }
}
