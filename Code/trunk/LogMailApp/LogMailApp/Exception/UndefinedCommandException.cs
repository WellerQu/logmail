using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogMailApp.Exception
{
    /// <summary>
    /// 未定义的命令
    /// </summary>
    class UndefinedCommandException : ExceptionBase
    {
        public UndefinedCommandException(string message)
            : base(message)
        {
        }
    }
}
