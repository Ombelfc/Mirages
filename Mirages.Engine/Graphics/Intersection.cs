using Mirages.Engine.Graphics.Components;

namespace Mirages.Engine.Graphics
{
    // TODO: Add documentation once the utility of this class is understood
    public struct Intersection
    {
        public readonly Mesh Mesh;
        public readonly Ray Ray;
        public readonly double Distination;

        public Intersection(Mesh mesh, Ray ray, double distination)
        {
            Mesh = mesh;
            Ray = ray;
            Distination = distination;
        }
    }
}
