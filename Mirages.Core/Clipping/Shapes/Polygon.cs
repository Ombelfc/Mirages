using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Media.Imaging;

namespace Mirages.Core.Clipping.Shapes
{
    public class Polygon : DrawingShape
    {
        #region Fields

        public List<Line> Lines = new List<Line>();

        #endregion

        public Polygon() : base() { }

        #region Overrides

        public override DrawingShape Clone()
        {
            throw new NotImplementedException();
        }

        public override void DrawObject(WriteableBitmap writeableBitmap)
        {
            throw new NotImplementedException();
        }

        public override void EraseObject(List<DrawingShape> list, WriteableBitmap writeableBitmap, System.Windows.Media.Color color)
        {
            throw new NotImplementedException();
        }

        public override void HighlightObject(bool ifHighlight, WriteableBitmap writeableBitmap, System.Windows.Media.Color color)
        {
            throw new NotImplementedException();
        }

        public override bool IfPointCloseToBoundary(Point point)
        {
            throw new NotImplementedException();
        }

        public override DrawingShape MoveObject(Vector2 vector)
        {
            throw new NotImplementedException();
        }

        public override void UpdateBoundaries()
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
