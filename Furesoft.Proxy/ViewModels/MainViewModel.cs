using Furesoft.Proxy.Core;
using Furesoft.Proxy.Pages;
using Furesoft.Proxy.UI;
using System.Collections.ObjectModel;
using System.Windows.Controls;
using System.Windows.Input;

namespace Furesoft.Proxy.ViewModels
{
    public class MainViewModel : BaseViewModel
    {
        public ICommand LogoutCommand { get; set; }
        public ICommand SearchCommand { get; set; }
        public PageTransition TransitionContainer { get; set; }
        public ObservableCollection<ListBoxItem> SearchPopupSource { get; set; } = new ObservableCollection<ListBoxItem>();

        private string _searchText;
        public string SearchText
        {
            get { return _searchText; }
            set { _searchText = value;OnPropertyChanged(); }
        }

        private bool _searchChanged;
        public bool SearchChanged
        {
            get { return _searchChanged; }
            set { _searchChanged = value; OnPropertyChanged(); }
        }

        public MainViewModel()
        {
            LogoutCommand = new RelayCommand((_) =>
            {
                ServiceLocator.Instance.IsLoggedIn = false;
                TransitionContainer.ShowPage(new LoginPage());
            });
            SearchCommand = new RelayCommand( _ =>
            {
                
            });
        }
    }
}