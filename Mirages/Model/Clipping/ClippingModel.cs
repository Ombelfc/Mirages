using GalaSoft.MvvmLight;
using Mirages.Core.Clipping.Shapes;
using Mirages.Infrastructure.Components.Colors;
using Mirages.Infrastructure.Extensions;
using Mirages.Utility.Extensions;
using System;
using System.Collections.Generic;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace Mirages.Model.Clipping
{
    public class ClippingModel : ObservableObject
    {
        #region Non-Binding Properties

        #region Actions

        public Action<bool> RedrawGrid { get; set; }
        public Action ResetBackground { get; set; }

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

        private bool isGridShown = false;
        private bool isDrawingMode = false;
        private bool isMovePolygonMode = false;
        private bool isClipPolygonMode = false;
        private bool isFillPolygonMode = false;
        private bool isRemovalMode = false;

        public bool IsGridShown
        {
            get => isGridShown;
            set
            {
                isGridShown = value;
                GridButtonBackgroundColor = value ? BrushesExtensions.EnabledBrush : BrushesExtensions.ButtonBrush;
                ResetBackground();
            }
        }

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
                MoveButtonBackground = value ? BrushesExtensions.EnabledBrush : BrushesExtensions.ButtonBrush;

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
                ClipButtonBackground = value ? BrushesExtensions.EnabledBrush : BrushesExtensions.ButtonBrush;
            }
        }

        public bool IsFillPolygonMode
        {
            get => isFillPolygonMode;
            set
            {
                isFillPolygonMode = value;
                FillButtonBackground = value ? BrushesExtensions.EnabledBrush : BrushesExtensions.ButtonBrush;
            }
        }

        public bool IsRemovalMode
        {
            get => isRemovalMode;
            set
            {
                isRemovalMode = value;
                RemoveButtonBackground = value ? BrushesExtensions.EnabledBrush : BrushesExtensions.ButtonBrush;
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
                RedrawGrid(true);

                RaisePropertyChanged("GridLineWidth");
            }
        }

        #region Colors

        private ByteColor backgroundColor = ColorsExtensions.LightGray;
        /// <summary>
        /// Background color of the canvas.
        /// </summary>
        public ByteColor BackgroundColor
        {
            get => backgroundColor;
            set
            {
                backgroundColor = value;
                ResetBackground();

                RaisePropertyChanged("BackgroundColor");
            }
        }

        private ByteColor gridColor = ColorsExtensions.AliceBlue;
        /// <summary>
        /// Color of the grid lines.
        /// </summary>
        public ByteColor GridColor
        {
            get => gridColor;
            set
            {
                gridColor = value;
                RedrawGrid(false);

                RaisePropertyChanged("GridColor");
            }
        }

        private ByteColor drawingColor = ColorsExtensions.OrangeRed;
        /// <summary>
        /// Color of the line to draw.
        /// </summary>
        public ByteColor DrawingColor
        {
            get => drawingColor;
            set
            {
                drawingColor = value;
                RaisePropertyChanged("DrawingColor");
            }
        }

        private ByteColor fillColor = ColorsExtensions.Firebrick;
        /// <summary>
        /// Color of the area to fill.
        /// </summary>
        public ByteColor FillColor
        {
            get => fillColor;
            set
            {
                fillColor = value;
                RaisePropertyChanged("FillColor");
            }
        }

        #endregion

        #region Button Background-Colors

        private Brush gridButtonBackgroundColor = BrushesExtensions.ButtonBrush;
        /// <summary>
        /// The background color of the grid-button.
        /// </summary>
        public Brush GridButtonBackgroundColor
        {
            get => gridButtonBackgroundColor;
            set
            {
                gridButtonBackgroundColor = value;
                RaisePropertyChanged("GridButtonBackgroundColor");
            }
        }

        private Brush moveButtonBackground = BrushesExtensions.ButtonBrush;
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

        private Brush removeButtonBackground = BrushesExtensions.ButtonBrush;
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

        private Brush clipButtonBackground = BrushesExtensions.ButtonBrush;
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

        private Brush fillButtonBackground = BrushesExtensions.ButtonBrush;
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

        #endregion

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
