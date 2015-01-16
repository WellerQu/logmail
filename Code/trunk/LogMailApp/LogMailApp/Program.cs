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
        [System.STAThreadAttribute()]
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "4.0.0.0")]
        static void Main(string[] args)
        {
            Console.WriteLine("Hello LogMail");
            //Console.WriteLine(AppDomain.CurrentDomain.BaseDirectory);
            //Console.WriteLine(Environment.CurrentDirectory);
            //Console.WriteLine(UserDefault.Instance.StartupPath);

            try
            {
                if (args.Length == 0 || UserDefault.Instance.IsFirstUsing)
                {
                    App app = new App();
                    app.MainWindow = new MainWindow();
                    app.MainWindow.Show();
                    app.Run();
                }
                else
                {
                    CommandLineParser parser = new CommandLineParser(args);
                    ICommand[] cmds = parser.Commands;

                    Executor executor = new Executor(cmds);
                    executor.Run();
                }
            }
            catch (System.Exception ex)
            {
                string message = "Error";
                while (ex != null)
                {
                    message += ", " + ex.Message;
#if DEBUG
                    message += ", " + ex.StackTrace.ToString();
#endif
                    ex = ex.InnerException;
                }

                Console.WriteLine(string.Format("[{0}]: {1}", DateTime.Now, message));

                App app = new App();
                app.MainWindow = new WarningWindow(message);
                app.MainWindow.Show();
                app.Run();
            }
        }
    }
}
