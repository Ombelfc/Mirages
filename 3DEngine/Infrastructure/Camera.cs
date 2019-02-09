using _3DEngine.Utilities;
using System;
using System.Collections.Generic;
using System.Text;

namespace _3DEngine.Infrastructure
{
    public class Camera
    {
        private float fieldOfView = 60;

        public Vector3 Position { get; set; }
        public Quaternion Rotation { get; set; } = Quaternion.RotationYawPitchRoll(0, 0, 0);

        public Vector3 UpDirection { get; set; } = Vector3.UnitY;
        public Vector3 LookDirection { get; set; } = Vector3.UnitZ;

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
            get => (float)(FieldOfView / 180 * Math.PI);
            set
            {
                FieldOfView = (float)(value * 180 / Math.PI);
            }
        }
    }
}
