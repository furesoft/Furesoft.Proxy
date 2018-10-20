using Furesoft.Proxy.Core;
using Furesoft.Proxy.Pages;
using Furesoft.Proxy.UI;
using System;
using System.Collections.ObjectModel;
using System.Windows.Controls;
using System.Windows.Input;

namespace Furesoft.Proxy.ViewModels
{
    public class MainViewModel : BaseViewModel
    {
        public ICommand LogoutCommand { get; set; }
        public PageTransition TransitionContainer { get; set; }
        public ObservableCollection<SearchPopupItem> SearchPopupSource { get; set; } = new ObservableCollection<SearchPopupItem>();

        #region Properties
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

        private bool _searchBarVisible;
        public bool SearchBarVisible
        {
            get { return _searchBarVisible; }
            set {
                _searchBarVisible = value;
                OnPropertyChanged();
            }
        }

        private DialogType _dialogContent;
        public DialogType DialogContent
        {
            get { return _dialogContent; }
            set { _dialogContent = value;OnPropertyChanged(); }
        }

        private bool _dialogOpened;
        public bool DialogOpened
        {
            get { return _dialogOpened; }
            set { _dialogOpened = value; OnPropertyChanged(); }
        }

        #endregion

        public MainViewModel()
        {
            LogoutCommand = new ActionCommand((_) =>
            {
                ServiceLocator.Instance.IsLoggedIn = false;

                TransitionContainer.ShowPage(new LoginPage());
            });
        }
    }
}