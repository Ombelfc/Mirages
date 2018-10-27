using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace Mirages.ConvolutionFilters
{
    public static class Gaussian
    {
        private const int PIXEL_SIZE = 4;
        private const double factor = 1.0 / 80.0;
        private const double offset = 0.0;

        public unsafe static BitmapSource GaussianFilter(this BitmapSource source, double[,] matrix)
        {
            int width = source.PixelWidth;
            int height = source.PixelHeight;
            var bitmap = new WriteableBitmap(source);

            byte[] pixelBuffer = new byte[bitmap.BackBufferStride * (int)bitmap.Height];

            int filterWidth = matrix.GetLength(1);
            int filterHeight = matrix.GetLength(0);

            int filterOffset = (filterWidth - 1) / 2;
            int calculatedOffset = 0;
            int byteOffset = 0;

            double blue = 0.0;
            double green = 0.0;
            double red = 0.0;

            bitmap.Lock();

            var backBuffer = (byte*)bitmap.BackBuffer.ToPointer();

            for(int offsetY = filterOffset; offsetY < height - filterOffset; offsetY++)
            {
                var row = backBuffer + (offsetY * bitmap.BackBufferStride);

                for (int offsetX = filterOffset; offsetX < width - filterOffset; offsetX++)
                {
                    blue = 0;
                    green = 0;
                    red = 0;

                    byteOffset = offsetY * bitmap.BackBufferStride + offsetX * 4;

                    for(int filterY = -filterOffset; filterY <= filterOffset; filterY++)
                    {
                        for(int filterX = -filterOffset; filterX <= filterOffset; filterX++)
                        {
                            calculatedOffset = byteOffset + (filterX * 4) + (filterY * bitmap.BackBufferStride);

                            blue += (pixelBuffer[calculatedOffset]) * matrix[filterY + filterOffset, filterX + filterOffset];
                            green += (pixelBuffer[calculatedOffset + 1]) * matrix[filterY + filterOffset, filterX + filterOffset];
                            red += (pixelBuffer[calculatedOffset + 2]) * matrix[filterY + filterOffset, filterX + filterOffset];
                        }
                    }

                    blue = factor * blue + offset;
                    green = factor * green + offset;
                    red = factor * red + offset;

                    blue = blue > 255 ? 255 : blue < 0 ? 0 : blue;
                    green = green > 255 ? 255 : green < 0 ? 0 : green;
                    red = red > 255 ? 255 : red < 0 ? 0 : red;

                    row[offsetX * PIXEL_SIZE] = (byte)blue;
                    row[offsetX * PIXEL_SIZE + 1] = (byte)green;
                    row[offsetX * PIXEL_SIZE + 2] = (byte)red;
                    row[offsetX * PIXEL_SIZE + 3] = 255;
                }
            }

            /*var backBuffer = (byte*)bitmap.BackBuffer.ToPointer();

            for (int y = 0; y < height; y++)
            {
                var row = backBuffer + (y * bitmap.BackBufferStride);

                for (int x = 0; x < width; x++)
                {
                    for (int i = 0; i < PIXEL_SIZE; i++)
                    {
                        
                    }
                }
            }*/

            bitmap.AddDirtyRect(new System.Windows.Int32Rect(0, 0, width, height));
            bitmap.Unlock();

            return bitmap;
        }
    }
}
