using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace Mirages.ConvolutionFilters
{
    public static class RobertsCross
    {
        private const int PIXEL_SIZE = 4;

        public unsafe static BitmapSource RobertsCrossFilter(this BitmapSource source, double[,] matrixX, double[,] matrixY)
        {
            int width = source.PixelWidth;
            int height = source.PixelHeight;
            var bitmap = new WriteableBitmap(source);

            int filterOffset = (matrixX.GetLength(1) - 1) / 2;

            bitmap.Lock();

            var backBuffer = (byte*)bitmap.BackBuffer.ToPointer();

            for (int y = 0; y < height - filterOffset; y++)
            {
                var row = backBuffer + (y * bitmap.BackBufferStride);

                for (int x = 0; x < width - filterOffset; x++)
                {
                    var redX = 0.0;
                    var greenX = 0.0;
                    var blueX = 0.0;

                    var redY = 0.0;
                    var greenY = 0.0;
                    var blueY = 0.0;

                    for (int filterY = -filterOffset; filterY <= filterOffset; filterY++)
                    {
                        for (int filterX = -filterOffset; filterX <= filterOffset; filterX++)
                        {
                            redX += (byte)(row[x * PIXEL_SIZE] * matrixX[filterY + filterOffset, filterX + filterOffset]);
                            greenX += (byte)(row[x * PIXEL_SIZE + 1] * matrixX[filterY + filterOffset, filterX + filterOffset]);
                            blueX += (byte)(row[x * PIXEL_SIZE + 2] * matrixX[filterY + filterOffset, filterX + filterOffset]);
                        }
                    }

                    for (int filterY = -filterOffset; filterY <= filterOffset; filterY++)
                    {
                        for (int filterX = -filterOffset; filterX <= filterOffset; filterX++)
                        {
                            redY += (byte)(row[x * PIXEL_SIZE] * matrixX[filterY + filterOffset, filterX + filterOffset]);
                            greenY += (byte)(row[x * PIXEL_SIZE + 1] * matrixX[filterY + filterOffset, filterX + filterOffset]);
                            blueY += (byte)(row[x * PIXEL_SIZE + 2] * matrixX[filterY + filterOffset, filterX + filterOffset]);
                        }
                    }

                    var red = Math.Sqrt((redX * redX) + (redY * redY));
                    var green = Math.Sqrt((greenX * greenX) + (greenY * greenY));
                    var blue = Math.Sqrt((blueX * blueX) + (blueY * blueY));

                    red = red > 255 ? 255 : red < 0 ? 0 : red;
                    green = green > 255 ? 255 : green < 0 ? 0 : green;
                    blue = blue > 255 ? 255 : blue < 0 ? 0 : blue;

                    row[x * PIXEL_SIZE] = (byte)red;
                    row[x * PIXEL_SIZE + 1] = (byte)green;
                    row[x * PIXEL_SIZE + 2] = (byte)blue;
                }
            }

            bitmap.AddDirtyRect(new System.Windows.Int32Rect(0, 0, width, height));
            bitmap.Unlock();

            return bitmap;
        }
    }
}
