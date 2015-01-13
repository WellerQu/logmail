using LogMailApp.Storage;

namespace LogMailApp.Command
{
    class SaveLogCommand : EditorPanelCommand
    {
        public SaveLogCommand(UserData userData)
            : base(userData)
        {
        }

        public override void Execute(object parameter)
        {
            if (this.ViewModel != null)
            {
                string name = this.ViewModel.SelectedDate.Value.ToString("yyyy-MM-dd");
                string[] content = this.ViewModel.LogContent.Split('\r', '\n');

                LogDocument doc = new LogDocument();
                doc.Save(name, content);
            }
        }
    }
}
