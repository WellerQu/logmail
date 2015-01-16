using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogMailApp.Communication
{
    interface IPoster
    {
        bool WillStopOnError { get; }
        void Ready(UserData userData);
        void Post(UserData userData);
    }
}
