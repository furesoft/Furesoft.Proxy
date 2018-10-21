using Furesoft.Proxy.Core;
using System;
using System.Windows.Input;

namespace Furesoft.Proxy.Commands
{
    [SearchableCommand("Search for Updates")]
    public class SearchUpdatesCommand : ICommand
    {
        public event EventHandler CanExecuteChanged;

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            SearchableCommandRepository.Instance.OpenDialog("Search Updates");
        }
    }
}