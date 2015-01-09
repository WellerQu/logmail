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
    class VMNewPanel : ViewModel
    {
        public VMNewPanel()
        {
            this.SaveLogCommand = new SaveLogCommand(this);
            this.DeleteLogCommand = new DeleteLogCommand(this);
            this.LoadLogCommand = new LoadLogCommand(this);

            this.LogDate = DateTime.Now;
            this.LogContent = Resources.Welcome;
        }

        private string _LogContent;
        private DateTime? _LogDate;
        private DateTime? _SelectedDate;
        private CommandBase SaveLogCommand = null;
        private CommandBase DeleteLogCommand = null;
        private CommandBase LoadLogCommand = null;

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
            set { _SelectedDate = value; }
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
    }
}
