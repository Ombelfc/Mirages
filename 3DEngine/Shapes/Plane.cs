﻿using _3DEngine.Components;
using _3DEngine.Utilities;

namespace _3DEngine.Shapes
{
    public class Plane : Mesh
    {
        private readonly Vector3 Normalized;
        private readonly double Offset;
        private readonly int verticesCount;
        private readonly int facesCount;

        public Plane(int verticesCount, int facesCount) : base(verticesCount, facesCount)
        {
            this.verticesCount = verticesCount;
            this.facesCount = facesCount;
        }

        public Plane(Vector3 normalized, double offset, Surface surface) : base(surface)
        {
            Normalized = normalized;
            Offset = offset;
        }

        public override double IntersectDistance(Ray ray)
        {
            var denominator = Vector3.DotProduct(Normalized, ray.Direction);

            if (denominator > 0)
                return double.PositiveInfinity;

            var distance = (Vector3.DotProduct(Normalized, ray.Start) + Offset) / (-denominator);
            return distance;
        }

        public override Vector3 Normalize(Vector3 position)
        {
            return Normalized;
        }
    }
}
