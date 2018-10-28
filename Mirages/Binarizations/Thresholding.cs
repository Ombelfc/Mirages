using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace Mirages.Binarizations
{
    public static class Thresholding
    {
        private const int PIXEL_SIZE = 4;

        public unsafe static BitmapSource ToGThreshold(this BitmapSource source, int threshold)
        {
            int width = source.PixelWidth;
            int height = source.PixelHeight;
            var bitmap = new WriteableBitmap(source);

            bitmap.Lock();

            var backBuffer = (byte*)bitmap.BackBuffer.ToPointer();

            for (int y = 0; y < height; y++)
            {
                var row = backBuffer + (y * bitmap.BackBufferStride);

                for (int x = 0; x < width; x++)
                {
                    for (int i = 0; i < PIXEL_SIZE; i++)
                    {
                        if (row[x * PIXEL_SIZE + i] <= threshold) row[x * PIXEL_SIZE + i] = 0;
                        else row[x * PIXEL_SIZE + i] = 255;
                    }
                }
            }

            bitmap.AddDirtyRect(new System.Windows.Int32Rect(0, 0, width, height));
            bitmap.Unlock();

            return bitmap;
        }

        public unsafe static BitmapSource ToHThreshold(this BitmapSource source, int threshold)
        {
            int width = source.PixelWidth;
            int height = source.PixelHeight;
            var bitmap = new WriteableBitmap(source);

            bitmap.Lock();

            var backBuffer = (byte*)bitmap.BackBuffer.ToPointer();

            for (int y = 0; y < height; y++)
            {
                var row = backBuffer + (y * bitmap.BackBufferStride);

                for (int x = 0; x < width; x++)
                {
                    for (int i = 0; i < PIXEL_SIZE; i++)
                    {
                        if (row[x * PIXEL_SIZE + i] <= threshold) row[x * PIXEL_SIZE + i] = 0;
                    }
                }
            }

            bitmap.AddDirtyRect(new System.Windows.Int32Rect(0, 0, width, height));
            bitmap.Unlock();

            return bitmap;
        }

        public unsafe static BitmapSource ToBernsen(this BitmapSource source, int threshold)
        {
            int width = source.PixelWidth;
            int height = source.PixelHeight;
            var bitmap = new WriteableBitmap(source);

            var windowSize = 11;
            var eps = 50;
            var neighbours = 4;

            bitmap.Lock();

            var backBuffer = (byte*)bitmap.BackBuffer.ToPointer();

            for(int y = 1; y < height - 1; y++)
            {
                for(int x = 1; x < width - 1; x++)
                {
                    var pixelValue = (backBuffer[4 * x + (y * bitmap.BackBufferStride) + 1] + backBuffer[4 * x + (y * bitmap.BackBufferStride) + 2] + backBuffer[4 * x + (y * bitmap.BackBufferStride) + 3]) / 3;

                    var row = y;
                    var col = x;
                    var values = new List<int>();

                    for (int yy = row - 1; yy < row + 1; yy++)
                    {
                        for(int xx = col - 1; xx < col + 1; xx++)
                        {
                            values.Add((backBuffer[4 * xx + (yy * bitmap.BackBufferStride) + 1] + backBuffer[4 * xx + (yy * bitmap.BackBufferStride) + 2] + backBuffer[4 * xx + (yy * bitmap.BackBufferStride) + 3]) / 3);
                        }
                    }

                    var maxValue = values.Max();
                    var minValue = values.Min();
                    int pixelThreshold;

                    if (maxValue - minValue <= eps)
                    {
                        pixelThreshold = threshold;
                    }
                    else
                    {
                        pixelThreshold = Convert.ToInt32((maxValue + minValue) * 0.5);
                    }

                    if (pixelValue > pixelThreshold)
                    {
                        backBuffer[4 * x + (y * bitmap.BackBufferStride)] = 255;
                        backBuffer[4 * x + (y * bitmap.BackBufferStride) + 1] = 255;
                        backBuffer[4 * x + (y * bitmap.BackBufferStride) + 2] = 255;
                        backBuffer[4 * x + (y * bitmap.BackBufferStride) + 3] = 255;
                    }
                    else if (pixelValue <= pixelThreshold)
                    {
                        backBuffer[4 * x + (y * bitmap.BackBufferStride)] = 0;
                        backBuffer[4 * x + (y * bitmap.BackBufferStride) + 1] = 0;
                        backBuffer[4 * x + (y * bitmap.BackBufferStride) + 2] = 0;
                        backBuffer[4 * x + (y * bitmap.BackBufferStride) + 3] = 0;
                    }
                }
            }

            /*if(windowSize % 2 != 0)
            {
                // Neighbors to consider
                int radius = Convert.ToInt32((windowSize - 1) * 0.5);
                var backBuffer = (byte*)bitmap.BackBuffer.ToPointer();

                for(int y = 0; y < height; y++)
                {
                    var row = backBuffer + (y * bitmap.BackBufferStride);

                    for(int x = 0; x < width; x++)
                    {
                        int pixelValue = (int)((row[x * PIXEL_SIZE + 1] * 0.3) + (row[x * PIXEL_SIZE + 2] * 0.59) + (row[x * PIXEL_SIZE + 3] * 0.11));

                        if (y >= radius && y < height - radius)
                        {
                            if(x >= radius && x < width - radius)
                            {
                                var values = new List<int>();

                                for(int i = y - radius; i < y + radius; i++)
                                {
                                    var innerRow = backBuffer + (i * bitmap.BackBufferStride);

                                    for(int k = x - radius; k < x + radius; k++)
                                    {
                                        values.Add((int)((innerRow[k * PIXEL_SIZE + 1] * 0.3) + (innerRow[k * PIXEL_SIZE + 2] * 0.59) + (innerRow[k * PIXEL_SIZE + 3] * 0.11)));
                                    }
                                }

                                var maxValue = values.Max();
                                var minValue = values.Min();
                                int pixelThreshold;

                                if(maxValue - minValue <= eps)
                                {
                                    pixelThreshold = threshold;
                                }
                                else
                                {
                                    pixelThreshold = Convert.ToInt32((maxValue + minValue) * 0.5);
                                }

                                if(x >= windowSize && y >= windowSize && x <= width - windowSize && y <= height - windowSize)
                                {
                                    if (pixelValue > pixelThreshold)
                                    {
                                        for (int p = 0; p < PIXEL_SIZE; p++)
                                        {
                                            row[x * PIXEL_SIZE + p] = 255;
                                        }
                                    }
                                    else if (pixelValue <= pixelThreshold)
                                    {
                                        for (int p = 0; p < PIXEL_SIZE; p++)
                                        {
                                            row[x * PIXEL_SIZE + p] = 0;
                                        }
                                    }
                                }
                                else
                                {
                                    for (int p = 0; p < PIXEL_SIZE; p++)
                                    {
                                        row[x * PIXEL_SIZE + p] = (byte)pixelThreshold;
                                    }
                                }
                            }
                        }
                    }
                }
            }*/

            bitmap.AddDirtyRect(new System.Windows.Int32Rect(0, 0, width, height));
            bitmap.Unlock();

            return bitmap;
        }
    }
}
