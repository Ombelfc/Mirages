﻿using System;

namespace _3DEngine.Utilities
{
    public struct Quaternion
    {
        public readonly double X;
        public readonly double Y;
        public readonly double Z;
        public readonly double W;

        public Quaternion(double x, double y, double z, double w)
        {
            X = x;
            Y = y;
            Z = z;
            W = w;
        }

        // The angle of yaw (in radians)
        public double Yaw => Math.Atan2(2.0 * (Y*Z + W*X), W*W - X*X - Y*Y + Z*Z);

        // The angle of pitch (in radians)
        public double Pitch => Math.Asin(-2.0 * (X*Z - W*Y));

        // The angle of roll (in radians)
        public double Roll => Math.Atan2(2.0 * (X*Y + W*Z), W*W + X*X - Y*Y - Z*Z);

        public static Quaternion RotationYawPitchRoll(double yaw, double pitch, double roll)
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
        }

        public static Quaternion operator * (Quaternion left, Quaternion right)
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
    }
}
