using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace Mirages.Utility
{
    public static class WriteableBitmapExtensions
    {
        public static void DrawPoint(this WriteableBitmap wb, Point point, Color color, int radius)
        {
            int pX = (int)point.X;
            int pY = (int)point.Y;

            for (int i = pX - radius; i <= pX + radius; i++)
            {
                for (int j = pY - radius; j <= pY + radius; j++)
                {
                    if ((i - pX) * (i - pX) + (j - pY) * (j - pY) <= radius * radius)
                    {
                        wb.SetPixel(i, j, color);
                    }
                }
            }
        }
    }
}
