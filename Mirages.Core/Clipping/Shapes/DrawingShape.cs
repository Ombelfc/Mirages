using Mirages.Core.Clipping.Utilities;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Media.Imaging;
using Color = System.Windows.Media.Color;

namespace Mirages.Core.Clipping.Shapes
{
    public abstract class DrawingShape
    {
        public int Width { get; set; } = 1;
        public System.Windows.Media.Color Color { get; set; }
        public Boundary Boundary;

        public DrawingShape()
        {
            Boundary = new Boundary();
        }

        public double XCenter => Boundary.XMin + (Boundary.XMax - Boundary.XMin) / 2;
        public double YCenter => Boundary.YMin + (Boundary.YMax - Boundary.YMin) / 2;

        public abstract DrawingShape MoveObject(Vector2 vector);
        public abstract DrawingShape Clone();

        public abstract void UpdateBoundaries();
        public abstract void DrawObject(WriteableBitmap writeableBitmap);
        public abstract void EraseObject(List<DrawingShape> list, WriteableBitmap writeableBitmap, Color color);
        public abstract void HighlightObject(bool ifHighlight, WriteableBitmap writeableBitmap, Color color);
        public abstract bool IfPointCloseToBoundary(Point point);
    }
}
