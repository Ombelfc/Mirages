using Mirages.Infrastructure.Components.Colors;
using Mirages.Utility.Extensions;
using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;
using System.Windows.Media;

namespace Mirages.Converters
{
    /// <summary>
    /// Converts the byte-color to System.Windows.Media.Color and back.
    /// </summary>
    public class ColorToByteColorConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (!(value is ByteColor color))
                return DependencyProperty.UnsetValue;

            return ColorExtensions.FromByteColor(color);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (!(value is Color color))
                return DependencyProperty.UnsetValue;

            return new ByteColor(color.R, color.G, color.B, color.A);
        }
    }
}
