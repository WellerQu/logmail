using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogMailApp.Exception
{
    class UserInformationDeficiencyException : ExceptionBase
    {
        public UserInformationDeficiencyException(string message)
            : base(message)
        { }
    }
}
