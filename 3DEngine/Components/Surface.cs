using _3DEngine.Utilities;

namespace _3DEngine.Components
{
    public abstract class Surface
    {
        public readonly double Roughness;

        public Surface(double roughness)
        {
            Roughness = roughness;
        }

        public abstract Color Diffuse(Vector3 position);
        public abstract Color Specular(Vector3 position);
        public abstract double Reflect(Vector3 position);
    }
}
