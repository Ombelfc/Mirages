using _3DEngine.Components;
using _3DEngine.Utilities;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using Mirages.Model;
using System;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace Mirages.ViewModels
{
    public class RayTracerViewModel : ViewModelBase
    {
        private RayTracerModel model = new RayTracerModel();
        /// <summary>
        /// The data-model for the view
        /// </summary>
        public RayTracerModel Model
        {
            get => model;
            set
            {
                model = value;
                RaisePropertyChanged("Model");
            }
        }

        public RayTracerViewModel()
        {
            Model.WriteableBitmap = new WriteableBitmap(1450, 445, 96, 96, PixelFormats.Bgr32, null);
            Model.Image = Model.WriteableBitmap;
        }

        #region Commands

        public ICommand LoadScene => new RelayCommand(() =>
        {
            CompositionTarget.Rendering += RenderScene;

            Model.IsLoadSceneEnabled = false;
            Model.IsUnloadSceneEnabled = true;
        });

        public ICommand UnloadScene => new RelayCommand(() =>
        {
            CompositionTarget.Rendering -= RenderScene;

            Model.IsLoadSceneEnabled = true;
            Model.IsUnloadSceneEnabled = false;
        });

        #endregion

        /// <summary>
        /// Renders the scene on the image
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void RenderScene(object sender, EventArgs e)
        {
            IntPtr pBackBuffer = default(IntPtr);
            int backBufferStride = 0;

            RayTracer rayTracer = new RayTracer(1450, 445, (int x, int y, Color<byte> color) =>
            {
                DrawPixel(pBackBuffer, backBufferStride, x, y, color);
            });

            for (double x = 1; x < 360; x += 5)
            {
                // Reserve the back buffer for updates.
                Model.WriteableBitmap.Dispatcher.Invoke(() =>
                {
                    Model.WriteableBitmap.Lock();
                    pBackBuffer = Model.WriteableBitmap.BackBuffer;
                    backBufferStride = Model.WriteableBitmap.BackBufferStride;
                });

                rayTracer.Render(rayTracer.DefaultScene(x));

                // Release the back buffer and make it available for display.
                Model.WriteableBitmap.Dispatcher.Invoke(() =>
                {
                    // Specify the area of the bitmap that changed.
                    Model.WriteableBitmap.AddDirtyRect(new Int32Rect(0, 0, 1450, 445));
                    Model.WriteableBitmap.Unlock();
                });
            }
        }

        /// <summary>
        /// The draw-pixel method updates the WriteableBitmap by using
        /// unsafe code to write a pixel into the back buffer
        /// </summary>
        /// <param name="pBackBuffer"></param>
        /// <param name="backBufferStride"></param>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="color"></param>
        private void DrawPixel(IntPtr pBackBuffer, int backBufferStride, int x, int y, Color<byte> color)
        {
            unsafe
            {
                // Find the address of the pixel to draw.
                pBackBuffer += y * backBufferStride;
                pBackBuffer += x * 4;

                // Compute the pixel's color.
                int color_data = color.R << 16; // R
                color_data |= color.G << 8; // G
                color_data |= color.B << 0; // B

                // Assign the color data to the pixel.
                *((int*)pBackBuffer) = color_data;
            }
        }
    }
}
