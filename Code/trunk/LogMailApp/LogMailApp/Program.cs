using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using LogMailApp.Command;
using LogMailApp.Preference;

namespace LogMailApp
{
    static class Program
    {
        //[System.STAThreadAttribute()]
        //[System.Diagnostics.DebuggerNonUserCodeAttribute()]
        //[System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "4.0.0.0")]
        static void Main(string[] args)
        {
            CommandLineParser parser = new CommandLineParser(args);
            ICommand[] cmds = parser.Commands;

            Executor executor = new Executor(cmds);
            executor.Run();
        }
    }
}
