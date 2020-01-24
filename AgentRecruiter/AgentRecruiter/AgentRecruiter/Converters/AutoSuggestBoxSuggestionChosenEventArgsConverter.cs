using dotMorten.Xamarin.Forms;

using System;
using System.Globalization;

using Xamarin.Forms;

namespace AgentRecruiter.Converters
{
    public class AutoSuggestBoxSuggestionChosenEventArgsConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var eventArgs = value as AutoSuggestBoxSuggestionChosenEventArgs;
            return eventArgs?.SelectedItem;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
