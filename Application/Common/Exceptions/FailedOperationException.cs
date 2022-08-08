using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Common.Exceptions
{
    public class FailedOperationException: Exception
    {
        public FailedOperationException():base()
        {

        }
        public FailedOperationException(string message):base(message)
        {

        }
        public FailedOperationException(string operation,string entityname, string errorMessage) : base($"failed to perform {operation} on entity - {entityname}  : {errorMessage}")
        {
          
        }
      
    }
}
