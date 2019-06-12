﻿using Mirages.Infrastructure.Components;
using Mirages.Infrastructure.Components.Colors;
using System;
using System.Collections.Generic;
using System.Windows.Media.Imaging;

namespace Mirages.Core.Clipping.Shapes
{
    public class Line : DrawingShape
    {
        #region Fields

        /// <summary>
        /// Starting point of the line.
        /// </summary>
        public Point StartPoint { get; set; }

        /// <summary>
        /// End point of the line.
        /// </summary>
        public Point EndPoint { get; set; }

        #endregion

        public Line()
        {
            StartPoint = EndPoint = new Point(0, 0);
        }

        public Line(Point startPoint, Point endPoint)
        {
            StartPoint = startPoint;
            EndPoint = endPoint;
        }

        #region Overrides

        public override DrawingShape Clone()
        {
            throw new NotImplementedException();
        }

        public override void DrawObject(WriteableBitmap writeableBitmap)
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

        public override void EraseObject(List<DrawingShape> list, WriteableBitmap writeableBitmap, DoubleColor color)
        {
            throw new NotImplementedException();
        }

        public override void HighlightObject(bool ifHighlight, WriteableBitmap writeableBitmap, DoubleColor color)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
