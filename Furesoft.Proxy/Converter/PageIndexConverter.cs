﻿using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Markup;

namespace Furesoft.Proxy.Converter
{
    public class PageIndexConverter : MarkupExtension, IValueConverter
    {
        private static PageIndexConverter Instance = new PageIndexConverter();

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (ServiceLocator.Instance.IsLoggedIn)
            {
                return 1;
            }

            return 0;
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