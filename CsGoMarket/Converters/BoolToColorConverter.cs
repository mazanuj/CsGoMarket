using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Media;

namespace CsGoMarket.Converters
{
    [ValueConversion(typeof (bool), typeof (Colors))]
    internal class BoolToColorConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return (bool) value ? Colors.Yellow : Colors.Red;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return false;
        }
    }
}