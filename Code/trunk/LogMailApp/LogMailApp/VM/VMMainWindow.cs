using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LogMailApp.Properties;

namespace LogMailApp.VM
{
    class VMMainWindow : ViewModel
    {
        public VMMainWindow()
        {
            this.NewPanelDataContext = new VMEditorPanel();
            this.SettingPanelDataContext = new VMSettingPanel();
            this.AboutPanelDataContext = new VMAboutPanel();
        }

        public VMEditorPanel NewPanelDataContext { get; set; }
        public VMSettingPanel SettingPanelDataContext { get; set; }
        public VMAboutPanel AboutPanelDataContext { get; set; }
    }
}
