using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogMailApp.Exception
{
    abstract class ExceptionBase : System.Exception
    {
        // 摘要:
        //     初始化 System.Exception 类的新实例。
        public ExceptionBase()
            : base()
        {
        }
        //
        // 摘要:
        //     使用指定的错误信息初始化 System.Exception 类的新实例。
        //
        // 参数:
        //   message:
        //     描述错误的消息。
        public ExceptionBase(string message)
            : base(message)
        {
        }

        //
        // 摘要:
        //     使用指定错误消息和对作为此异常原因的内部异常的引用来初始化 System.Exception 类的新实例。
        //
        // 参数:
        //   message:
        //     解释异常原因的错误信息。
        //
        //   innerException:
        //     导致当前异常的异常；如果未指定内部异常，则是一个 null 引用（在 Visual Basic 中为 Nothing）。
        public ExceptionBase(string message, System.Exception innerException)
            : base(message, innerException)
        {
        }
    }
}
