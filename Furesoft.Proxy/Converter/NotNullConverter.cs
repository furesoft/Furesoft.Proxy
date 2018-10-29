using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Markup;

namespace Furesoft.Proxy.Converter
{
    public class NotNullConverter : MarkupExtension, IValueConverter
    {
        private static NotNullConverter Instance = new NotNullConverter();

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return !(value is null);
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