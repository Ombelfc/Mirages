using GalaSoft.MvvmLight.Command;
using System.Windows;
using System.Windows.Input;

namespace Mirages.Converters
{
    public class MouseButtonEventArgsToPointConverter : IEventArgsConverter
    {
        /// <summary>
        /// Converts the mouse click point to a point passed to the commands.
        /// </summary>
        /// <param name="value"></param>
        /// <param name="parameter"></param>
        /// <returns></returns>
        public object Convert(object value, object parameter)
        {
            if (!(value is MouseButtonEventArgs args) || !(parameter is FrameworkElement element))
                return null;

            var point = args?.GetPosition(element);

            return new Core.Clipping.Shapes.Point(point.Value.X, point.Value.Y);
        }
    }
}
