using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Domain.Exceptions
{
    public class BookNotAvailableException : Exception
    {
        public BookNotAvailableException() 
            : base("The book is not available. ")
        { 
        }   
    }
}
