using Furesoft.Proxy.Core;
using Furesoft.Proxy.Models;
using Furesoft.Proxy.Rpc.Interfaces;
using MaterialDesignThemes.Wpf;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using ToastNotifications.Messages;

namespace Furesoft.Proxy.ViewModels
{
    public class AddFilterDialogViewModel : BaseViewModel
    {
        public ICommand OkCommand { get; set; }

        private Filter filter;
        public Filter Filter
        {
            get { return filter; }
            set { filter = value;OnPropertyChanged(); }
        }

        public AddFilterDialogViewModel()
        {
            Filter = new Filter();

            OkCommand = new AsyncActionCommand(async _ =>
            {
                NotificationManager.notifier.ShowSuccess("Filter added successfully");

                var result = (bool)await ServiceLocator.Instance.RpcClient.CallMethodAsync<IFilterOperations>("Add", Filter);
                
                if(result)
                {
                    ServiceLocator.Instance.AllFilter.Add(Filter);
                    
                    //ToDo: add reloading listbox
                }

                Application.Current.Dispatcher.Invoke(() => {
                    Filter = new Filter();
                    DialogHost.CloseDialogCommand.Execute(null, null);
                });
            });
        }
    }
}