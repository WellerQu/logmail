using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LogMailApp.Properties;

namespace LogMailApp.VM
{
    class VMSettingPanel : ViewModel
    {
        public VMSettingPanel()
        {
            this.SettingButtonText = Resources.SettingButtonText;
        }

        public string SettingButtonText { get; set; }
    }
}
