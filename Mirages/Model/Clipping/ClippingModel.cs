using _3DEngine.Components;
using GalaSoft.MvvmLight;
using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace Mirages.Model.Clipping
{
    public class ClippingModel : ObservableObject
    {
        #region Private Fields

        #region Brushes

        private readonly Brush buttonBrush = Brushes.LightGray;
        private readonly Brush enabledBrush = Brushes.LightGreen;

        #endregion

        #endregion

        #region Non-Binding Properties

        #region Actions

        public Action ClearAndRedraw { get; set; }

        #endregion

        /// <summary>
        /// Holds the back-buffer pushed to the image-source.
        /// </summary>
        public WriteableBitmap WriteableBitmap { get; set; }

        /// <summary>
        /// List of shapes drawn of the canvas.
        /// </summary>
        public List<DrawingShape> Shapes { get; set; }

        #region Modes

        private bool isDrawingMode = false;
        private bool isMovePolygonMode = false;
        private bool isClipPolygonMode = false;
        private bool isFillPolygonMode = false;
        private bool isRemovalMode = false;

        public bool IsDrawingMode
        {
            get => isDrawingMode;
            set
            {
                isDrawingMode = value;
                IsDrawingColorPickerEnabled = !value;
                IsLineWidthEnabled = !value;
            }
        }

        public bool IsMovePolygonMode
        {
            get => isMovePolygonMode;
            set
            {
                isMovePolygonMode = value;
                MoveButtonBackground = value ? enabledBrush : buttonBrush;

                //DrawGrid();
                //RedrawAllObjects();
            }
        }

        public bool IsClipPolygonMode
        {
            get => isClipPolygonMode;
            set
            {
                isClipPolygonMode = value;
                ClipButtonBackground = value ? enabledBrush : buttonBrush;
            }
        }

        public bool IsFillPolygonMode
        {
            get => isFillPolygonMode;
            set
            {
                isFillPolygonMode = value;
                FillButtonBackground = value ? enabledBrush : buttonBrush;
            }
        }

        public bool IsRemovalMode
        {
            get => IsRemovalMode;
            set
            {
                isRemovalMode = value;
                RemoveButtonBackground = value ? enabledBrush : buttonBrush;
            }
        }

        #endregion

        #endregion

        #region Binding Properties

        private bool areControlsEnabled = false;
        /// <summary>
        /// Determines whether the controls of the clipping-tab are enabled.
        /// </summary>
        public bool AreControlsEnabled
        {
            get => areControlsEnabled;
            set
            {
                areControlsEnabled = value;
                RaisePropertyChanged("AreControlsEnabled");
            }
        }

        #region Image

        private double imageWidth;
        /// <summary>
        /// Width of the image once rendered.
        /// </summary>
        public double ImageWidth
        {
            get => imageWidth;
            set
            {
                imageWidth = value > 0 ? value : double.NaN;
            }
        }

        private double imageHeight;
        /// <summary>
        /// Height of the image once rendered.
        /// </summary>
        public double ImageHeight
        {
            get => imageHeight;
            set
            {
                imageHeight = value > 0 ? value : double.NaN;
            }
        }

        #endregion

        private DrawingType drawingType = DrawingType.Polygon;
        /// <summary>
        /// Type of the shape to draw.
        /// </summary>
        public DrawingType DrawingType
        {
            get => drawingType;
            set
            {
                drawingType = value;
                RaisePropertyChanged("DrawingType");
            }
        }

        private int lineWidth = 1;
        /// <summary>
        /// Width of the line to draw.
        /// </summary>
        public int LineWidth
        {
            get => lineWidth;
            set
            {
                lineWidth = value;
                //ClearAndRedraw();

                RaisePropertyChanged("LineWidth");
            }
        }

        private int gridLineWidth = 25;
        /// <summary>
        /// Width of the line to draw.
        /// </summary>
        public int GridLineWidth
        {
            get => gridLineWidth;
            set
            {
                gridLineWidth = value;

                //DrawGrid(true);
                //RedrawAllObjects();

                RaisePropertyChanged("GridLineWidth");
            }
        }

        private Color backgroundColor = Colors.LightGray;
        /// <summary>
        /// Background color of the canvas.
        /// </summary>
        public Color BackgroundColor
        {
            get => backgroundColor;
            set
            {
                backgroundColor = value;
                ClearAndRedraw();

                RaisePropertyChanged("BackgroundColor");
            }
        }

        private Color gridColor = Colors.AliceBlue;
        /// <summary>
        /// Color of the grid lines.
        /// </summary>
        public Color GridColor
        {
            get => gridColor;
            set
            {
                gridColor = value;
                //ClearAndRedraw();

                RaisePropertyChanged("GridColor");
            }
        }

        private Color drawingColor = Colors.OrangeRed;
        /// <summary>
        /// Color of the line to draw.
        /// </summary>
        public Color DrawingColor
        {
            get => drawingColor;
            set
            {
                drawingColor = value;
                RaisePropertyChanged("DrawingColor");
            }
        }

        private Color fillColor = Colors.Firebrick;
        /// <summary>
        /// Color of the area to fill.
        /// </summary>
        public Color FillColor
        {
            get => fillColor;
            set
            {
                fillColor = value;
                RaisePropertyChanged("FillColor");
            }
        }

        private ImageSource image;
        /// <summary>
        /// 
        /// </summary>
        public ImageSource ImageSource
        {
            get => image;
            set
            {
                image = value;
                RaisePropertyChanged("ImageSource");
            }
        }

        private Brush moveButtonBackground;
        /// <summary>
        /// The background color of the move-button.
        /// </summary>
        public Brush MoveButtonBackground
        {
            get => moveButtonBackground;
            set
            {
                moveButtonBackground = value;
                RaisePropertyChanged("MoveButtonBackground");
            }
        }

        private Brush removeButtonBackground;
        /// <summary>
        /// The background color of the remove-button.
        /// </summary>
        public Brush RemoveButtonBackground
        {
            get => removeButtonBackground;
            set
            {
                removeButtonBackground = value;
                RaisePropertyChanged("RemoveButtonBackground");
            }
        }

        private Brush clipButtonBackground;
        /// <summary>
        /// The background color of the clip-button.
        /// </summary>
        public Brush ClipButtonBackground
        {
            get => clipButtonBackground;
            set
            {
                clipButtonBackground = value;
                RaisePropertyChanged("ClipButtonBackground");
            }
        }

        private Brush fillButtonBackground;
        /// <summary>
        /// The background color of the fill-button.
        /// </summary>
        public Brush FillButtonBackground
        {
            get => fillButtonBackground;
            set
            {
                fillButtonBackground = value;
                RaisePropertyChanged("FillButtonBackground");
            }
        }

        #region Enabled Booleans

        private bool isDrawingColorPickerEnabled = false;
        /// <summary>
        /// 
        /// </summary>
        public bool IsDrawingColorPickerEnabled
        {
            get => isDrawingColorPickerEnabled;
            set
            {
                isDrawingColorPickerEnabled = value;
                RaisePropertyChanged("IsLineWidthEnabled");
            }
        }

        private bool isLineWidthEnabled = false;
        /// <summary>
        /// 
        /// </summary>
        public bool IsLineWidthEnabled
        {
            get => isLineWidthEnabled;
            set
            {
                isLineWidthEnabled = value;
                RaisePropertyChanged("IsLineWidthEnabled");
            }
        }

        #endregion

        #endregion

        public ClippingModel()
        {
            Shapes = new List<DrawingShape>();
        }
    }
}
