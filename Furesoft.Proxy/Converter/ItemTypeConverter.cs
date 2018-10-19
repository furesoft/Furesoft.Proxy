using Furesoft.Proxy.Core;
using MaterialDesignThemes.Wpf;
using System;
using System.Diagnostics;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace Furesoft.Proxy.Converter
{
    public class ItemTypeConverter : IValueConverter
    {
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
    }
}