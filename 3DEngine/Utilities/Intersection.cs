using _3DEngine.Components;

namespace _3DEngine.Utilities
{
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
