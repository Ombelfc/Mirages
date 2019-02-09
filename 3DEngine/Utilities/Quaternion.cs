using System;
using System.Collections.Generic;
using System.Text;

namespace _3DEngine.Utilities
{
    public class Quaternion
    {
        private readonly float X;
        private readonly float Y;
        private readonly float Z;
        private readonly float W;

        public Quaternion(float x, float y, float z, float w)
        {
            X = x;
            Y = y;
            Z = z;
            W = w;
        }

        public static Quaternion RotationYawPitchRoll(float yaw, float pitch, float roll)
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
                (float)(cosYaw * sinPitch * cosRoll + sinYaw * cosPitch * sinRoll),
                (float)(sinYaw * cosPitch * cosRoll - cosYaw * sinPitch * sinRoll),
                (float)(cosYaw * cosPitch * sinRoll - sinYaw * sinPitch * cosRoll),
                (float)(cosYaw * cosPitch * cosRoll + sinYaw * sinPitch * sinRoll));
        }
    }
}
