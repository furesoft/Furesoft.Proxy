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

            tageditor.TokenMatcher = text =>
            {
                if (text.EndsWith(" "))
                {
                    // Remove the ' '
                    return text.Substring(0, text.Length - 1).Trim().ToUpper();
                }

                return null;
            };
        }

        
    }
}