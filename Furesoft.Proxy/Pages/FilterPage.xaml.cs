using Furesoft.Proxy.ViewModels;
using System.Windows.Controls;

namespace Furesoft.Proxy.Pages
{
    public partial class FilterPage : UserControl
    {
        public FilterPage()
        {
            InitializeComponent();
        }

        private void UserControl_Loaded(object sender, System.Windows.RoutedEventArgs e)
        {
            foreach (var item in ServiceLocator.Instance.AllFilter)
            {
                var ctx = (MainViewModel)DataContext;

                ctx.Filters.Add(item);
            }
        }
    }
}