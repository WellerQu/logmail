using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LogMailApp.Preference;
using LogMailApp.Properties;

namespace LogMailApp.VM
{
    class VMAboutPanel : ViewModelBase
    {
        public VMAboutPanel()
        {
            this.AboutButtonText = Resources.AboutButtonText;
            this.AboutContentText = UserDefault.Instance.Readme;
        }

        public string AboutButtonText { get; private set; }
        public string AboutContentText { get; set; }
    }
}
