using Furesoft.Proxy.Core;
using Furesoft.Proxy.Dialogs;
using Furesoft.Proxy.Models;
using Furesoft.Proxy.Rpc.Interfaces;
using Furesoft.Proxy.ViewModels;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace Furesoft.Proxy.Pages
{
    public partial class FilterPage : UserControl
    {
        public FilterPage()
        {
            InitializeComponent();
        }

        private async void UserControl_Loaded(object sender, System.Windows.RoutedEventArgs e)
        {
            var ops = ServiceLocator.Instance.RpcClient.Bind<IFilterOperations>();

            ServiceLocator.Instance.AllFilter = new FilterCollection(filterLb);
            ServiceLocator.Instance.AllFilter.AddRange(await Task.Run(()=> ops.GetFilters()));
        }

        private void filterLb_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var f = filterLb.SelectedItem;

            if(f is Filter filter)
            {
                CommandContext.AddContext(CommandContextIds.FilterSelected);
                ServiceLocator.Instance.SelectedFilter = filter;
            }
        }
    }
}