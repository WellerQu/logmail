using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LogMailApp.Storage;

namespace LogMailApp.Command
{
    /// <summary>
    /// 追加日志
    /// </summary>
    class AppendLogCommand : CommandBase
    {
        public const string LOG_NAME_KEY = "Log.PrimaryKey";
        public const string LOG_CONT_KEY = "Log.Content";

        private UserData UserData = null;

        public AppendLogCommand(UserData userData)
        {
            this.UserData = userData;
        }

        public override void Execute(object parameter)
        {
            string name = this.UserData[LOG_NAME_KEY] as string;
            string cont = this.UserData[LOG_CONT_KEY] as string;

            LogDocument doc = new LogDocument();
            string content = doc.Load(name);

            StringBuilder sbContent = new StringBuilder(content.TrimEnd('\r', '\n'));

            if (!string.IsNullOrEmpty(cont))
            {
                sbContent.AppendLine();
                sbContent.AppendLine(string.Format("{0}[{1}]", cont, DateTime.Now.ToString("HH:mm")));
                doc.Save(name, sbContent.ToString());
            }
        }
    }
}
