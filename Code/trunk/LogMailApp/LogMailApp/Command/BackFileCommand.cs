using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LogMailApp.Storage;

namespace LogMailApp.Command
{
    class BackFileCommand : EditorPanelCommand
    {
        public BackFileCommand(UserData userData)
            : base(userData)
        { }

        public override void Execute(object parameter)
        {
            if (this.ViewModel != null)
            {
                string name = this.ViewModel.SelectedDate.Value.ToString("yyyy-MM-dd");

                LogDocument doc = new LogDocument();
                doc.Back(name);

                this.ViewModel.IsFiled = false;
            }
        }
    }
}
