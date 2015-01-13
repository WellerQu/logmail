using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LogMailApp.VM;

namespace LogMailApp.Command
{
    abstract class EditorPanelCommand : CommandBase
    {
        public EditorPanelCommand(UserData userData)
        {
            this.ViewModel = userData[VIEWMODEL_KEY] as VMEditorPanel;
        }

        public const string VIEWMODEL_KEY = "ViewModel.EditorData";

        protected VMEditorPanel ViewModel { get; private set; }
    }
}
