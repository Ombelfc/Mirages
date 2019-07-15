using Mirages.Core.Clipping.Shapes;
using Mirages.Infrastructure.Components.Colors;
using System;
using System.Windows.Media.Imaging;

namespace Mirages.Utility.Extensions
{
    /// <summary>
    /// Static class containing methods used for drawing.
    /// </summary>
    public static class WriteableBitmapExtensions
    {
        /// <summary>
        /// Draws a line between 2 points relaying on a slope determination.
        /// </summary>
        /// <param name="writeableBitmap"></param>
        /// <param name="startPoint"></param>
        /// <param name="endPoint"></param>
        /// <param name="color"></param>
        /// <param name="radius"></param>
        public static void DrawGridLine(this WriteableBitmap writeableBitmap, Point startPoint, Point endPoint, ByteColor color, int radius)
        {
            int x1 = (int) startPoint.X;
            int y1 = (int) startPoint.Y;
            int x2 = (int) endPoint.X;
            int y2 = (int) endPoint.Y;

            // Draw 'core' line
            writeableBitmap.DrawLine(x1, y1, x2, y2, ColorExtensions.FromByteColor(color));
            // Delta(y) > Delta(x)
            var slope = Math.Abs(y2 - y1) > Math.Abs(x2 - x1);

            if (slope && radius > 0)
            {
                for (int i = -radius; i <= radius; i++)
                    writeableBitmap.DrawLine(x1 + i, y1, x2 + i, y2, ColorExtensions.FromByteColor(color));
            }
            else
            {
                for (int i = -radius; i <= radius; i++)
                    writeableBitmap.DrawLine(x1, y1 + i, x2, y2 + i, ColorExtensions.FromByteColor(color));
            }
        }

        public static void DrawPoint(this WriteableBitmap writeableBitmap, Point point, ByteColor color, int radius)
        {
            int x = (int) point.X;
            int y = (int) point.Y;

            for (int i = x - radius; i <= x + radius; i++)
                for (int j = y - radius; j <= y + radius; j++)
                    if ((i - x) * (i - x) + (j - y) * (j - y) <= radius * radius)
                        writeableBitmap.SetPixel(i, j, ColorExtensions.FromByteColor(color));
        }
    }
}
