using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogMailApp
{
    static class Program
    {
        [System.STAThreadAttribute()]
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "4.0.0.0")]
        static void Main(string[] args)
        {
            //UserDefault userDefault = UserDefault.Instance;

            //if (!userDefault.IsFirstUsing && new Cmd(args).ToBeContinued)
            //{
            App app = new App();

            app.StartupUri = new System.Uri("MainWindow.xaml", System.UriKind.Relative);
            app.MainWindow = new MainWindow();

            //app.StartupUri = new System.Uri("WarningWindow.xaml", System.UriKind.Relative);
            //app.MainWindow = new WarningWindow("Hello World");

            

            app.Run();
            //}
        }
    }
}
