using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogMailApp.Exception
{
    class PostErrorException : ExceptionBase
    {
        public PostErrorException(string message)
            : base(message)
        { }
    }
}
