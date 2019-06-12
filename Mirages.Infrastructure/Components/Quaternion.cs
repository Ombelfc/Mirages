using System;

namespace Mirages.Infrastructure.Components
{
    /// <summary>
    /// Struct representing a 3D rotation vector.
    /// </summary>
    public struct Quaternion
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
        /// <summary>
        /// Rotation component of the vector.
        /// </summary>
        public double W { get; private set; }

        #endregion

        #region Properties

        /// <summary>
        /// Normal axis. Axis drawn from top to bottom of the plane.
        /// </summary>
        public double Yaw => Math.Atan2(2.0 * (Y * Z + W * X), W * W - X * X - Y * Y + Z * Z);
        /// <summary>
        /// Transverse axis. Axis drawn from the pilot's left to right, parallel to the wings.
        /// </summary>
        public double Pitch => Math.Asin(-2.0 * (X * Z - W * Y));
        /// <summary>
        /// Longitudinal axis. Axis drawn through the body of the plane. (Tail to Nose).
        /// </summary>
        public double Roll => Math.Atan2(2.0 * (X * Y + W * Z), W * W + X * X - Y * Y - Z * Z);

        #endregion

        #region Constructors

        /// <summary>
        /// Creates a 3D rotation vector instance with the same value for either component.
        /// </summary>
        /// <param name="value"></param>
        public Quaternion(double value)
        {
            X = Y = Z = W = value;
        }

        /// <summary>
        /// Creates a 3D rotation vector instance from a 3D vector and a value for the rotaion.
        /// </summary>
        /// <param name="vector"></param>
        /// <param name="rotation"></param>
        public Quaternion(Vector3 vector, double rotation)
        {
            X = vector.X;
            Y = vector.Y;
            Z = vector.Z;
            W = rotation;
        }

        /// <summary>
        /// Creates a 3D rotation vector instance with the specified values.
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="z"></param>
        /// <param name="w"></param>
        public Quaternion(double x, double y, double z, double w)
        {
            X = x;
            Y = y;
            Z = z;
            W = w;
        }

        #endregion

        #region Operators

        /// <summary>
        /// Multiplies 2 3D rotation vectors with each-other.
        /// </summary>
        /// <param name="left"></param>
        /// <param name="right"></param>
        /// <returns></returns>
        public static Quaternion operator *(Quaternion left, Quaternion right)
        {
            var lx = left.X;
            var ly = left.Y;
            var lz = left.Z;
            var lw = left.W;

            var rx = right.X;
            var ry = right.Y;
            var rz = right.Z;
            var rw = right.W;

            var a = ly * rz - lz * ry;
            var b = lz * rx - lx * rz;
            var c = lx * ry - ly * rx;
            var d = lx * rx - ly * ry + lz * rz;

            return new Quaternion(
                lx * rw + rx * lw + a,
                ly * rw + ry * lw + b,
                lz * rw + rz * lw + c,
                lw * rw - d);
        }

        #endregion

        #region Methods

        /// <summary>
        /// Returns the dot product of 2 rotation vectors.
        /// </summary>
        /// <param name="rotationVector"></param>
        /// <returns></returns>
        public double DotProduct(Quaternion rotationVector)
        {
            return this.X * rotationVector.X + this.Y * rotationVector.Y + this.Z * rotationVector.Z + this.W * rotationVector.W;
        }

        #endregion

        /*public static Quaternion RotationYawPitchRoll(double yaw, double pitch, double roll)
        {
            var halfRoll = roll * 0.5;
            var halfPitch = pitch * 0.5;
            var halfYaw = yaw * 0.5;

            var sinRoll = Math.Sin(halfRoll);
            var cosRoll = Math.Cos(halfRoll);
            var sinPitch = Math.Sin(halfPitch);
            var cosPitch = Math.Cos(halfPitch);
            var sinYaw = Math.Sin(halfYaw);
            var cosYaw = Math.Cos(halfYaw);

            return new Quaternion(
                (cosYaw * sinPitch * cosRoll + sinYaw * cosPitch * sinRoll),
                (sinYaw * cosPitch * cosRoll - cosYaw * sinPitch * sinRoll),
                (cosYaw * cosPitch * sinRoll - sinYaw * sinPitch * cosRoll),
                (cosYaw * cosPitch * cosRoll + sinYaw * sinPitch * sinRoll));
        }

        public static Quaternion RotationAxis(Vector3 axis, double angle)
        {
            var normalized = axis.Normalize();

            var halfAngle = angle * 0.5f;
            var sin = Math.Sin(halfAngle);

            return new Quaternion(
                normalized.X * sin, normalized.Y * sin, normalized.Z * sin, Math.Cos(halfAngle));
        }*/
    }
}
