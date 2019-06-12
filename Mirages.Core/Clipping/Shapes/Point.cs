using System;
using System.Collections.Generic;
using System.Windows.Media.Imaging;
using Mirages.Core.Clipping.Utilities;
using Mirages.Infrastructure.Components;
using Mirages.Infrastructure.Components.Colors;

namespace Mirages.Core.Clipping.Shapes
{
    /// <summary>
    /// Object representing X, Y coordinates in 2D space.
    /// </summary>
    public class Point : DrawingShape
    {
        #region Fields

        /// <summary>
        /// X coordinate of the point.
        /// </summary>
        public double X { get; private set; }
        /// <summary>
        /// Y coordinate of the point.
        /// </summary>
        public double Y { get; private set; }

        #endregion

        #region Constructors

        /// <summary>
        /// Creates a point at (0, 0).
        /// </summary>
        public Point() : this(0, 0) { }
        /// <summary>
        /// Creates a point with equal X, Y coordinates.
        /// </summary>
        /// <param name="value"></param>
        public Point(double value) : this(value, value) { }
        /// <summary>
        /// Creates a point with the specified coordinates.
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        public Point(double x, double y)
        {
            X = x;
            Y = y;
        }

        #endregion

        #region Public Methods

        public void DrawAndAdd(WriteableBitmap writeableBitmap, Point lastPoint, ByteColor color, int width)
        {
            //Point = lastPoint;
            Color = color;
            LineWidth = width;
            Boundary = new Boundary(lastPoint.X, lastPoint.Y);
            //writeableBitmap.DrawPoint(lastPoint, Color, Width);
        }

        #endregion

        #region Overrides

        public override DrawingShape MoveObject(Vector2 vector)
        {
            throw new NotImplementedException();
        }

        public override DrawingShape Clone()
        {
            throw new NotImplementedException();
        }

        public override void UpdateBoundaries()
        {
            throw new NotImplementedException();
        }

        public override void DrawObject(WriteableBitmap writeableBitmap)
        {
            throw new NotImplementedException();
        }

        public override bool IfPointCloseToBoundary(Point point)
        {
            throw new NotImplementedException();
        }

        public override void EraseObject(List<DrawingShape> list, WriteableBitmap writeableBitmap, DoubleColor color)
        {
            throw new NotImplementedException();
        }

        public override void HighlightObject(bool ifHighlight, WriteableBitmap writeableBitmap, DoubleColor color)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
