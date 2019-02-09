using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media.Imaging;

namespace Mirages.ElementaryAlgorithms
{
    public static class GrayScale
    {
        private const int PIXEL_SIZE = 4;

        public unsafe static BitmapSource ToGrayScale(this BitmapSource source)
        {
            int width = source.PixelWidth;
            int height = source.PixelHeight;
            var bitmap = new WriteableBitmap(source);

            bitmap.Lock();

            var backBuffer = (byte*)bitmap.BackBuffer.ToPointer();

            for(int y = 0; y < height; y++)
            {
                var row = backBuffer + (y * bitmap.BackBufferStride);

                for(int x = 0; x < width; x++)
                {
                    var grayScale = (byte)((row[x * PIXEL_SIZE] * 0.3) + 
                                           (row[x * PIXEL_SIZE + 1] * 0.59) + 
                                           (row[x * PIXEL_SIZE + 2] * 0.11));

                    for(int i = 0; i < PIXEL_SIZE; i++)
                    {
                        row[x * PIXEL_SIZE + i] = grayScale;
                    }
                }
            }

            bitmap.AddDirtyRect(new Int32Rect(0, 0, width, height));
            bitmap.Unlock();

            return bitmap;
        }
    }
}
