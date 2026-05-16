using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Domain.Entities
{
    public class Author
    {
        public int id { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }
        public DateTime DateofBirth { get; set; }
        public ICollection<Book> Books { get; set; }
    }
}
