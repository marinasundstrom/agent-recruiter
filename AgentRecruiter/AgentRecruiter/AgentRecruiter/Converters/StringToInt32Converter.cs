using System;
using System.Globalization;

using Xamarin.Forms;

namespace AgentRecruiter.Converters
{
    class StringToInt32Converter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value.ToString();
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (int.TryParse((string)value, out var result))
            {
                return result;
            }

            return 1;
        }
    }
}
