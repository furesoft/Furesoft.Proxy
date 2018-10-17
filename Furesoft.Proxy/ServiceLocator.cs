using Furesoft.Proxy.Core;
using Furesoft.Proxy.UI;
using System.ComponentModel;

namespace Furesoft.Proxy
{
    public class ServiceLocator : INotifyPropertyChanged
    {
        public static ServiceLocator Instance = new ServiceLocator();

        public ServiceProvider Provider = new ServiceProvider();

        private bool _loggedIn;
        public bool IsLoggedIn
        {
            get
            {
                return _loggedIn;
            }
            set
            {
                _loggedIn = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(IsLoggedIn)));
            }
        }

        public PageTransition PageContainer = new PageTransition();

        public event PropertyChangedEventHandler PropertyChanged;
    }
}