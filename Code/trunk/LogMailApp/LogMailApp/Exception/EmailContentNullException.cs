using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogMailApp.Exception
{
    class EmailContentNullException : ExceptionBase
    {
        public EmailContentNullException(string message)
            : base(message)
        {
        }
    }
}
