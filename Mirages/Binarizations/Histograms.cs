using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace Mirages.Binarizations
{
    public static class Histograms
    {
        private const int PIXEL_SIZE = 4;

        public unsafe static int[] GenerateHistogram(this BitmapSource source)
        {
            int width = source.PixelWidth;
            int height = source.PixelHeight;
            var bitmap = new WriteableBitmap(source);

            bitmap.Lock();

            int[] histogram = new int[256];
            histogram.Select(a => a = 0);

            var backBuffer = (byte*)bitmap.BackBuffer.ToPointer();

            for(int y = 0; y < height; y++)
            {
                var row = backBuffer + (y * bitmap.BackBufferStride);

                for(int x = 0; x < width; x++)
                {
                    var value = (int)((row[x * PIXEL_SIZE] * 0.3) + (row[x * PIXEL_SIZE + 1] * 0.59) + (row[x * PIXEL_SIZE + 2] * 0.11));
                    histogram[value]++;
                }
            }

            bitmap.AddDirtyRect(new System.Windows.Int32Rect(0, 0, width, height));
            bitmap.Unlock();

            return histogram;
        }

        public unsafe static (int[], BitmapSource) GenerateNormalizeHistogram(this BitmapSource source)
        {
            int width = source.PixelWidth;
            int height = source.PixelHeight;
            var bitmap = new WriteableBitmap(source);

            bitmap.Lock();

            int[] histogram = new int[256];

            var backBuffer = (byte*)bitmap.BackBuffer.ToPointer();

            for (int y = 0; y < height; y++)
            {
                var row = backBuffer + (y * bitmap.BackBufferStride);

                for (int x = 0; x < width; x++)
                {
                    var value = (int)((row[x * PIXEL_SIZE] * 0.3) + (row[x * PIXEL_SIZE + 1] * 0.59) + (row[x * PIXEL_SIZE + 2] * 0.11));
                    histogram[value]++;
                }
            }

            int max = FindMaxIndex(histogram);
            int min = FindMinIndex(histogram);

            histogram = new int[256];
            for (int y = 0; y < height; y++)
            {
                var row = backBuffer + (y * bitmap.BackBufferStride);

                for (int x = 0; x < width; x++)
                {
                    var innerIntensity = (byte)((row[x * PIXEL_SIZE] * 0.3) + (row[x * PIXEL_SIZE + 1] * 0.59) + (row[x * PIXEL_SIZE + 2] * 0.11));
                    var outerIntensity = (byte)(((innerIntensity - min) * 255 / (max - min)));

                    for (int i = 0; i < PIXEL_SIZE; i++)
                    {
                        row[x * PIXEL_SIZE + i] = outerIntensity;
                    }

                    var value = (int)((row[x * PIXEL_SIZE] * 0.3) + (row[x * PIXEL_SIZE + 1] * 0.59) + (row[x * PIXEL_SIZE + 2] * 0.11));
                    histogram[value]++;
                }
            }

            bitmap.AddDirtyRect(new System.Windows.Int32Rect(0, 0, width, height));
            bitmap.Unlock();

            return (histogram, bitmap);
        }

        private static int FindMaxIndex(int[] histogram)
        {
            int max = 0;
            for (int i = histogram.Length - 1; i > 0; i--)
            {
                if (histogram[i] > 0)
                {
                    max = i;
                    break;
                }
            }

            return max;
        }

        private static int FindMinIndex(int[] histogram)
        {
            int min = 0;
            for (int i = 0; i < histogram.Length; i++)
            {
                if (histogram[i] > 0)
                {
                    min = i;
                    break;
                }
            }

            return min;
        }
    }
}
