using Mirages.Core.Algorithms;
using Mirages.Engine.Graphics;
using Mirages.Infrastructure.Components;
using System.Linq;

namespace Mirages.Core.Engine
{
    public class Device
    {
        private readonly BufferedBitmap bufferedBitmap;
        private readonly ILineDrawingAlgorithm lineDrawingAlgorithm;
        private readonly IClippingAlgorithm clippingAlgorithm;

        public Device(BufferedBitmap bufferedBitmap)
        {
            this.bufferedBitmap = bufferedBitmap;

            lineDrawingAlgorithm = new LineDrawingAlgorithm();
            clippingAlgorithm = new LineClippingAlgorithm();

            clippingAlgorithm.SetBoundingRectangle(
                new Vector2(0, 0), new Vector2(bufferedBitmap.PixelWidth, bufferedBitmap.PixelHeight));
        }

        /*public void Render(Scene scene)
        {
            // Order of transformation:
            // 1. Object space: In this space there are models at the beginning, they have no position or rotation.
            // 2. World space: A common space in which the camera and all models are located, after giving them coordiantes and rotation.
            // 3. View space: Coordinate space with respect to the camera, which is located at (0, 0, 0)
            // 4. Projection space: After this transformation, the objects seen by the camera gain perspective.

            // Matrix transformation from 2. to 3.
            var viewMatrix = scene.Camera.LookAtLH();

            // Matrix transformation from 3. to 4.
            var projectionMatrix = Matrix.PrespectiveFovLH(
                                          scene.Camera.FieldOfViewRadians,
                                          bufferedBitmap.AspectRatio,
                                          scene.Camera.ZNear,
                                          scene.Camera.ZFar);

            foreach (var mesh in scene.Meshes)
            {
                // Transformation matrix from 1. to 2.
                // First we apply the rotation and then the transformation
                var worldMatrix = Matrix.Scaling(mesh.Scaling) * Matrix.RotationQuaternion(mesh.Rotation) * Matrix.Translation(mesh.Position);

                // Matrix multiplication combining all transformations in the correct order.
                var transformationMatrix = worldMatrix * viewMatrix * projectionMatrix;

                // 3D coordinantes to 2D coordinantes on a bitmap.
                var pixels = mesh.Vertices.Select(vertex => Project(vertex, transformationMatrix)).ToArray();

                var vertices = mesh.Vertices.Select(vertex => Vector3.TransformCoordinate(vertex, worldMatrix * viewMatrix)).ToArray();
                var color32 = mesh.Color.ToColor32();

                foreach (var face in mesh.Faces)
                {
                    if (vertices[face.A].Z < scene.Camera.ZNear ||
                        vertices[face.B].Z < scene.Camera.ZNear ||
                        vertices[face.C].Z < scene.Camera.ZNear) continue;

                    face.Edges((a, b) =>
                    {
                        var p1 = pixels[a];
                        var p2 = pixels[b];

                        if (clippingAlgorithm.ClipLine(ref p1, ref p2))
                            lineDrawingAlgorithm.DrawLine(p1, p2, (x, y) => bufferedBitmap.DrawPoint(x, y, color32));
                    });
                }
            }
        }

        /// <summary>
        /// Converts 3D coordinates to 2D coordinates.
        /// </summary>
        /// <param name="coordinates"></param>
        /// <param name="transformationMatrix"></param>
        /// <returns></returns>
        public Vector2 Project(Vector3 coordinates, Matrix transformationMatrix)
        {
            var point = Vector3.TransformCoordinate(coordinates, transformationMatrix);

            var x = bufferedBitmap.PixelWidth * (point.X + 0.5f);
            var y = bufferedBitmap.PixelHeight * (-point.Y + 0.5f);

            return new Vector2(x, y);
        }*/
    }
}