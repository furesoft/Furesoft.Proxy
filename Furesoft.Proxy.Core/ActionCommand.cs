using System;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Furesoft.Proxy.Core
{
    public class ActionCommand : ICommand
    {
        public ActionCommand(Action<object> p)
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

    public class AsyncActionCommand : ICommand
    {
        public AsyncActionCommand(Action<object> p)
        {
            Action = p;
        }

        public Action<object> Action { get; set; }

        public event EventHandler CanExecuteChanged;

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public async void Execute(object parameter)
        {
            await Task.Run(()=> Action(parameter));
        }
    }
}