﻿using _3DEngine.Utilities;
using System;

namespace _3DEngine.Infrastructure
{
    public enum Axis
    {
        X = 0,
        Y = 1,
        Z = 2
    }

    public enum RelativeSpace
    {
        World,
        Camera
    }

    public class Camera
    {
        private float fieldOfView = 60;

        public Vector3 Position { get; set; }
        public Quaternion Rotation { get; set; } = Quaternion.RotationYawPitchRoll(0, 0, 0);

        public Vector3 UpDirection { get; set; } = Vector3.UnitY;
        public Vector3 LookDirection { get; set; } = Vector3.UnitZ;

        public float ZNear { get; set; } = 0.01f;
        public float ZFar { get; set; } = 100f;

        public float FieldOfView
        {
            get => fieldOfView;
            set
            {
                if (value > 0 && value < 180)
                    fieldOfView = value;
            }
        }

        public float FieldOfViewRadians
        {
            get => (float) (FieldOfView / 180 * Math.PI);
            set
            {
                FieldOfView = (float) (value * 180 / Math.PI);
            }
        }

        /// <summary>
        /// Moves the camera on any axis relative to the current position.
        /// </summary>
        /// <param name="move"></param>
        public void Move(Vector3 move)
        {
            var viewMatrix = LookAtLH();
            var relativePosition = Vector3.TransformCoordinate(Position, viewMatrix);

            Position = Vector3.TransformCoordinate(relativePosition + move, viewMatrix.Invert());
        }

        /// <summary>
        /// Rotates the camera relative to the current position.
        /// </summary>
        /// <param name="axis"></param>
        /// <param name="angle"></param>
        public void Rotate(Axis axis, float angle)
        {
            var radians = (float) (Math.PI * angle / 180);
            Vector3 rotationAxis = LookAtLH().GetAxis(axis);

            Rotation = Quaternion.RotationAxis(rotationAxis, radians) * Rotation;
        }

        /// <summary>
        /// Creates a transformation matrix from the 2D world space to the 3D object space.
        /// </summary>
        /// <returns></returns>
        public Matrix LookAtLH()
        {
            var cameraRotationMatrix = Matrix.RotationQuaternion(Rotation);
            var cameraLookDirection = Vector3.TransformCoordinate(LookDirection, cameraRotationMatrix);
            var cameraUpDirection = Vector3.TransformCoordinate(UpDirection, cameraRotationMatrix);

            return Matrix.LookAtLH(Position, Position + cameraLookDirection, cameraUpDirection);
        }
    }
}
