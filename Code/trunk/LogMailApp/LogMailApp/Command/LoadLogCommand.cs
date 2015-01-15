using System.Text;
using LogMailApp.Properties;
using LogMailApp.Storage;

namespace LogMailApp.Command
{
    class LoadLogCommand : EditorPanelCommand
    {
        public LoadLogCommand(UserData userData)
            : base(userData)
        { }

        public override void Execute(object parameter)
        {
            LogDocument doc = new LogDocument();
            string content = doc.Load(this.ViewModel.SelectedDate.Value.ToString("yyyy-MM-dd"));

            if (content != null)
            {
                this.ViewModel.LogContent = content;
            }
            else
            {
                this.ViewModel.LogContent = Resources.WelcomeText;
            }
        }
    }
}
