using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LogMailApp.Preference;

namespace LogMailApp.Command
{
    /// <summary>
    /// 打开UI窗口
    /// </summary>
    class UICommand : CommandBase
    {
        private UserData UserData;
        private const string WIN_TYPE_KEY = "Window.Type";
        private const string WIN_CONT_KEY = "Window.Content";

        public UICommand(UserData userData)
        {
            this.UserData = userData;
        }

        public override void Execute(object parameter)
        {
            App app = new App();

            app.MainWindow = new MainWindow();

            if (this.UserData != null && this.UserData[WIN_CONT_KEY] != null)
            {
                string message = this.UserData[WIN_CONT_KEY] as string;
                app.MainWindow = new WarningWindow(message);
            }

            app.MainWindow.Show();

            app.Run();
        }
    }
}
