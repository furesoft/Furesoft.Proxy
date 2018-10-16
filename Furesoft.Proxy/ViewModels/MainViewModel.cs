using Furesoft.Proxy.Core;
using System.Windows.Input;

namespace Furesoft.Proxy.ViewModels
{
    public class MainViewModel : BaseViewModel
    {
        public ICommand LogoutCommand { get; set; }
        private int _pageIndex;

        public int PageIndex
        {
            get { return _pageIndex; }
            set { _pageIndex = value;OnPropertyChanged(); }
        }


        public MainViewModel()
        {
            LogoutCommand = new RelayCommand((_) => {
                ServiceLocator.Instance.IsLoggedIn = false;
                PageIndex = 0;
                });
        }
    }
}