using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace Mirages.Core.Clipping.Shapes
{
    public class CustomPoint : DrawingShape
    {
        public Point Point { get; set; }

        public CustomPoint() : this(0, 0) { }

        public CustomPoint(double x, double y)
        {
            Point = new Point(x, y);
        }

        public CustomPoint(Point point)
        {
            Point = point;
        }

        public override DrawingShape Clone()
        {
            throw new NotImplementedException();
        }

        public override void DrawObject(WriteableBitmap writeableBitmap)
        {
            throw new NotImplementedException();
        }

        public override void EraseObject(List<DrawingShape> list, WriteableBitmap writeableBitmap, System.Windows.Media.Color color)
        {
            throw new NotImplementedException();
        }

        public override void HighlightObject(bool ifHighlight, WriteableBitmap writeableBitmap, System.Windows.Media.Color color)
        {
            throw new NotImplementedException();
        }

        public override bool IfPointCloseToBoundary(Point point)
        {
            throw new NotImplementedException();
        }

        public override DrawingShape MoveObject(Vector2 vector)
        {
            throw new NotImplementedException();
        }

        public override void UpdateBoundaries()
        {
            throw new NotImplementedException();
        }

        public void DrawAndAdd(WriteableBitmap writeableBitmap, object wb, Point point1, Point point2, System.Windows.Media.Color color1, object color2, int v, object width)
        {
            throw new NotImplementedException();
        }

        #region Public Methods

        public void DrawAndAdd(WriteableBitmap writeableBitmap, Point lastPoint, System.Windows.Media.Color color, int width)
        {
            Point = lastPoint;
            Color = color;
            Width = width;
            Boundary = new Boundary(Point.X, Point.Y);
            writeableBitmap.DrawPoint(Point, Color, Width);
        }

        #endregion
    }
}
