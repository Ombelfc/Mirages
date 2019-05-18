using System;

namespace _3DEngine.Utilities
{
    public struct Vector3
    {
        public readonly double X;
        public readonly double Y;
        public readonly double Z;

        public static Vector3 Zero => new Vector3(0, 0, 0);
        public static Vector3 One => new Vector3(1, 1, 1);

        public static Vector3 UnitX => new Vector3(1, 0, 0);
        public static Vector3 UnitY => new Vector3(0, 1, 0);
        public static Vector3 UnitZ => new Vector3(0, 0, 1);

        public static double Magnitude(Vector3 vector) => Math.Sqrt(DotProduct(vector, vector));

        public Vector3(double x, double y, double z)
        {
            X = x;
            Y = y;
            Z = z;
        }

        public Vector3 Normalize()
        {
            return this * (1 / Length());
        }

        public static Vector3 Normalize(Vector3 vector)
        {
            double magnitude = Magnitude(vector);

            return magnitude > 0 ? vector * (1 / magnitude) : default(Vector3);
        }

        /// <summary>
        /// Length of the vector.
        /// </summary>
        /// <returns></returns>
        private double Length()
        {
            return Math.Sqrt(X * X + Y * Y + Z * Z);
        }

        public static Vector3 CrossProduct(Vector3 left, Vector3 right)
        {
            return new Vector3(
                left.Y * right.Z - left.Z * right.Y,
                left.Z * right.X - left.X * right.Z,
                left.X * right.Y - left.Y * right.X);
        }

        public static double DotProduct(Vector3 left, Vector3 right)
        {
            return left.X * right.X + left.Y * right.Y + left.Z * right.Z;
        }

        public static Vector3 operator +(Vector3 left, Vector3 right)
        {
            return new Vector3(left.X + right.X, left.Y + right.Y, left.Z + right.Z);
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

        public static Vector3 operator *(Vector3 left, double value)
        {
            return new Vector3(left.X * value, left.Y * value, left.Z * value);
        }

        public static Vector3 TransformCoordinate(Vector3 coordinates, Matrix transformationMatrix)
        {
            var x = coordinates.X * transformationMatrix.Mat[0, 0] + coordinates.Y * transformationMatrix.Mat[1, 0] + coordinates.Z * transformationMatrix.Mat[2, 0] + transformationMatrix.Mat[3, 0];
            var y = coordinates.X * transformationMatrix.Mat[0, 1] + coordinates.Y * transformationMatrix.Mat[1, 1] + coordinates.Z * transformationMatrix.Mat[2, 1] + transformationMatrix.Mat[3, 1];
            var z = coordinates.X * transformationMatrix.Mat[0, 2] + coordinates.Y * transformationMatrix.Mat[1, 2] + coordinates.Z * transformationMatrix.Mat[2, 2] + transformationMatrix.Mat[3, 2];
            var w = coordinates.X * transformationMatrix.Mat[0, 3] + coordinates.Y * transformationMatrix.Mat[1, 3] + coordinates.Z * transformationMatrix.Mat[2, 3] + transformationMatrix.Mat[3, 3];

            return new Vector3(x / w, y / w, z / w);
        }
    }
}
