using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogMailApp
{
    class Cmd
    {
        public class InnerCommand
        {
            public string CommandName { get; set; }
            public string CommandParams { get; set; }
        }

        public Cmd(string[] args)
        {
            this.ToBeContinued = true;
        }

        public bool ToBeContinued { get; private set; }

        public Cmd Run()
        {
            return this;
        }
    }
}
