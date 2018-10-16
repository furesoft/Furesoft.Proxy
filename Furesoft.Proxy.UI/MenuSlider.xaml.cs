using System.Windows;
using System.Windows.Markup;
using System.Windows.Media;

namespace Furesoft.Proxy.UI
{
    [ContentProperty("InnerContent")]
    public partial class MenuSlider
    {
        public object InnerContent
        {
            get { return (object)GetValue(InnerContentProperty); }
            set { SetValue(InnerContentProperty, value); }
        }

        // Using a DependencyProperty as the backing store for InnerContent.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty InnerContentProperty =
            DependencyProperty.Register("InnerContent", typeof(object), typeof(MenuSlider), new PropertyMetadata(0));

        public Color ButtonBackground
        {
            get { return (Color)GetValue(ButtonBackgroundProperty); }
            set { SetValue(ButtonBackgroundProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ButtonBackgroundProperty =
            DependencyProperty.Register("ButtonBackground", typeof(int), typeof(MenuSlider), new PropertyMetadata(0));



        public MenuSlider()
        {
            InitializeComponent();
        }

        private void ButtonOpenMenu_Click(object sender, RoutedEventArgs args)
        {
            ButtonCloseMenu.Visibility = Visibility.Visible;
            ButtonOpenMenu.Visibility = Visibility.Hidden;
        }

        private void ButtonCloseMenu_Click(object sender, RoutedEventArgs args)
        {
            ButtonCloseMenu.Visibility = Visibility.Hidden;
            ButtonOpenMenu.Visibility = Visibility.Visible;

        }
    }
}