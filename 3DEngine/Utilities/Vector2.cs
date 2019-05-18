using System;
using System.Collections.Generic;
using System.Text;

namespace _3DEngine.Utilities
{
    public class Vector2
    {
        public readonly double X;
        public readonly double Y;

        public Vector2(double x, double y)
        {
            X = x;
            Y = y;
        }

        public static Vector2 operator +(Vector2 left, Vector2 right)
        {
            return new Vector2(left.X + right.X, left.Y + right.Y);
        }

        public static Vector2 operator -(Vector2 left, Vector2 right)
        {
            return new Vector2(left.X - right.X, left.Y - right.Y);
        }

        public static Vector2 operator *(Vector2 left, double value)
        {
            return new Vector2(left.X * value, left.Y * value);
        }
    }
}
