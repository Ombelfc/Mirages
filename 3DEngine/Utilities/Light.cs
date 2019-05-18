namespace _3DEngine.Utilities
{
    public class Light
    {
        public readonly Vector3 Position;
        public readonly Color Color;

        public Light(Vector3 position, Color color)
        {
            Position = position;
            Color = color;
        }
    }
}
