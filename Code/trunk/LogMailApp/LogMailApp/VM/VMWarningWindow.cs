using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogMailApp.VM
{
    class VMWarningWindow : ViewModel
    {
        private string _Text;
        public string Text
        {
            get { return _Text; }
            set
            {
                _Text = value;
                this.OnPropertyChanged("Text");
            }
        }
    }
}
