﻿using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Media;

namespace CsGoMarket.Converters
{
    [ValueConversion(typeof (bool), typeof (Brush))]
    internal class BoolToColorConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return (bool) value ? new SolidColorBrush(Colors.Yellow) : new SolidColorBrush(Colors.Red);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            try
            {
                return (Color) value == Colors.Yellow;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}