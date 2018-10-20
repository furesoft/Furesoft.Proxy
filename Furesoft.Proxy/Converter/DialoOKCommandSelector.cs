using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Markup;

namespace Furesoft.Proxy.Converter
{
    public class DialoOKCommandSelector : MarkupExtension, IValueConverter
    {
        private static DialoOKCommandSelector Instance = new DialoOKCommandSelector();

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            //ToDo: implement command selector
            return null;
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