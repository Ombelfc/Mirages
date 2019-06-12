using Mirages.Core.Clipping.Shapes;
using System.Windows.Input;

namespace Mirages.Model.Clipping
{
    /// <summary>
    /// Object representing arguments of the mouse and the click point.
    /// </summary>
    public class MouseArgsAndPoint
    {
        #region Properties

        /// <summary>
        /// Mouse arguments.
        /// </summary>
        public MouseEventArgs Args { get; set; }
        /// <summary>
        /// Point clicked.
        /// </summary>
        public Point Point { get; set; }

        #endregion
    }
}
