using _3DEngine.Utilities;
using System;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace Mirages.Core.Engine
{
    public class BufferedBitmap
    {
        private readonly byte[] _backBuffer;

        public int PixelWidth { get; }
        public int PixelHeight { get; }
        public float AspectRatio { get; }

        public WriteableBitmap BitmapSource { get; }

        public BufferedBitmap(int pixelWidth, int pixelHeight)
        {
            PixelWidth = pixelWidth;
            PixelHeight = pixelHeight;
            AspectRatio = PixelWidth / (float)PixelHeight;

            BitmapSource = new WriteableBitmap(PixelWidth, PixelHeight, 96, 96, PixelFormats.Bgr32, null);

            _backBuffer = new byte[PixelWidth * PixelHeight * 4];
        }

        public void Clear(Color32 color)
        {
            for (var offset = 0; offset < _backBuffer.Length; offset += 4)
            {
                _backBuffer[offset] = color.B;
                _backBuffer[offset + 1] = color.G;
                _backBuffer[offset + 2] = color.R;
                _backBuffer[offset + 3] = color.A;
            }
        }

        public void DrawPoint(int x, int y, Color32 color)
        {
            if (x < 0 || y < 0 || x >= PixelWidth || y >= PixelHeight)
                return;

            var offset = (x + y * PixelWidth) * 4;

            _backBuffer[offset] = color.B;
            _backBuffer[offset + 1] = color.G;
            _backBuffer[offset + 2] = color.R;
            _backBuffer[offset + 3] = color.A;
        }

        public void Present()
        {
            var rect = new Int32Rect(0, 0, PixelWidth, PixelHeight);
            BitmapSource.WritePixels(rect, _backBuffer, BitmapSource.BackBufferStride, 0);
        }
    }
}
