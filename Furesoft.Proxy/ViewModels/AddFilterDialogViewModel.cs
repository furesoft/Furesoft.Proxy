using Furesoft.Proxy.Core;
using MaterialDesignThemes.Wpf;
using System.Windows.Input;
using ToastNotifications.Messages;

namespace Furesoft.Proxy.ViewModels
{
    public class AddFilterDialogViewModel : BaseViewModel
    {
        public ICommand OkCommand { get; set; }

        public AddFilterDialogViewModel()
        {
            OkCommand = new ActionCommand(_ =>
            {
                //ToDo: add filter command to database through rpc service
                NotificationManager.notifier.ShowInformation("Add Filter is not implemented");

                DialogHost.CloseDialogCommand.Execute(null, null);
            });
        }
    }
}