using LogMailApp.Properties;
using LogMailApp.Storage;

namespace LogMailApp.Command
{
    class DeleteLogCommand : EditorPanelCommand
    {
        public DeleteLogCommand(UserData userData)
            : base(userData)
        {
        }

        public override void Execute(object parameter)
        {
            LogDocument doc = new LogDocument();
            doc.Delete(this.ViewModel.SelectedDate.Value.ToString("yyyy-MM-dd"));

            this.ViewModel.LogContent = Resources.WelcomeText;
        }
    }
}
