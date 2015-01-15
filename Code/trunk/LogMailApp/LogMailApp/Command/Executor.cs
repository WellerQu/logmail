using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace LogMailApp.Command
{
    class Executor
    {
        public Executor(ICommand[] commands)
        {
            this.Commands = commands;
        }

        protected ICommand[] Commands { get; set; }

        public void Run()
        {
            if (this.Commands != null)
            {
                foreach (var cmd in this.Commands)
                {
                    if (cmd.CanExecute(null))
                    {
                        cmd.Execute(null);
                    }
                }

                Console.WriteLine("All commands are executed.");
            }
        }
    }
}
