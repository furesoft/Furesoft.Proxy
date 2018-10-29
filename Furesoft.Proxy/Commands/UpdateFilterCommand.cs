using Furesoft.Proxy.Core;
using System;
using System.Windows.Input;
using System.Windows.Markup;

namespace Furesoft.Proxy.Commands
{
    public class UpdateFilterCommand : MarkupExtension, ICommand
    {
        public static UpdateFilterCommand Instance = new UpdateFilterCommand();

        public event EventHandler CanExecuteChanged;

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            SearchableCommandRepository.Instance.OpenDialog("Change Filter");
        }

        public override object ProvideValue(IServiceProvider serviceProvider)
        {
            return Instance;
        }
    }
}