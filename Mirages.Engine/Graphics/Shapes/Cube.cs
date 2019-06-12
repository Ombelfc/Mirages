using Mirages.Engine.Graphics.Components;
using Mirages.Infrastructure.Components;
using System;

namespace Mirages.Engine.Graphics.Shapes
{
    public class Cube : Mesh
    {
        private readonly float length = 1f; 
        private readonly float width = 1f; 
        private readonly float height = 1f;

        public Cube(float length, float width, float height) : base(8, 10)
        {
            this.length = length;
            this.width = width;
            this.height = height;

            //Vertices = GetVertices();
            //Faces = GetFaces();
        }

        private Vector3[] GetVertices()
        {
            Vector3 p0 = new Vector3(-length * .5f, -width * .5f, height * .5f);
            Vector3 p1 = new Vector3(length * .5f, -width * .5f, height * .5f);
            Vector3 p2 = new Vector3(length * .5f, -width * .5f, -height * .5f);
            Vector3 p3 = new Vector3(-length * .5f, -width * .5f, -height * .5f);

            Vector3 p4 = new Vector3(-length * .5f, width * .5f, height * .5f);
            Vector3 p5 = new Vector3(length * .5f, width * .5f, height * .5f);
            Vector3 p6 = new Vector3(length * .5f, width * .5f, -height * .5f);
            Vector3 p7 = new Vector3(-length * .5f, width * .5f, -height * .5f);

            Vector3[] vertices = new Vector3[]
            {
                // Bottom
                p0, p1, p2, p3,

                // Left
                p7, p4, p0, p3,

                // Front
                p4, p5, p1, p0,

                // Back
                p6, p7, p3, p2,

                // Right
                p5, p6, p2, p1,

                // Top
                p7, p6, p5, p4
            };

            return vertices;
        }

        private Face[] GetFaces()
        {
            Face[] faces = new Face[12];

            faces[0] = new Face(3, 1, 0);
            faces[1] = new Face(3, 2, 1);
            faces[2] = new Face(3 + 4 * 1, 1 + 4 * 1, 0 + 4 * 1);
            faces[3] = new Face(3 + 4 * 1, 2 + 4 * 1, 1 + 4 * 1);
            faces[4] = new Face(3 + 4 * 3, 1 + 4 * 3, 0 + 4 * 3);
            faces[5] = new Face(3 + 4 * 3, 2 + 4 * 3, 1 + 4 * 3);
            faces[6] = new Face(3 + 4 * 4, 1 + 4 * 4, 0 + 4 * 4);
            faces[7] = new Face(3 + 4 * 4, 2 + 4 * 4, 1 + 4 * 4);
            faces[8] = new Face(3 + 4 * 5, 1 + 4 * 5, 0 + 4 * 5);
            faces[9] = new Face(3 + 4 * 5, 2 + 4 * 5, 1 + 4 * 5);

            return faces;
        }

        public override double IntersectDistance(Ray ray)
        {
            throw new NotImplementedException();
        }

        public override Vector3 Normalize(Vector3 position)
        {
            throw new NotImplementedException();
        }
    }
}
