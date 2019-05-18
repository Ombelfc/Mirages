using _3DEngine.Utilities;

namespace _3DEngine.Components
{
    public abstract class Mesh
    {
        public string Name { get; set; }
        public Color Color { get; set; } = Colors.Yellow;

        public Vector3[] Vertices { get; set; }
        public Face[] Faces { get; set; }
        
        public Vector3 Position { get; set; } = Vector3.Zero;
        public Quaternion Rotation { get; set; } = Quaternion.RotationYawPitchRoll(0, 0, 0);
        public Vector3 Scaling { get; set; } = Vector3.One;

        public readonly Surface Surface;

        public Mesh(Surface surface)
        {
            Surface = surface;
        }

        public Mesh(int verticesCount, int facesCount)
        {
            Vertices = new Vector3[verticesCount];
            Faces = new Face[facesCount];
        }

        public abstract double IntersectDistance(Ray ray);
        public abstract Vector3 Normalize(Vector3 position);
    }
}
