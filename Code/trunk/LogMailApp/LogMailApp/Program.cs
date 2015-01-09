using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LogMailApp.Preference;

namespace LogMailApp
{
    static class Program
    {
        [System.STAThreadAttribute()]
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "4.0.0.0")]
        static void Main(string[] args)
        {
            UserDefault userDefault = UserDefault.Instance;

            //if (!userDefault.IsFirstUsing && new Cmd(args).ToBeContinued)
            //{
            App app = new App();

            app.MainWindow = new MainWindow(userDefault.IsFirstUsing);
            //app.MainWindow = new WarningWindow("Hello World");

            app.MainWindow.Show();

            app.Run();
            //}
        }
    }
}
