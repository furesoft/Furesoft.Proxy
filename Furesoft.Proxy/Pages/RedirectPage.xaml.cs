using Furesoft.Proxy.UI;
using System.Windows.Controls;

namespace Furesoft.Proxy.Pages
{
    public partial class RedirectPage : UserControl
    {
        public RedirectPage()
        {
            InitializeComponent();

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

        private void filterLb_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}