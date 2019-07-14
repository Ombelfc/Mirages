using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using Mirages.Core.Clipping.Shapes;
using Mirages.Model.Clipping;
using Mirages.Utility.Extensions;
using System;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace Mirages.ViewModels
{
    public class ClippingViewModel : ViewModelBase
    {
        #region Private Fields

        /// <summary>
        /// Instance of the model for the clipping tab.
        /// </summary>
        private ClippingModel model = new ClippingModel();
        /// <summary>
        /// Temporary shape being drawn.
        /// </summary>
        private DrawingShape temporaryShape;
        /// <summary>
        /// First point drawn.
        /// </summary>
        private Point firstPoint;
        /// <summary>
        /// Last point drawn.
        /// </summary>
        private Point lastPoint;
        /// <summary>
        /// Point that the mouse last drew.
        /// </summary>
        private Point lastMovedToPoint;

        #endregion

        #region Properties

        /// <summary>
        /// The data-model for the view
        /// </summary>
        public ClippingModel Model
        {
            get => model;
            set
            {
                model = value;
                RaisePropertyChanged("Model");
            }
        }

        #endregion

        #region Constructors

        /// <summary>
        /// Instantiates a new clipping-view-model instance.
        /// </summary>
        public ClippingViewModel()
        {
            // Set the re-draw grid action
            Model.RedrawGrid = new Action<bool>(RedrawGrid);
            // Set the reset background action
            Model.ResetBackground = new Action(ResetBackground);
        }

        #endregion

        #region Commands

        #region Window Events

        /// <summary>
        /// Command executed when the clipping-tab is loaded.
        /// </summary>
        public ICommand ClippingTabLoaded => new RelayCommand(() =>
        {
            // If the image width and height are set
            if (!double.IsNaN(Model.ImageWidth) && !double.IsNaN(Model.ImageHeight))
            {
                // Initialize a new writeable-bitmap with the given image width and image height
                Model.WriteableBitmap = new WriteableBitmap((int)Model.ImageWidth, (int)Model.ImageHeight, 96, 96, PixelFormats.Bgr32, null);
                // Set the background of the image
                Model.WriteableBitmap.Clear(ColorExtensions.FromByteColor(Model.BackgroundColor));
                // Assign the writeable-bitmap as a source of the image
                Model.ImageSource = Model.WriteableBitmap;
                // Enable all the controls
                Model.AreControlsEnabled = true;
            }
        });

        /// <summary>
        /// Command executed when the clipping-tab's size is changed.
        /// </summary>
        public ICommand ClippingTabSizeChanged => new RelayCommand(() =>
        {
            // Disable all the controls
            Model.AreControlsEnabled = false;

            // If the image width and height are set
            if (!double.IsNaN(Model.ImageWidth) && !double.IsNaN(Model.ImageHeight))
            {
                // Initialize a new writeable-bitmap with the given image width and image height
                Model.WriteableBitmap = new WriteableBitmap((int)Model.ImageWidth, (int)Model.ImageHeight, 96, 96, PixelFormats.Bgr32, null);
                // Set the background of the image
                Model.WriteableBitmap.Clear(ColorExtensions.FromByteColor(Model.BackgroundColor));
                // Assign the writeable-bitmap as a source of the image
                Model.ImageSource = Model.WriteableBitmap;
                // Re-draw the grid
                RedrawGrid();
            }

            // Enable all the controls
            Model.AreControlsEnabled = true;
        });

        #endregion

        #region Button Clicks

        /// <summary>
        /// Switches the grid-shown flag (on/off).
        /// </summary>
        public ICommand DrawGrid => new RelayCommand(() =>
        {
            // Switch the grid-shown boolean (on/off)
            Model.IsGridShown = !Model.IsGridShown;
            // Re-draw the grid
            RedrawGrid();
        });

        /// <summary>
        /// Switches the move-polygon mode flag (on/off)
        /// </summary>
        public ICommand Move => new RelayCommand(() =>
        {
            // Switch the move-polygon mode boolean (on/off)
            Model.IsMovePolygonMode = !Model.IsMovePolygonMode;
        });

        /// <summary>
        /// Switches the removal mode flag (on/off)
        /// </summary>
        public ICommand Remove => new RelayCommand(() =>
        {
            // Switch the removal mode boolean (on/off)
            Model.IsRemovalMode = !Model.IsRemovalMode;
        });

        /// <summary>
        /// Switches the fill-polygon mode flag (on/off)
        /// </summary>
        public ICommand Fill => new RelayCommand(() =>
        {
            // Switch the fill-polygon mode boolean (on/off)
            Model.IsFillPolygonMode = !Model.IsFillPolygonMode;
        });

        /// <summary>
        /// Swicthes the clip-polygon mode flag (on/off)
        /// </summary>
        public ICommand Clip => new RelayCommand(() =>
        {
            // Switch the clip-polygon mode boolean (on/off)
            Model.IsClipPolygonMode = !Model.IsClipPolygonMode;
        });
        
        /// <summary>
        /// Clears and resets the image.
        /// </summary>
        public ICommand Clear => new RelayCommand(() =>
        {
            // Turn all buttons off
            Model.IsGridShown = false;
            Model.IsDrawingMode = false;
            Model.IsFillPolygonMode = false;
            Model.IsMovePolygonMode = false;
            Model.IsClipPolygonMode = false;
            Model.IsRemovalMode = false;

            // Re-instantiate the writeable-bitmap
            Model.WriteableBitmap = new WriteableBitmap((int)Model.ImageWidth, (int)Model.ImageHeight, 96, 96, PixelFormats.Bgr32, null);
            // Set the background of the image
            Model.WriteableBitmap.Clear(ColorExtensions.FromByteColor(Model.BackgroundColor));
            // Assign the writeable-bitmap as a source of the image
            Model.ImageSource = Model.WriteableBitmap;
        });

        #endregion

        #region Button Events / Commands

        /// <summary>
        /// Executes when the left-mouse-button is down inside the image area.
        /// </summary>
        public ICommand MouseLeftButtonDown => new RelayCommand<Point>(point  =>
        {
            if (point == null)
                return;

            // If no mode is selected already
            if (!Model.IsDrawingMode && !Model.IsMovePolygonMode && !Model.IsRemovalMode && !Model.IsFillPolygonMode && !Model.IsClipPolygonMode)
            {
                // Create a temporary shape
                temporaryShape = GetShape(Model.DrawingType);
                // We are in the drawing mode
                Model.IsDrawingMode = true;

                // Set the width of the shape
                temporaryShape.LineWidth = Model.LineWidth;
                // Set the color of the shape
                temporaryShape.Color = Model.DrawingColor;
                // The first point and the last point are set to the point where the mouse had been clicked
                firstPoint = lastPoint = point;
            }
        });

        /// <summary>
        /// Executes when the left-mouse-button is up inside the image area.
        /// </summary>
        public ICommand MouseLeftButtonUp => new RelayCommand<Point>(point =>
        {
            if (point == null)
                return;

            // If the drawing mode is set
            if (Model.IsDrawingMode && !Model.IsRemovalMode && !Model.IsMovePolygonMode && !Model.IsClipPolygonMode)
            {
                // If drawing a point
                if (temporaryShape is Point temporaryPoint)
                {
                    //temporaryPoint.DrawAndAdd(Model.WriteableBitmap, lastPoint, temporaryShape.Color, temporaryShape.LineWidth);
                }
            }
        });

        /// <summary>
        /// Executes when the mouse-cursor leaves the image area.
        /// </summary>
        public ICommand MouseLeave => new RelayCommand<Point>(point =>
        {
            if (point == null)
                return;
        });

        /// <summary>
        /// Executes when the mouse-cursor moves inside the mouse area.
        /// </summary>
        public ICommand MouseMove => new RelayCommand<MouseArgsAndPoint>(model =>
        {
            if (model == null)
                return;

            // If the drawing mode is set and point mouse is pressed and moved
            if (model.Args.LeftButton == MouseButtonState.Pressed && Model.IsDrawingMode && Model.DrawingType != DrawingType.Point && !Model.IsRemovalMode && !Model.IsClipPolygonMode && !Model.IsMovePolygonMode)
            {
                // Draw the line from start to end
                //Model.WriteableBitmap.DrawLine(lastPoint, model.Point, Model.DrawingColor, Model.LineWidth);
            }

            // Last point the mouse moved to
            lastMovedToPoint = model.Point;
        });

        /// <summary>
        /// Executes when the right-mouse button is down inside the image area.
        /// </summary>
        public ICommand MouseRightButtonDown => new RelayCommand<Point>(point =>
        {
            if (point == null)
                return;
        });

        #endregion

        #endregion

        #region Helpers

        /// <summary>
        /// Returns the type of the shape to draw based on the drawing-type selected.
        /// </summary>
        /// <param name="drawingType"></param>
        /// <returns></returns>
        private DrawingShape GetShape(DrawingType drawingType)
        {
            if (drawingType == DrawingType.Polygon)
                return new Polygon();
            else if (drawingType == DrawingType.Line)
                return new Line();
            else
                return new Point();
        }

        /// <summary>
        /// Resets the background color of the image.
        /// </summary>
        private void ResetBackground()
        {
            // Clear the background of the image.
            Model.WriteableBitmap.Clear(ColorExtensions.FromByteColor(Model.BackgroundColor));
            // Redraw the grid.
            RedrawGrid();
        }

        /// <summary>
        /// Re-draws the grid. Erasing the current grid if the flag is set to true.
        /// </summary>
        /// <param name="toErase"></param>
        private void RedrawGrid(bool toErase = false)
        {
            // If the grid is not to be shown, return
            if (!Model.IsGridShown)
                return;

            // If to erase the current grid before drawing a new one
            if (toErase)
                Model.WriteableBitmap.Clear(ColorExtensions.FromByteColor(Model.BackgroundColor));

            // Draw the vertical lines
            for (int i = 0; i <= Model.ImageWidth; i += Model.GridLineWidth)
                Model.WriteableBitmap.DrawGridLine(new Point(i, 0), new Point(i, Model.ImageHeight), Model.GridColor, 0);

            // Draw the horizontal lines
            for (int i = 0; i <= Model.ImageHeight; i += Model.GridLineWidth)
                Model.WriteableBitmap.DrawGridLine(new Point(0, i), new Point(Model.ImageWidth, i), Model.GridColor, 0);
        }

        /// <summary>
        /// Returns the distance between 2 points.
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        private double DistanceBetweenPoints(Point a, Point b)
        {
            return Math.Sqrt((a.X - b.X) * (a.X - b.X) + (a.Y - b.Y) * (a.Y - b.Y));
        }

        #endregion
    }
}