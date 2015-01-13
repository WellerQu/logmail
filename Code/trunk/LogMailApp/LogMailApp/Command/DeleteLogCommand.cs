using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LogMailApp.Storage;
using LogMailApp.VM;

namespace LogMailApp.Command
{
    class DeleteLogCommand : EditorPanelCommand
    {
        public DeleteLogCommand(UserData userData)
            : base(userData)
        { }

        public override void Execute(object parameter)
        {
            LogDocument doc = new LogDocument();
            doc.Delete(this.ViewModel.SelectedDate.Value.ToString("yyyy-MM-dd"));
        }
    }
}
