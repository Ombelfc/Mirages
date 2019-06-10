using GalaSoft.MvvmLight.Command;
using Mirages.Model.Clipping;
using System.Windows;
using System.Windows.Input;

namespace Mirages.Converters
{
    public class MouseButtonEventArgsAndPointToObjectConverter : IEventArgsConverter
    {
        public object Convert(object value, object parameter)
        {
            var args = value as MouseEventArgs;
            var element = parameter as FrameworkElement;

            return new MouseArgsAndPoint { Args = args, Point = args?.GetPosition(element) };
        }
    }
}
