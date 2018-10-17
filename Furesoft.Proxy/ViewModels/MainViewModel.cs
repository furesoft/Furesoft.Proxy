using Furesoft.Proxy.Core;
using Furesoft.Proxy.Pages;
using Furesoft.Proxy.UI;
using System.Windows.Input;

namespace Furesoft.Proxy.ViewModels
{
    public class MainViewModel : BaseViewModel
    {
        public ICommand LogoutCommand { get; set; }
        public PageTransition TransitionContainer { get; set; }

        public MainViewModel()
        {
            LogoutCommand = new RelayCommand((_) =>
            {
                ServiceLocator.Instance.IsLoggedIn = false;
                TransitionContainer.ShowPage(new LoginPage());
            });
        }
    }
}