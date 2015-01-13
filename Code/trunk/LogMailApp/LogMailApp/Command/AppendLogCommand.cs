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
        public const string LOG_NAME_KEY = "Log.Key";
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

            List<string> lines = new List<string>();

            LogDocument doc = new LogDocument();

            string[] content = doc.Load(name);
            if (content != null)
                lines.AddRange(content);

            lines.Add(cont);

            doc.Save(name, lines.ToArray());
        }
    }
}
