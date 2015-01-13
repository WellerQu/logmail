using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace LogMailApp.Command
{
    class CanExecuteChangedEventArgs : EventArgs
    {
        public bool Current { get; set; }
    }

    abstract class CommandBase : ICommand
    {
        public CommandBase()
        {
        }

        public bool LastCanExecute = true;

        #region ICommand 成员

        public virtual bool CanExecute(object parameter)
        {
#if DEBUG_UI
            LastCanExecute = false;
#else
            LastCanExecute = true;
#endif
            return LastCanExecute;
        }

        public event EventHandler CanExecuteChanged;

        public abstract void Execute(object parameter);

        #endregion

        protected void OnCanExecuteChanged()
        {
            EventHandler handler = this.CanExecuteChanged;
            if (handler != null)
                handler(this, new CanExecuteChangedEventArgs() { Current = this.LastCanExecute });
        }

        /// <summary>
        /// 命令优先级, 数值越小优先级越高
        /// </summary>
        public int Order { get; set; }
    }
}
