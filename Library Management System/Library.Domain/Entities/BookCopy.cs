using Library.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Domain.Entities
{
    public class BookCopy
    {
        public int Id { get; set; }
        public string InventoryCode { get; set; }
        public BookCopyStatus Status { get; set; }
        public bool IsActive { get; set; }
        public int BookId { get; set; }
        public Book Book { get; set; }
    }
}
