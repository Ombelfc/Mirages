using _3DEngine.Components;
using _3DEngine.Utilities;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using Mirages.Model.Clipping;
using Mirages.Utility;
using System;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Threading;
using Color = System.Windows.Media.Color;

namespace Mirages.ViewModels
{
    public class ClippingViewModel : ViewModelBase
    {
        #region Private Fields

        private bool IsGridShown = false;

        private DrawingShape temporaryShape;

        private Point firstPoint;
        private Point lastPoint;

        private Color BackgroundColor = Color.FromRgb(128, 128, 128);

        #endregion

        private ClippingModel model = new ClippingModel();
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

        public ClippingViewModel()
        {
            Model.ClearAndRedraw = new Action(ClearAndRedraw);
        }

        #region Commands

        public ICommand ClippingTabLoaded => new RelayCommand(() =>
        {
            if (!double.IsNaN(Model.ImageWidth) && !double.IsNaN(Model.ImageHeight))
            {
                Model.WriteableBitmap = new WriteableBitmap((int)Model.ImageWidth, (int)Model.ImageHeight, 96, 96, PixelFormats.Bgr32, null);
                Model.WriteableBitmap.Clear(Model.BackgroundColor);
                Model.ImageSource = Model.WriteableBitmap;
                Model.AreControlsEnabled = true;
            }
        });

        public ICommand DrawGrid => new RelayCommand(() =>
        {
            IsGridShown = !IsGridShown;

            RedrawGrid();
            //RedrawAllObjects();
        });

        public ICommand Move => new RelayCommand(() =>
        {
            Model.IsMovePolygonMode = !Model.IsMovePolygonMode;
        });

        public ICommand Remove => new RelayCommand(() =>
        {
            Model.IsRemovalMode = !Model.IsRemovalMode;
        });

        public ICommand Fill => new RelayCommand(() =>
        {
            Model.IsFillPolygonMode = !Model.IsFillPolygonMode;
        });

        public ICommand Clip => new RelayCommand(() =>
        {
            Model.IsClipPolygonMode = !Model.IsClipPolygonMode;
        });
        
        public ICommand Clear => new RelayCommand(() =>
        {
            Model.IsDrawingMode = false;
            Model.IsFillPolygonMode = false;
            Model.IsMovePolygonMode = false;
            Model.IsClipPolygonMode = false;

            //Model.WriteableBitmap = new WriteableBitmap()
        });

        #region Button Commands

        public ICommand MouseLeftButtonDown => new RelayCommand<Point>(point  =>
        {
            DrawingShape shape = null;

            if (!Model.IsDrawingMode && !Model.IsMovePolygonMode && !Model.IsClipPolygonMode && !Model.IsFillPolygonMode)
            {
                /*temporaryShape = new Polygon();
                Model.IsDrawingMode = true;

                temporaryShape.Width = Model.LineWidth;
                temporaryShape.Color = Model.DrawingColor;
                firstPoint = lastPoint = point;*/
            }
        });

        public ICommand MouseLeftButtonUp => new RelayCommand<Point>(point =>
        {
            if (Model.IsDrawingMode && !Model.IsMovePolygonMode && !Model.IsClipPolygonMode)
            {
                Point snappedPoint = SnapPoint(temporaryShape, point, 15);

                /*if (snappedPoint.Equals(firstPoint) && ((Polygon)temporaryShape).Lines.Count > 1)
                {
                    //ClosePolygon();
                    //EraseLine(lastPoint, lastMovePoint);
                    //RedrawAllObjects(Model.WriteableBitmap);
                }
                else
                {
                }*/
            }
        });

        public ICommand MouseLeave => new RelayCommand<Point>(point =>
        {

        });

        public ICommand MouseMove => new RelayCommand<MouseArgsAndPoint>(model =>
        {
            /*if (model.Args.LeftButton == MouseButtonState.Pressed && Model.IsDrawingMode)
            {
                Model.WriteableBitmap.Clear(BackgroundColor);

                //RedrawObject(temporaryShape);
                //RedrawAllObjects(Model.WriteableBitmap);

                Model.WriteableBitmap.DrawLine(lastPoint, model.Point, Model.DrawingColor, Model.LineWidth);
            }

            lastPoint = model.Point;*/
        });

        public ICommand MouseRightButtonDown => new RelayCommand<Point>(point =>
        {

        });

        #endregion

        #endregion

        #region Helpers

        private void ClearAndRedraw()
        {
            Model.WriteableBitmap.Clear(Model.BackgroundColor);

            RedrawGrid();
            //RedrawAllObjects();
        }

        private void RedrawGrid()
        {
            if (!IsGridShown)
                return;

            //for(int i =0; i<=Math.Max())
        }

        private Point SnapPoint(DrawingShape shape, Point point, int distance)
        {
            if (distance > 0)
            {
                if(shape is Polygon)
                {
                    foreach (var item in (shape as Polygon).Lines)
                    {
                        if (DistanceBetweenPoints(point, item.StartPoint) <= distance)
                            return item.StartPoint;

                        if (DistanceBetweenPoints(point, item.EndPoint) <= distance)
                            return item.EndPoint;
                    }
                }
                else if (shape is Line)
                {
                    var line = shape as Line;

                    if (DistanceBetweenPoints(point, line.StartPoint) <= distance)
                        return line.StartPoint;

                    if (DistanceBetweenPoints(point, line.EndPoint) <= distance)
                        return line.EndPoint;
                }
            }

            return point;
        }

        private double DistanceBetweenPoints(Point a, Point b)
        {
            return Math.Sqrt((a.X - b.X) * (a.X - b.X) + (a.Y - b.Y) * (a.Y - b.Y));
        }

        #endregion
    }
}
