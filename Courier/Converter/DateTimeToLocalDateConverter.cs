using System;
using System.Globalization;
using Xamarin.Forms;

namespace Courier.Converter
{
    public class DateTimeToLocalDateConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
	        return value is DateTimeOffset offset ? offset.ToString("dd.MM.yyyy") : ((DateTime)value).ToString("dd.MM.yyyy");
        }
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return DateTime.Now.ToString("dd.MM.yyyy");
        }
    }
}