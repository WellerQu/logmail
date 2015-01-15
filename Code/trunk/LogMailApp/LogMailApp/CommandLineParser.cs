using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using LogMailApp.Command;
using LogMailApp.Exception;

namespace LogMailApp
{
    /// <summary>
    /// 命令行参数解析器
    /// </summary>
    class CommandLineParser
    {
        public CommandLineParser(string[] args)
        {
            // -f -p -a -ui
            List<CommandBase> commands = new List<CommandBase>();
            Stack<string> stack = new Stack<string>();

            foreach (string str in args)
            {
                if (!string.IsNullOrEmpty(str.Trim()))
                {
                    Console.WriteLine("params: " + str);

                    if (str.StartsWith("-"))
                    {
                        // 是命令就入栈
                        stack.Push(str);
                    }
                    else
                    {
                        // 是参数把命令就出栈
                        string cmdName = stack.Pop();
                        string parameter = str;

                        CommandBase cmd = this.CreateCommand(cmdName, parameter);

                        if (cmd != null)
                            commands.Add(cmd);
                    }
                }
            }

            while (stack != null && stack.Count > 0)
            {
                // 剩下的都是无参数的构造函数
                string cmdName = stack.Pop();
                CommandBase cmd = this.CreateCommand(cmdName, null);

                if (cmd != null)
                    commands.Add(cmd);
            }

            this.Commands = commands.OrderBy(obj => obj.Order).ToArray();

            Console.WriteLine("Collect all the commands!");
        }

        public CommandBase[] Commands { get; protected set; }

        protected virtual CommandBase CreateCommand(string name, string paramter)
        {
            CommandBase cmd = null;
            UserData userData = new UserData();

            if ("-a".Equals(name))
            {
                userData[AppendLogCommand.LOG_NAME_KEY] = DateTime.Now.ToString("yyyy-MM-dd");
                userData[AppendLogCommand.LOG_CONT_KEY] = paramter;

                cmd = new AppendLogCommand(userData);
                cmd.Order = 0;
            }
            else if ("-p".Equals(name))
            {
                cmd = new PostLogCommand(null);
                cmd.Order = 1;
            }
            else if ("-f".Equals(name))
            {
                cmd = new FileLogCommand();
                cmd.Order = 2;
            }
            else if ("-ui".Equals(name))
            {
                cmd = new UICommand(null);
                cmd.Order = 3;
            }
            else
            {
                throw new UndefinedCommandException("出现没有定义的参数: " + name);
            }

            return cmd;
        }
    }
}
