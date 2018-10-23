using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace Mirages.ElementaryAlgorithms
{
    public static class Contrast
    {
        private const int PIXEL_SIZE = 4;

        public unsafe static BitmapSource InhanceContrast(this BitmapSource source, int contrastValue)
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
                        row[x * PIXEL_SIZE + i] = (byte)((Enhance(row[x * PIXEL_SIZE + i], contrastValue) < 0) ? 0 :
                                                        (Enhance(row[x * PIXEL_SIZE + i], contrastValue) < 255.0) ?
                                                        (int)Enhance(row[x * PIXEL_SIZE + i], contrastValue) : 255);
                    }
                }
            }

            bitmap.AddDirtyRect(new System.Windows.Int32Rect(0, 0, width, height));
            bitmap.Unlock();

            return bitmap;
        }

        private static double Enhance(double channelValue, double contrastCoefficient)
        {
            return ((((channelValue / 255.0) - 0.5) * contrastCoefficient) + 0.5) * 255.0;
        }
    }
}
