using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using LogMailApp.Command;
using LogMailApp.Preference;
using LogMailApp.Properties;

namespace LogMailApp.VM
{
    class VMEditorPanel : ViewModelBase
    {
        public VMEditorPanel()
        {
            this.NewButtonText = Resources.NewButtonText;

            UserData userData = new UserData();
            userData[EditorPanelCommand.VIEWMODEL_KEY] = this;

            this.SaveLogCommand = new SaveLogCommand(userData);
            this.DeleteLogCommand = new DeleteLogCommand(userData);
            this.LoadLogCommand = new LoadLogCommand(userData);
            this.BackFileCommand = new BackFileCommand(userData);

            this.LogDate = DateTime.Now;
            this.LogContent = Resources.WelcomeText;
            this.SelectedDate = this.LogDate;
        }

        private string _LogContent;
        private DateTime? _LogDate;
        private DateTime? _SelectedDate;
        private bool? _IsFiled;

        private CommandBase SaveLogCommand = null;
        private CommandBase DeleteLogCommand = null;
        private CommandBase LoadLogCommand = null;
        private CommandBase BackFileCommand = null;

        public string NewButtonText { get; private set; }

        public DateTime? LogDate
        {
            get { return _LogDate; }
            set
            {
                _LogDate = value;
                this.OnPropertyChanged("LogDate");
            }
        }

        public DateTime? SelectedDate
        {
            get { return _SelectedDate; }
            set
            {
                _SelectedDate = value;
                this.OnPropertyChanged("SelectedDate");
            }
        }

        public string LogContent
        {
            get { return _LogContent; }
            set
            {
                _LogContent = value;
                this.OnPropertyChanged("LogContent");
            }
        }

        public bool? IsFiled
        {
            get { return _IsFiled; }
            set
            {
                _IsFiled = value;
                this.OnPropertyChanged("IsFiled");
            }
        }

        public ICommand Save
        {
            get
            {
                return this.SaveLogCommand;
            }
        }

        public ICommand Delete
        {
            get
            {
                return this.DeleteLogCommand;
            }
        }

        public ICommand Load
        {
            get
            {
                return this.LoadLogCommand;
            }
        }

        public ICommand Back
        {
            get
            {
                return this.BackFileCommand;
            }
        }
    }
}
