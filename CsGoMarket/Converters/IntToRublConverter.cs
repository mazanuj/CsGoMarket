using System;
using System.Globalization;
using System.Text.RegularExpressions;
using System.Windows.Data;

namespace CsGoMarket.Converters
{
    [ValueConversion(typeof (int), typeof (string))]
    public class IntToRublConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return (double.Parse(value.ToString())/100).ToString("C", new CultureInfo("Ru-ru"));
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            try
            {
                var res = Regex.Replace(value.ToString(), @"^0|[^\d\.\,]", string.Empty).Replace(".", ",");
                var resO = Math.Round(100*double.Parse(res), 2);
                return (int) resO;
            }
            catch (Exception)
            {
                return 0;
            }
        }
    }
}