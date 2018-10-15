using System;
using System.Windows.Input;

namespace Furesoft.Proxy.Core
{
    public class RelayCommand : ICommand
    {
        public RelayCommand(Action<object> p)
        {
            Action = p;
        }

        public Action<object> Action { get; set; }

        public event EventHandler CanExecuteChanged;

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            Action(parameter);
        }
    }
}