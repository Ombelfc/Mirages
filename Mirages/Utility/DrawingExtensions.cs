using System;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace Mirages.Utility
{
    public static class DrawingExtensions
    {
        public static void DrawLine(this WriteableBitmap writeableBitmap, Point startPoint, Point endPoint, Color color, int radius)
        {
            int x1 = (int)startPoint.X;
            int y1 = (int)startPoint.Y;
            int x2 = (int)endPoint.X;
            int y2 = (int)endPoint.Y;

            writeableBitmap.DrawLine(x1, y1, x2, y2, color);

            var steep = Math.Abs(y2 - y1) > Math.Abs(x2 - x1);

            if (steep && radius > 0)
            {
                for (var i = -radius; i <= radius; i++)
                {
                    writeableBitmap .DrawLine(x1 + i, y1, x2 + i, y2, color);
                }
            }
            else
            {
                for (var i = -radius; i <= radius; i++)
                {
                    writeableBitmap .DrawLine(x1, y1 + i, x2, y2 + i, color);
                }
            }
        }
    }
}
