using Furesoft.Proxy.Core;
using Furesoft.Proxy.Models;
using Furesoft.Proxy.Rpc.Interfaces;
using MaterialDesignThemes.Wpf;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Windows;
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
                //ToDo: add filter command to database through rpc service
                NotificationManager.notifier.ShowSuccess("Filter added successfully");

                var result = await ServiceLocator.Instance.RpcClient.CallMethodAsync<IFilterOperations>("Add", Filter);
                Debug.WriteLine(result);

                Application.Current.Dispatcher.Invoke(() => {
                    Filter = new Filter();
                    DialogHost.CloseDialogCommand.Execute(null, null);
                });
            });
        }
    }
}