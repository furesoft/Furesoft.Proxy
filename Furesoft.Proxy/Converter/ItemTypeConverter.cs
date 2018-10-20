using Furesoft.Proxy.Core;
using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;
using System.Windows.Markup;

namespace Furesoft.Proxy.Converter
{
    public class ItemTypeConverter : MarkupExtension, IValueConverter
    {
        private static ItemTypeConverter Instance = new ItemTypeConverter();

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            string enumName = Enum.GetName(typeof(PopupItemType), value);

            if (enumName == parameter.ToString())
            {
                return Visibility.Visible;
            }

            return Visibility.Collapsed;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }

        public override object ProvideValue(IServiceProvider serviceProvider)
        {
            return Instance;
        }
    }
}