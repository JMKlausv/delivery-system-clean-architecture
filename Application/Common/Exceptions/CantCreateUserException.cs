using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.common.Exceptions
{
    public class CantCreateUserException: Exception
    {
        public CantCreateUserException():base()
        {
           
        }

        public CantCreateUserException(
            string
            message):base(message)
        {
          
        }
      

    }
}
