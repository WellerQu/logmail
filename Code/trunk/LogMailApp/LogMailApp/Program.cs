using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogMailApp
{
    static class Program
    {
        [STAThread]
        static void Main(string[] args)
        {
            // 得到 -ui 命令才打开可视窗口
            App app = new App();
            app.InitializeComponent();
            app.MainWindow = new MainWindow();
            app.Run();

            // 否则执行cmd
            Cmd cmd = new Cmd();
            cmd.Execute();
        }
    }
}
