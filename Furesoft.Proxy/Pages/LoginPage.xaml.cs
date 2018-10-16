using Furesoft.Proxy.Utils;
using Furesoft.Proxy.ViewModels;
using System.Windows;
using System.Windows.Controls;

namespace Furesoft.Proxy.Pages
{
    public partial class LoginPage
    {
        public LoginPage()
        {
            InitializeComponent();
        }

        private void passwordBox_PasswordChanged(object sender, RoutedEventArgs e)
        {
            ((LoginViewModel)DataContext).PasswordHash = MD5.ToHash(passwordBox.Password);
        }
    }
}