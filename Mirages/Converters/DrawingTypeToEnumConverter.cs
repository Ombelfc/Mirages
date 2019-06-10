using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace Mirages.Converters
{
    /// <summary>
    /// Converts the radio button selection to its equivalent enum value and back.
    /// </summary>
    public class DrawingTypeToEnumConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (!(parameter is string parameterAsString))
                return DependencyProperty.UnsetValue;

            if (Enum.IsDefined(value.GetType(), value) == false)
                return DependencyProperty.UnsetValue;

            object parameterValue = Enum.Parse(value.GetType(), parameterAsString);

            return parameterValue.Equals(value);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return !(parameter is string parameterAsString) ? DependencyProperty.UnsetValue : Enum.Parse(targetType, parameterAsString);
        }
    }
}
