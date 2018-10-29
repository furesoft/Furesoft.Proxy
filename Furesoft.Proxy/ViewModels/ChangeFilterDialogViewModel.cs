using Furesoft.Proxy.Core;
using Furesoft.Proxy.Models;
using Furesoft.Proxy.Rpc.Interfaces;
using MaterialDesignThemes.Wpf;
using System.Windows;
using System.Windows.Input;
using ToastNotifications.Messages;

namespace Furesoft.Proxy.ViewModels
{
    public class ChangeFilterDialogViewModel : BaseViewModel
    {
        public ICommand OkCommand { get; set; }
        public Filter Filter => ServiceLocator.Instance.SelectedFilter;

        public ChangeFilterDialogViewModel()
        {
            OkCommand = new AsyncActionCommand(async _ =>
            {
                var filter = ServiceLocator.Instance.SelectedFilter;
                
                NotificationManager.notifier.ShowSuccess("Filter changed successfully");

                var result = (bool)await ServiceLocator.Instance.RpcClient.CallMethodAsync<IFilterOperations>("Update", filter);

                if (result)
                {
                    ServiceLocator.Instance.AllFilter.Replace(filter);

                    //ToDo: add reloading listbox
                }

                Application.Current.Dispatcher.Invoke(() => {
                    DialogHost.CloseDialogCommand.Execute(null, null);
                });
            });
        }
    }
}