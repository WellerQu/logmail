using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LogMailApp.Storage;

namespace LogMailApp.Command
{
    /// <summary>
    /// 归档
    /// </summary>
    class FileLogCommand : CommandBase
    {
        public FileLogCommand()
        {
        }

        public override void Execute(object parameter)
        {
            LogDocument doc = new LogDocument();
            doc.File();
        }
    }
}
