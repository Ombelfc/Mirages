using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace Mirages.ConvolutionFilters
{
    public static class EdgeDetection
    {
        private const int PIXEL_SIZE = 4;
        private const double factor = 1.0;
        private const double offset = 127.0;

        public unsafe static BitmapSource EdgeDetectionFilter(this BitmapSource source, double[,] matrix)
        {
            int width = source.PixelWidth;
            int height = source.PixelHeight;
            var bitmap = new WriteableBitmap(source);

            bitmap.Lock();

            var backBuffer = (byte*)bitmap.BackBuffer.ToPointer();

            double red = 0.0;
            double green = 0.0;
            double blue = 0.0;

            int filterOffset = (matrix.GetLength(1) - 1) / 2;
            int calcOffset = 0;
            int byteOffset = 0;

            for (int y = filterOffset; y < height - filterOffset; y++)
            {
                for (int x = filterOffset; x < width - filterOffset; x++)
                {
                    red = 0;
                    green = 0;
                    blue = 0;

                    byteOffset = y * bitmap.BackBufferStride + x * 4;

                    for (int filterY = -filterOffset; filterY <= filterOffset; filterY++)
                    {
                        for (int filterX = -filterOffset; filterX <= filterOffset; filterX++)
                        {
                            calcOffset = byteOffset + (filterX * 4) + (filterY * bitmap.BackBufferStride);

                            red += (double)((backBuffer[calcOffset]) * matrix[filterY + filterOffset, filterX + filterOffset]);
                            green += (double)((backBuffer[calcOffset + 1]) * matrix[filterY + filterOffset, filterX + filterOffset]);
                            blue += (double)((backBuffer[calcOffset + 2]) * matrix[filterY + filterOffset, filterX + filterOffset]);
                        }
                    }

                    red = factor * red + offset;
                    green = factor * green + offset;
                    blue = factor * blue + offset;

                    if (red > 255) red = 255;
                    else if (red < 0) red = 0;

                    if (green > 255) green = 255;
                    else if (green < 0) green = 0;

                    if (blue > 255) blue = 255;
                    else if (blue < 0) blue = 0;

                    backBuffer[byteOffset] = (byte)(red);
                    backBuffer[byteOffset + 1] = (byte)(green);
                    backBuffer[byteOffset + 2] = (byte)(blue);
                    backBuffer[byteOffset + 3] = (byte)(255);
                }
            }

            bitmap.AddDirtyRect(new System.Windows.Int32Rect(0, 0, width, height));
            bitmap.Unlock();

            return bitmap;
        }
    }
}
