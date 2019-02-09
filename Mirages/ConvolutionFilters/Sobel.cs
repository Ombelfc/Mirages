using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace Mirages.ConvolutionFilters
{
    public static class Sobel
    {
        private const int PIXEL_SIZE = 4;

        public unsafe static BitmapSource SobelFilter(this BitmapSource source, double[,] matrixX, double[,] matrixY)
        {
            int width = source.PixelWidth;
            int height = source.PixelHeight;
            var bitmap = new WriteableBitmap(source);

            bitmap.Lock();

            var backBuffer = (byte*)bitmap.BackBuffer.ToPointer();

            double redX = 0.0;
            double greenX = 0.0;
            double blueX = 0.0;

            double redY = 0.0;
            double greenY = 0.0;
            double blueY = 0.0;

            double redTotal = 0.0;
            double greenTotal = 0.0;
            double blueTotal = 0.0;

            int filterOffset = 1;
            int calcOffset = 0;
            int byteOffset = 0;

            for (int y = filterOffset; y < height - filterOffset; y++)
            {
                for(int x = filterOffset; x < width - filterOffset; x++)
                {
                    redX = greenX = blueX = 0;
                    redY = greenY = blueY = 0;
                    redTotal = greenTotal = blueTotal = 0.0;

                    byteOffset = y * bitmap.BackBufferStride + x * 4;

                    for(int filterY = -filterOffset; filterY <= filterOffset; filterY++)
                    {
                        for(int filterX = -filterOffset; filterX <= filterOffset; filterX++)
                        {
                            calcOffset = byteOffset + (filterX * 4) + (filterY * bitmap.BackBufferStride);

                            redX += (double)(backBuffer[calcOffset] * matrixX[filterY + filterOffset, filterX + filterOffset]);
                            greenX += (double)(backBuffer[calcOffset + 1] * matrixX[filterY + filterOffset, filterX + filterOffset]);
                            blueX += (double)(backBuffer[calcOffset + 2] * matrixX[filterY + filterOffset, filterX + filterOffset]);

                            redY += (double)(backBuffer[calcOffset] * matrixY[filterY + filterOffset, filterX + filterOffset]);
                            greenY += (double)(backBuffer[calcOffset + 1] * matrixY[filterY + filterOffset, filterX + filterOffset]);
                            blueY += (double)(backBuffer[calcOffset + 2] * matrixY[filterY + filterOffset, filterX + filterOffset]);
                        }
                    }

                    redTotal = Math.Sqrt((redX * redX) + (redY * redY));
                    greenTotal = Math.Sqrt((greenX * greenX) + (greenY * greenY));
                    blueTotal = Math.Sqrt((blueX * blueX) + (blueY * blueY));

                    if (blueTotal > 255)
                    { blueTotal = 255; }
                    else if (blueTotal < 0)
                    { blueTotal = 0; }

                    if (greenTotal > 255)
                    { greenTotal = 255; }
                    else if (greenTotal < 0)
                    { greenTotal = 0; }

                    if (redTotal > 255)
                    { redTotal = 255; }
                    else if (redTotal < 0)
                    { redTotal = 0; }

                    backBuffer[byteOffset] = (byte)(redTotal);
                    backBuffer[byteOffset + 1] = (byte)(greenTotal);
                    backBuffer[byteOffset + 2] = (byte)(blueTotal);
                    backBuffer[byteOffset + 3] = (byte)(255);
                }
            }

            bitmap.AddDirtyRect(new System.Windows.Int32Rect(0, 0, width, height));
            bitmap.Unlock();

            return bitmap;
        }
    }
}
