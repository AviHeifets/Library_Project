using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace Gui.Helpers
{
    internal class StringToVisibilityConvertercs : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null || parameter == null)
                return Visibility.Visible;

            string valueString = value.ToString()!;
            string parameterString = parameter.ToString()!;

            if (valueString == parameterString)
            {
                return Visibility.Collapsed;
            }

            return Visibility.Visible;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
