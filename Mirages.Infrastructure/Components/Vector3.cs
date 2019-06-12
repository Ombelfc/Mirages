using System;

namespace Mirages.Infrastructure.Components
{
    /// <summary>
    /// Struct representing a 3D vector.
    /// </summary>
    public struct Vector3
    {
        #region Fields

        /// <summary>
        /// X component of the vector.
        /// </summary>
        public double X { get; private set; }
        /// <summary>
        /// Y component of the vector.
        /// </summary>
        public double Y { get; private set; }
        /// <summary>
        /// Z component of the vector.
        /// </summary>
        public double Z { get; private set; }

        #endregion

        #region Properties

        /// <summary>
        /// 3D vector whose components are equal to 0.
        /// </summary>
        public static Vector3 Zero => new Vector3(0);
        /// <summary>
        /// 3D vector whose components are equal to 1.
        /// </summary>
        public static Vector3 One => new Vector3(1);
        /// <summary>
        /// 3D vector (1, 0, 0)
        /// </summary>
        public static Vector3 UnitX => new Vector3(1, 0, 0);
        /// <summary>
        /// 3D vector (0, 1, 0)
        /// </summary>
        public static Vector3 UnitY => new Vector3(0, 1, 0);
        /// <summary>
        /// 3D vector (0, 0, 1)
        /// </summary>
        public static Vector3 UnitZ => new Vector3(0, 0, 1);

        #endregion

        #region Constructors

        /// <summary>
        /// Creates a 3D vector instance with the same value for either component.
        /// </summary>
        /// <param name="value"></param>
        public Vector3(double value)
        {
            X = Y = Z = value;
        }
        /// <summary>
        /// Creates a 3D vector instance from a 2D vector and a value for the Z component.
        /// </summary>
        /// <param name="vector"></param>
        /// <param name="zValue"></param>
        public Vector3(Vector2 vector, double zValue)
        {
            X = vector.X;
            Y = vector.Y;
            Z = zValue;
        }
        /// <summary>
        /// Creates a 3D vector instance with the specified values.
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="z"></param>
        public Vector3(double x, double y, double z)
        {
            X = x;
            Y = y;
            Z = z;
        }

        #endregion

        #region Methods

        /// <summary>
        /// Returns the length of the vector.
        /// </summary>
        /// <returns></returns>
        public double Length()
        {
            return Math.Sqrt(X * X + Y * Y + Z * Z);
        }
        /// <summary>
        /// Returns the normalized vector.
        /// </summary>
        /// <returns></returns>
        public Vector3 Normalize()
        {
            return this * (1 * Length());
        }
        /// <summary>
        /// Returns the dot product of 2 vectors.
        /// </summary>
        /// <param name="left"></param>
        /// <param name="vector"></param>
        /// <returns></returns>
        public double DotProduct(Vector3 vector)
        {
            return this.X * vector.X + this.Y * vector.Y + this.Z * vector.Z;
        }
        /// <summary>
        /// Returns the magnitude of a vector.
        /// </summary>
        /// <returns></returns>
        public double Magnitude()
        {
            return Math.Sqrt(this.DotProduct(this));
        }
        /// <summary>
        /// Returns the cross product of the current vector with the passed vector.
        /// </summary>
        /// <param name="vector"></param>
        /// <returns></returns>
        public Vector3 CrossProduct(Vector3 vector)
        {
            return new Vector3(
                this.X * vector.Y - this.Y * vector.X,
                this.Y * vector.Z - this.Z * vector.Y,
                this.Z * vector.X - this.X * vector.Z);
        }

        #endregion

        #region Operators

        /// <summary>
        /// Adds 2 vectors together.
        /// </summary>
        /// <param name="left"></param>
        /// <param name="right"></param>
        /// <returns></returns>
        public static Vector3 operator +(Vector3 left, Vector3 right)
        {
            return new Vector3(left.X + right.X, left.Y + right.Y, left.Z + right.Z);
        }
        /// <summary>
        /// Substracting 2 vectors from each-other.
        /// </summary>
        /// <param name="left"></param>
        /// <param name="right"></param>
        /// <returns></returns>
        public static Vector3 operator -(Vector3 left, Vector3 right)
        {
            return new Vector3(left.X - right.X, left.Y - right.Y, left.Z - right.Z);
        }
        /// <summary>
        /// Multiplies the components of a vector by a scalar.
        /// </summary>
        /// <param name="left"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public static Vector3 operator *(Vector3 left, double value)
        {
            return new Vector3(left.X * value, left.Y * value, left.Z * value);
        }

        #endregion

        /*public static Vector3 TransformCoordinate(Vector3 coordinates, Matrix transformationMatrix)
        {
            var x = coordinates.X * transformationMatrix.Mat[0, 0] + coordinates.Y * transformationMatrix.Mat[1, 0] + coordinates.Z * transformationMatrix.Mat[2, 0] + transformationMatrix.Mat[3, 0];
            var y = coordinates.X * transformationMatrix.Mat[0, 1] + coordinates.Y * transformationMatrix.Mat[1, 1] + coordinates.Z * transformationMatrix.Mat[2, 1] + transformationMatrix.Mat[3, 1];
            var z = coordinates.X * transformationMatrix.Mat[0, 2] + coordinates.Y * transformationMatrix.Mat[1, 2] + coordinates.Z * transformationMatrix.Mat[2, 2] + transformationMatrix.Mat[3, 2];
            var w = coordinates.X * transformationMatrix.Mat[0, 3] + coordinates.Y * transformationMatrix.Mat[1, 3] + coordinates.Z * transformationMatrix.Mat[2, 3] + transformationMatrix.Mat[3, 3];

            return new Vector3(x / w, y / w, z / w);
        }*/
    }
}
