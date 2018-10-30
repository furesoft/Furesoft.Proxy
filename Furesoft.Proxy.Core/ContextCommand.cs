using System;
using System.Windows.Input;

namespace Furesoft.Proxy.Core
{
    public abstract class ContextCommand : ICommand
    {
        public CommandContextIds[] Ids { get; set; }
        public event EventHandler CanExecuteChanged;

        public bool CanExecute(object parameter)
        {
            CommandContext.ContextChanged += () =>
            {
                CanExecuteChanged(this, EventArgs.Empty);
            };

            if (Ids is null)
                return true;

            return CommandContext.IsInContext(Ids);
        }

        public abstract void Execute(object parameter);
    }
}