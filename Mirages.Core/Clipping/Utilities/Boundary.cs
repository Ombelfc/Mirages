using System.Windows;

namespace Mirages.Core.Clipping.Utilities
{
    /// <summary>
    /// Object representing the boundary of a shape in the clipping scene.
    /// </summary>
    public class Boundary
    {
        public double XMin { get; set; } = int.MaxValue;
        public double XMax { get; set; } = int.MinValue;
        public double YMin { get; set; } = int.MaxValue;
        public double YMax { get; set; } = int.MinValue;

        public Boundary() { }

        public Boundary(double x, double y)
        {
            XMin = XMax = x;
            YMin = YMax = y;
        }

        public Boundary(double xMin, double xMax, double yMin, double yMax)
        {
            XMin = xMin;
            XMax = xMax;
            YMin = yMin;
            YMax = yMax;
        }

        public void UpdateBoundary(double x, double y)
        {
            if (x <= XMin) XMin = x;
            if (x >= XMax) XMax = x;
            if (y <= YMin) YMin = y;
            if (y >= YMax) YMax = y;
        }

        public void Reset()
        {
            XMin = int.MaxValue;
            XMax = int.MinValue;
            YMin = int.MaxValue;
            YMax = int.MinValue;
        }

        public bool Contains(Point p)
        {
            if ((p.X > XMax) || (p.X < XMin))
                return false;

            return p.Y <= YMax && p.Y >= YMin;
        }
    }
}
