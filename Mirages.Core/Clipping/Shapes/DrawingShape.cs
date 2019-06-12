using Mirages.Core.Clipping.Utilities;
using Mirages.Infrastructure.Components;
using Mirages.Infrastructure.Components.Colors;
using System.Collections.Generic;
using System.Windows.Media.Imaging;

namespace Mirages.Core.Clipping.Shapes
{
    /// <summary>
    /// Base class for shapes drawn on the image.
    /// </summary>
    public abstract class DrawingShape
    {
        #region Properties

        /// <summary>
        /// Line-width of the shape.
        /// </summary>
        public int LineWidth { get; set; } = 1;
        /// <summary>
        /// Color of the shape.
        /// </summary>
        public ByteColor Color { get; set; }
        /// <summary>
        /// Boundary of the shape.
        /// </summary>
        public Boundary Boundary;

        #endregion

        #region Constructors

        /// <summary>
        /// Creates a new drawing-shape.
        /// </summary>
        public DrawingShape()
        {
            Boundary = new Boundary();
        }

        #endregion

        // TODO: add documentation once you figure out their use
        public double XCenter => Boundary.XMin + (Boundary.XMax - Boundary.XMin) / 2;
        public double YCenter => Boundary.YMin + (Boundary.YMax - Boundary.YMin) / 2;

        public abstract DrawingShape MoveObject(Vector2 vector);
        public abstract DrawingShape Clone();

        public abstract void UpdateBoundaries();
        public abstract void DrawObject(WriteableBitmap writeableBitmap);
        public abstract void EraseObject(List<DrawingShape> list, WriteableBitmap writeableBitmap, DoubleColor color);
        public abstract void HighlightObject(bool ifHighlight, WriteableBitmap writeableBitmap, DoubleColor color);
        public abstract bool IfPointCloseToBoundary(Point point);
    }
}
