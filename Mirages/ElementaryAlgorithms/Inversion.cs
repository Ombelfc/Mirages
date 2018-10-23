using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media.Imaging;

namespace Mirages.ElementaryAlgorithms
{
    public static class Inversion
    {
        private const int PIXEL_SIZE = 4;

        public unsafe static BitmapSource Invert(this BitmapSource source)
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
                    // Red
                    row[x * PIXEL_SIZE + 1] = (byte)((255 - row[x * PIXEL_SIZE + 1]));
                    // Green
                    row[x * PIXEL_SIZE + 2] = (byte)((255 - row[x * PIXEL_SIZE + 2]));
                    // Blue
                    row[x * PIXEL_SIZE + 3] = (byte)((255 - row[x * PIXEL_SIZE + 3]));
                }
            }

            bitmap.AddDirtyRect(new Int32Rect(0, 0, width, height));
            bitmap.Unlock();

            return bitmap;
        }
    }
}
