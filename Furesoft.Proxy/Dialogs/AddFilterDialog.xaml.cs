using Furesoft.Proxy.ViewModels;
using System.Windows.Controls;

namespace Furesoft.Proxy.Dialogs
{
    public partial class AddFilterDialog : UserControl
    {
        public AddFilterDialog()
        {
            InitializeComponent();

            DataContext = new AddFilterDialogViewModel();
        }
    }
}