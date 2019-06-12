using Mirages.Infrastructure.Components;
using Mirages.Infrastructure.Components.Colors;
using System;

namespace Mirages.Engine.Graphics
{
    public class RayTracer
    {
        private readonly int screenWidth;
        private readonly int screenHeight;
        private const int MaxDepth = 5;

        public Action<int, int, ByteColor> setPixel;

        public RayTracer(int screenWidth, int screenHeight, Action<int, int, ByteColor> setPixel)
        {
            this.screenWidth = screenWidth;
            this.screenHeight = screenHeight;
            this.setPixel = setPixel;
        }

        /*public Scene DefaultScene(double angle)
        {
            return new Scene
            {
                Camera = Camera.Create(new Vector3(Math.Sin(angle / 180 * Math.PI) * 6, 2, Math.Cos(angle / 180 * Math.PI) * 6), new Vector3(0, 0, 0)),
                Meshes = new List<Mesh>
                {
                    new Plane(new Vector3(0, 1, 0), 0, Surfaces.CheckerBoard),
                    new Sphere(new Vector3(0, 1, 0), 1, Surfaces.Shiny),
                    new Sphere(new Vector3(-1, 0.5, 1.5), 0.5, Surfaces.Shiny)
                },
                Lights = new List<Light>
                {
                    new Light(new Vector3(-2, 2.5, 0), new Color(0.49, 0.07, 0.07, 0)),
                    new Light(new Vector3(1.5, 2.5, 1.5), new Color(0.07, 0.07, 0.49, 0)),
                    new Light(new Vector3(1.5, 2.5, -1.5), new Color(0.07, 0.49, 0.071, 0)),
                    new Light(new Vector3(0, 3.5, 0), new Color(0.21, 0.21, 0.35, 0))
                }
            };
        }

        public void Render(Scene scene)
        {
            void DrawLine(int y)
            {
                for (int x = 0; x < screenWidth; x++)
                {
                    var ray = new Ray(scene.Camera.Position, GetPoint(x, y, scene.Camera));
                    Color color = TraceRay(ray, scene, 0);
                    setPixel(x, y, color.ToColor32());
                }
            }

            for (int y = 0; y < screenHeight; y++) DrawLine(y);
        }

        private Color TraceRay(Ray ray, Scene scene, int depth)
        {
            Intersection nearestIntersection = default(Intersection);
            double nearestDistination = double.PositiveInfinity;

            foreach (var mesh in scene.Meshes)
            {
                var currentDistance = mesh.IntersectDistance(ray);
                if (currentDistance < nearestDistination)
                {
                    nearestDistination = currentDistance;
                    nearestIntersection = new Intersection(mesh, ray, currentDistance);
                }
            }

            if (nearestDistination == double.PositiveInfinity)
                return Colors.Background;

            return Shade(nearestIntersection, scene, depth);
        }

        private Color Shade(Intersection nearestIntersection, Scene scene, int depth)
        {
            var d = nearestIntersection.Ray.Direction;
            var position = d * nearestIntersection.Distination + nearestIntersection.Ray.Start;
            var normal = nearestIntersection.Mesh.Normalize(position);
            var reflectDirection = d - normal * (Vector3.DotProduct(normal, d) * 2);

            Color ret = Colors.DefaultColor;
            ret += GetNaturalColor(nearestIntersection.Mesh, position, normal, reflectDirection, scene);

            if (depth >= MaxDepth)
                return ret + new Color(0.5, 0.5, 0.5, 0);

            return ret + GetReflectionColor(nearestIntersection.Mesh, position + (reflectDirection * 0.001), normal, reflectDirection, scene, depth);
        }

        #region Helpers

        private Vector3 GetPoint(int x, int y, Camera camera)
        {
            return Vector3.Normalize(camera.ForwardDirection + camera.RightDirection * RecenterX(x) + camera.UpDirection * RecenterY(y));
        }

        private double RecenterX(int x)
        {
            return (x - (screenWidth / 2.0)) / (2.0 * screenWidth);
        }

        private double RecenterY(int y)
        {
            return -(y - (screenHeight / 2.0)) / (2.0 * screenHeight);
        }

        private Color GetNaturalColor(Mesh mesh, Vector3 position, Vector3 normal, Vector3 rd, Scene scene)
        {
            Color ret = new Color(0, 0, 0, 0);

            foreach (Light light in scene.Lights)
            {
                var lightDistance = light.Position - position;
                var lightVector = Vector3.Normalize(lightDistance);

                double neatIntersection = TestRay(new Ray(position, lightVector), scene);

                bool isInShadow = !(neatIntersection == double.PositiveInfinity || (neatIntersection > Vector3.Magnitude(lightDistance)));

                if (!isInShadow)
                {
                    double illumination = Vector3.DotProduct(lightVector, normal);
                    if (illumination > 0)
                        ret += mesh.Surface.Diffuse(position) * (illumination * light.Color);

                    double specular = Vector3.DotProduct(lightVector, rd);
                    if (specular > 0)
                        ret += mesh.Surface.Specular(position) * (Math.Pow(specular, mesh.Surface.Roughness) * light.Color);
                }
            }

            return ret;
        }

        private double TestRay(Ray ray, Scene scene)
        {
            double nearestDistance = double.PositiveInfinity;

            foreach (var mesh in scene.Meshes)
            {
                var currentDistance = mesh.IntersectDistance(ray);
                if (currentDistance < nearestDistance)
                    nearestDistance = currentDistance;
            }

            return nearestDistance;
        }

        private Color GetReflectionColor(Mesh mesh, Vector3 position, Vector3 normal, Vector3 reflectDirection, Scene scene, int depth)
        {
            return mesh.Surface.Reflect(position) * TraceRay(new Ray(position, reflectDirection), scene, depth + 1);
        }

        #endregion*/
    }
}
