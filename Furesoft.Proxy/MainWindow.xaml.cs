using Furesoft.Proxy.Core;
using Furesoft.Proxy.Models;
using Furesoft.Proxy.Pages;
using Furesoft.Proxy.Rpc.Interfaces;
using Furesoft.Proxy.UI;
using Furesoft.Proxy.ViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
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

        private async void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            DataContext = new MainViewModel() { TransitionContainer = pageContainer };

            var context = (MainViewModel)DataContext;
            var container = context.TransitionContainer;

            ServiceLocator.Instance.RpcClient.Start();

            ServiceLocator.Instance.PageContainer = container;

            var ops = ServiceLocator.Instance.RpcClient.BindAsync<IFilterOperations>();

            await (Task)ops.Add(new Filter() {
                 Type = FilterType.Contains,
                 Pattern = "hello"
            });

            //Collect searchable commands
            CommandCollector.Collect(typeof(MainWindow).Assembly);

            container.ShowPage(new LoginPage());
        }

        private void ListBoxItem_Selected(object sender, RoutedEventArgs e)
        {
            var tb = (((ListBoxItem)sender)).GetChildOfType<TextBlock>();
            var container = ((MainViewModel)DataContext).TransitionContainer;

            MenuToggleButton.IsChecked = false;

            ShowPage(tb.Text, container);
        }

        private static void ShowPage(string tb, PageTransition container)
        {
            switch (tb)
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

                case "Templates":
                    container.ShowPage(new TemplatesPage());
                    break;

                default:
                    break;
            }
        }

        private void searchTb_TextChanged(object sender, TextChangedEventArgs e)
        {
            //PopupItems.Clear();

            var c = ((MainViewModel)DataContext);
            c.SearchChanged = !string.IsNullOrEmpty(searchTb.Text);

            var sites = new[] { "Filter", "Redirects", "Settings" };

            c.SearchPopupSource.Clear();
            CreatePopupItem(c, sites, PopupItemType.Page);

            SearchableCommandRepository.Instance.OpenDialog = new Action<string>((_) =>
            {
                c.DialogContent = GetDialogItem(_);
                c.DialogOpened = true;
            });

            CreatePopupItem(c, SearchableCommandRepository.Instance.CommandNames, PopupItemType.Action);
            CreatePopupItem(c, new[] { "Change Password" }, PopupItemType.Setting);

            AddPopupItems(c);
        }

        private List<SearchPopupItem> PopupItems = new List<SearchPopupItem>();

        private void AddPopupItems(MainViewModel model)
        {
            foreach (var s in CommandUsageProvider.Instance.GetSortedCommandNames())
            {
                if (s.ToLower().Contains(searchTb.Text.ToLower()))
                {
                    model.SearchPopupSource.Add(PopupItems.Find(_ => _.Title == s));
                }
            }
        }

        private void CreatePopupItem(MainViewModel model, string[] src, PopupItemType type = PopupItemType.Page)
        {
            foreach (var s in src)
            {
                var it = new SearchPopupItem
                {
                    PopupType = type,
                    Title = s,
                    IsFavourite = CommandUsageProvider.Instance.IsFavorite(s)
                };

                CommandUsageProvider.Instance.Add(s);

                if (!PopupItems.Contains(it))
                {
                    PopupItems.Add(it);
                }
            }
        }

        private void StackPanel_MouseLeftButtonUp(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            var item = ListboxPopup.SelectedItem as SearchPopupItem;
            var container = ((MainViewModel)DataContext).TransitionContainer;

            switch (item.PopupType)
            {
                case PopupItemType.Page:
                    ShowPage(item.Title, container);
                    break;

                case PopupItemType.Setting:
                    break;

                case PopupItemType.Action:
                    var c = ((MainViewModel)DataContext);

                    SearchableCommandRepository.Instance.ExecuteCommand(item.Title, c);

                    break;

                default:
                    break;
            }

            searchTb.Text = "";
        }

        private DialogType GetDialogItem(string title)
        {
            switch (title)
            {
                case "Add Filter":
                    return DialogType.AddFilter;
                case "Add Filter Group":
                    return DialogType.AddFilterGroup;
                case "Add Redirect":
                    return DialogType.AddRedirect;

                default:
                    break;
            }

            return DialogType.Empty;
        }

        protected override void OnClosing(CancelEventArgs e)
        {
            CommandUsageProvider.Instance.Save();
        }
    }
}