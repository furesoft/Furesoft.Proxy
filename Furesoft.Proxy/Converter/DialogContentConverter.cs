using Furesoft.Proxy.Dialogs;
using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Markup;

namespace Furesoft.Proxy.Converter
{
    public class DialogContentConverter : MarkupExtension, IValueConverter
    {
        private static DialogContentConverter Instance = new DialogContentConverter();

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if(value is DialogType dt)
            {
                //ToDo: add dialogs
                switch (dt)
                {
                    case DialogType.AddFilter:
                        return new AddFilterDialog();
                    case DialogType.AddFilterGroup:
                        break;
                    case DialogType.AddRedirect:
                        break;
                    default:
                        break;
                }
            }

            return new AddFilterDialog();
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