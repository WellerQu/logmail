using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LogMailApp.Properties;

namespace LogMailApp.VM
{
    class VMAboutPanel : ViewModel
    {
        public VMAboutPanel()
        {
            this.AboutButtonText = Resources.AboutButtonText;
        }

        public string AboutButtonText { get; private set; }
    }
}
