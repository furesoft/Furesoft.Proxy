using Furesoft.Proxy.Commands;
using Furesoft.Proxy.Models;
using Furesoft.Proxy.ViewModels;
using System.Windows.Controls;
using System.Windows.Input;

namespace Furesoft.Proxy.Dialogs
{
    public partial class ChangeFilterDialog : UserControl
    {
        public ChangeFilterDialog()
        {
            InitializeComponent();

            DataContext = new ChangeFilterDialogViewModel();
        }
    }
}