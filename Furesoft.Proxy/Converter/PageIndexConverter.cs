using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace Furesoft.Proxy.Converter
{
    public class PageIndexConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if(ServiceLocator.Instance.IsLoggedIn)
            {
                return 1;
            }

            return 0;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}