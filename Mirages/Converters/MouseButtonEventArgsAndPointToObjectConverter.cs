using GalaSoft.MvvmLight.Command;
using Mirages.Model.Clipping;
using System.Windows;
using System.Windows.Input;

namespace Mirages.Converters
{
    /// <summary>
    /// Converts the mouse click point to an object passed to the commands.
    /// </summary>
    public class MouseButtonEventArgsAndPointToObjectConverter : IEventArgsConverter
    {
        /// <summary>
        /// Converts the mouse click point to a MouseArgsAndPoint object passed to the commands.
        /// </summary>
        /// <param name="value"></param>
        /// <param name="parameter"></param>
        /// <returns></returns>
        public object Convert(object value, object parameter)
        {
            if (!(value is MouseButtonEventArgs args) || !(parameter is FrameworkElement element))
                return null;

            var point = args?.GetPosition(element);

            return new MouseArgsAndPoint { Args = args, Point = new Core.Clipping.Shapes.Point(point.Value.X, point.Value.Y) };
        }
    }
}
