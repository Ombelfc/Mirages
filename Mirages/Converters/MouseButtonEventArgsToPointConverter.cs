using GalaSoft.MvvmLight.Command;
using System.Windows;
using System.Windows.Input;

namespace Mirages.Converters
{
    public class MouseButtonEventArgsToPointConverter : IEventArgsConverter
    {
        public object Convert(object value, object parameter)
        {
            var args = value as MouseButtonEventArgs;
            var element = parameter as FrameworkElement;

            return args?.GetPosition(element);
        }
    }
}
