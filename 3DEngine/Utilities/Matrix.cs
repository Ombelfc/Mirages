using System;
using _3DEngine.Infrastructure;

namespace _3DEngine.Utilities
{
    public class Matrix
    {
        private const int Size = 4;

        public readonly double[,] Mat = new double[Size, Size];

        private static Matrix Identity => new Matrix
        {
            Mat =
            {
                [0, 0] = 1,
                [1, 1] = 1,
                [2, 2] = 1,
                [3, 3] = 1
            }
        };

        public Vector3 GetAxis(Axis axis)
        {
            var ax = (int) axis;
            return new Vector3(Mat[0, ax], Mat[1, ax], Mat[2, ax]);
        }

        public static Matrix LookAtLH(Vector3 position, Vector3 target, Vector3 upVector)
        {
            var zaxis = (target - position).Normalize();
            var xaxis = Vector3.CrossProduct(upVector, zaxis);
            var yaxis = Vector3.CrossProduct(zaxis, xaxis);

            var result = Identity;
            result.Mat[0, 0] = xaxis.X;
            result.Mat[1, 0] = xaxis.Y;
            result.Mat[2, 0] = xaxis.Z;
            result.Mat[3, 0] = -Vector3.DotProduct(xaxis, position);

            result.Mat[0, 1] = yaxis.X;
            result.Mat[1, 1] = yaxis.Y;
            result.Mat[2, 1] = yaxis.Z;
            result.Mat[3, 1] = -Vector3.DotProduct(yaxis, position);

            result.Mat[0, 2] = zaxis.X;
            result.Mat[1, 2] = zaxis.Y;
            result.Mat[2, 2] = zaxis.Z;
            result.Mat[3, 2] = -Vector3.DotProduct(zaxis, position);

            return result;
        }

        internal static Matrix Translation(Vector3 vector)
        {
            var result = Identity;
            result.Mat[3, 0] = vector.X;
            result.Mat[3, 1] = vector.Y;
            result.Mat[3, 2] = vector.Z;

            return result;
        }

        public static Matrix Scaling(Vector3 vector)
        {
            var result = Identity;
            result.Mat[0, 0] = vector.X;
            result.Mat[1, 1] = vector.Y;
            result.Mat[2, 2] = vector.Z;

            return result;
        }

        public static Matrix PrespectiveFovLH(float fieldOfViewRadians, float aspectRatio, float zNear, float zFar)
        {
            var cotTheta = (float) (1f / Math.Tan(fieldOfViewRadians * 0.5f));
            var q = zFar / (zFar - zNear);

            var result = new Matrix();
            result.Mat[0, 0] = cotTheta / aspectRatio;
            result.Mat[1, 1] = cotTheta;
            result.Mat[2, 2] = q;
            result.Mat[2, 3] = 1f;
            result.Mat[3, 3] = -q * zNear;

            return result;
        }

        public Matrix Invert()
        {
            var b0 = Mat[2, 0] * Mat[3, 1] - Mat[2, 1] * Mat[3, 0];
            var b1 = Mat[2, 0] * Mat[3, 2] - Mat[2, 2] * Mat[3, 0];
            var b2 = Mat[2, 3] * Mat[3, 0] - Mat[2, 0] * Mat[3, 3];
            var b3 = Mat[2, 1] * Mat[3, 2] - Mat[2, 2] * Mat[3, 1];
            var b4 = Mat[2, 3] * Mat[3, 1] - Mat[2, 1] * Mat[3, 3];
            var b5 = Mat[2, 2] * Mat[3, 3] - Mat[2, 3] * Mat[3, 2];

            var d11 = Mat[1, 1] * b5 + Mat[1, 2] * b4 + Mat[1, 3] * b3;
            var d12 = Mat[1, 0] * b5 + Mat[1, 2] * b2 + Mat[1, 3] * b1;
            var d13 = Mat[1, 0] * -b4 + Mat[1, 1] * b2 + Mat[1, 3] * b0;
            var d14 = Mat[1, 0] * b3 + Mat[1, 1] * -b1 + Mat[1, 2] * b0;

            var det = Mat[0, 0] * d11 - Mat[0, 1] * d12 + Mat[0, 2] * d13 - Mat[0, 3] * d14;

            if (Math.Abs(det) < float.Epsilon)
                return new Matrix();

            det = 1f / det;

            var a0 = Mat[0, 0] * Mat[1, 1] - Mat[0, 1] * Mat[1, 0];
            var a1 = Mat[0, 0] * Mat[1, 2] - Mat[0, 2] * Mat[1, 0];
            var a2 = Mat[0, 3] * Mat[1, 0] - Mat[0, 0] * Mat[1, 3];
            var a3 = Mat[0, 1] * Mat[1, 2] - Mat[0, 2] * Mat[1, 1];
            var a4 = Mat[0, 3] * Mat[1, 1] - Mat[0, 1] * Mat[1, 3];
            var a5 = Mat[0, 2] * Mat[1, 3] - Mat[0, 3] * Mat[1, 2];

            var d21 = Mat[0, 1] * b5 + Mat[0, 2] * b4 + Mat[0, 3] * b3;
            var d22 = Mat[0, 0] * b5 + Mat[0, 2] * b2 + Mat[0, 3] * b1;
            var d23 = Mat[0, 0] * -b4 + Mat[0, 1] * b2 + Mat[0, 3] * b0;
            var d24 = Mat[0, 0] * b3 + Mat[0, 1] * -b1 + Mat[0, 2] * b0;

            var d31 = Mat[3, 1] * a5 + Mat[3, 2] * a4 + Mat[3, 3] * a3;
            var d32 = Mat[3, 0] * a5 + Mat[3, 2] * a2 + Mat[3, 3] * a1;
            var d33 = Mat[3, 0] * -a4 + Mat[3, 1] * a2 + Mat[3, 3] * a0;
            var d34 = Mat[3, 0] * a3 + Mat[3, 1] * -a1 + Mat[3, 2] * a0;

            var d41 = Mat[2, 1] * a5 + Mat[2, 2] * a4 + Mat[2, 3] * a3;
            var d42 = Mat[2, 0] * a5 + Mat[2, 2] * a2 + Mat[2, 3] * a1;
            var d43 = Mat[2, 0] * -a4 + Mat[2, 1] * a2 + Mat[2, 3] * a0;
            var d44 = Mat[2, 0] * a3 + Mat[2, 1] * -a1 + Mat[2, 2] * a0;

            var result = new Matrix();
            result.Mat[0, 0] = +d11 * det;
            result.Mat[0, 1] = -d21 * det;
            result.Mat[0, 2] = +d31 * det;
            result.Mat[0, 3] = -d41 * det;
            result.Mat[1, 0] = -d12 * det;
            result.Mat[1, 1] = +d22 * det;
            result.Mat[1, 2] = -d32 * det;
            result.Mat[1, 3] = +d42 * det;
            result.Mat[2, 0] = +d13 * det;
            result.Mat[2, 1] = -d23 * det;
            result.Mat[2, 2] = +d33 * det;
            result.Mat[2, 3] = -d43 * det;
            result.Mat[3, 0] = -d14 * det;
            result.Mat[3, 1] = +d24 * det;
            result.Mat[3, 2] = -d34 * det;
            result.Mat[3, 3] = +d44 * det;

            return result;
        }

        public static Matrix RotationQuaternion(Quaternion rotation)
        {
            var xx = rotation.X * rotation.X;
            var yy = rotation.Y * rotation.Y;
            var zz = rotation.Z * rotation.Z;

            var xy = rotation.X * rotation.Y;
            var zw = rotation.Z * rotation.W;
            var zx = rotation.Z * rotation.X;
            var yw = rotation.Y * rotation.W;
            var yz = rotation.Y * rotation.Z;
            var xw = rotation.X * rotation.W;

            var result = Identity;
            result.Mat[0, 0] = 1.0f - 2.0f * (yy + zz);
            result.Mat[0, 1] = 2.0f * (xy + zw);
            result.Mat[0, 2] = 2.0f * (zx - yw);

            result.Mat[1, 0] = 2.0f * (xy - zw);
            result.Mat[1, 1] = 1.0f - 2.0f * (zz + xx);
            result.Mat[1, 2] = 2.0f * (yz + xw);

            result.Mat[2, 0] = 2.0f * (zx + yw);
            result.Mat[2, 1] = 2.0f * (yz - xw);
            result.Mat[2, 2] = 1.0f - 2.0f * (yy + xx);

            return result;
        }

        public static Matrix operator * (Matrix left, Matrix right)
        {
            var result = new Matrix();

            for (int i = 0; i < Size; i++)
            {
                for (int j = 0; j < Size; j++)
                {
                    for (int k = 0; k < Size; k++)
                    {
                        result.Mat[i, j] += left.Mat[i, k] * right.Mat[k, j];
                    }
                }
            }

            return result;
        }
    }
}
