﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LogMailApp.VM;

namespace LogMailApp.Command
{
    class DeleteLogCommand : CommandBase
    {
        public DeleteLogCommand(VMNewPanel ViewModel)
        { }

        public override void Execute(object parameter)
        {
            throw new NotImplementedException();
        }
    }
}
