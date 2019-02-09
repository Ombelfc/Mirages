using System;
using System.Collections.Generic;
using System.Text;

namespace _3DEngine.Utilities
{
    public class Vector3
    {
        private readonly float X;
        private readonly float Y;
        private readonly float Z;

        public static Vector3 Zero => new Vector3(0, 0, 0);
        public static Vector3 One => new Vector3(1, 1, 1);

        public static Vector3 UnitX => new Vector3(1, 0, 0);
        public static Vector3 UnitY => new Vector3(0, 1, 0);
        public static Vector3 UnitZ => new Vector3(0, 0, 1);

        public Vector3(float x, float y, float z)
        {
            X = x;
            Y = y;
            Z = z;
        }

        public Vector3 Normalize()
        {
            return this * (1 / Length());
        }

        /// <summary>
        /// Length of the vector.
        /// </summary>
        /// <returns></returns>
        private float Length()
        {
            return (float)Math.Sqrt(X * X + Y * Y + Z * Z);
        }

        /// <summary>
        /// Substracting 2 vectors.
        /// </summary>
        /// <param name="left"></param>
        /// <param name="right"></param>
        /// <returns></returns>
        public static Vector3 operator -(Vector3 left, Vector3 right)
        {
            return new Vector3(left.X - right.X, left.Y - right.Y, left.Z - right.Z);
        }

        public static Vector3 operator *(Vector3 left, float value)
        {
            return new Vector3(left.X * value, left.Y * value, left.Z * value);
        }
    }
}
