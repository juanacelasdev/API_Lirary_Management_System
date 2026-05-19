using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Domain.Exceptions
{
    public class LoanLimitExceededException : Exception
    {
        public LoanLimitExceededException() 
        : base ("The user exceeded the allowed limit.") 
            {}
    }
}
