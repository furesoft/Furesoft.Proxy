using Furesoft.Proxy.Models;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Markup;

namespace Furesoft.Proxy.Converter
{
    public class ComboBoxToEnumConverter : MarkupExtension, IValueConverter
    {
        private static ComboBoxToEnumConverter Instance = new ComboBoxToEnumConverter();

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var v = (FilterType)value;

            switch (v)
            {
                case FilterType.Regex:
                    return 3;
                case FilterType.Contains:
                    return 2;
                case FilterType.Starts:
                    return 0;
                case FilterType.Ends:
                    return 1;
                default:
                    break;
            }

            return 0;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var v = (int)value;

            return (FilterType)v;
        }

        public override object ProvideValue(IServiceProvider serviceProvider)
        {
            return Instance;
        }
    }
}