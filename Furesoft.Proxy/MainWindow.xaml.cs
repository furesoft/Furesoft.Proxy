using Furesoft.Proxy.Core;
using Furesoft.Proxy.Pages;
using Furesoft.Proxy.ViewModels;
using System.Windows;
using System.Windows.Controls;

namespace Furesoft.Proxy
{
    public partial class MainWindow
    {
        public MainWindow()
        {
            InitializeComponent();
            Loaded += MainWindow_Loaded;
        }

        private void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            DataContext = new MainViewModel() { TransitionContainer = pageContainer };

            var container = ((MainViewModel)DataContext).TransitionContainer;

            ServiceLocator.Instance.PageContainer = container;

            container.ShowPage(new LoginPage());
        }

        private void ListBoxItem_Selected(object sender, RoutedEventArgs e)
        {
            var tb = (((ListBoxItem)sender)).GetChildOfType<TextBlock>();
            var container = ((MainViewModel)DataContext).TransitionContainer;

            MenuToggleButton.IsChecked = false;

            switch (tb.Text)
            {
                case "Filter":
                    container.ShowPage(new FilterPage());
                    break;

                case "Filter Groups":
                    container.ShowPage(new FilterGroupsPage());
                    break;

                case "Redirects":
                    container.ShowPage(new RedirectPage());
                    break;

                case "Settings":
                    container.ShowPage(new SettingsPage());
                    break;

                default:
                    break;
            }
        }

        private void searchTb_TextChanged(object sender, TextChangedEventArgs e)
        {
            var c = ((MainViewModel)DataContext);
            c.SearchChanged = !string.IsNullOrEmpty(searchTb.Text);

            var sites = new[] { "Filter", "Redirects", "Settings" };

            c.SearchPopupSource.Clear();
            CreatePopupItem(c, sites, PopupItemType.Page);
            CreatePopupItem(c, new[] { "Add Filter", "Add Redirect", "Add Filtergroup" }, PopupItemType.Action);
            CreatePopupItem(c, new[] { "Change Password", "Search for Updates" }, PopupItemType.Setting);
        }

        private void CreatePopupItem(MainViewModel model, string[] src, PopupItemType type = PopupItemType.Page)
        {
            foreach (var s in src)
            {
                if (s.ToLower().Contains(searchTb.Text.ToLower()))
                {
                    var it = new SearchPopupItem
                    {
                        PopupType = type,
                        Title = s
                    };

                    model.SearchPopupSource.Add(it);
                }
            }
        }

        private void StackPanel_MouseLeftButtonUp(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            searchTb.Text = "";
        }
    }
}