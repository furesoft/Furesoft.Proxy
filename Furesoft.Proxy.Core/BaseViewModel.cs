using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;

namespace Furesoft.Proxy.Core
{
    public class BaseViewModel : FrameworkElement, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string name = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
    }
}