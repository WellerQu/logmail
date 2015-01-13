using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using LogMailApp.Command;

namespace LogMailApp
{
    class CommandLineParser
    {
        public CommandLineParser(string[] args)
        {
            // -f -p -a -ui -d
            List<ICommand> commands = new List<ICommand>();
            Stack<string> stack = new Stack<string>();

            foreach (string str in args)
            {
                if (str.StartsWith("-"))
                {
                    // 是命令就入栈
                    stack.Push(str);
                }
                else
                {
                    // 是参数就出栈
                    string paramters = stack.Pop();
                    string cmdName = str;
                }
            }

            while (stack.Count > 0)
            {
                // 剩下的都是无参数的构造函数
                string cmdName = stack.Pop();
            }
        }

        public ICommand[] Commands { get; protected set; }

        private ICommand CreateCommand(string name, params string paramters)
        {
            ICommand cmd = null;

            if ("-a".Equals(name))
            {

                //cmd = new AppendLogCommand(
            }

            return cmd;
        }
    }
}
