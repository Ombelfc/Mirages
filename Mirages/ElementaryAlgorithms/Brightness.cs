using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace Mirages.ElementaryAlgorithms
{
    public static class Brightness
    {
        private const int PIXEL_SIZE = 4;

        public unsafe static BitmapSource InhanceBrightness(this BitmapSource source, int brightnessValue)
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
                    for(int i = 0; i < PIXEL_SIZE; i++)
                    {
                        if (row[x * PIXEL_SIZE + i] + brightnessValue < 0) row[x * PIXEL_SIZE + i] = 0;
                        else if (row[x * PIXEL_SIZE + i] + brightnessValue > 255) row[x * PIXEL_SIZE + i] = 255;
                        else row[x * PIXEL_SIZE + i] = (byte)(row[x * PIXEL_SIZE + i] + brightnessValue);
                    }
                }
            }

            bitmap.AddDirtyRect(new System.Windows.Int32Rect(0, 0, width, height));
            bitmap.Unlock();

            return bitmap;
        }
    }
}
