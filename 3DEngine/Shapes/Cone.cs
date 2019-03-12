using _3DEngine.Components;
using _3DEngine.Utilities;
using System;

namespace _3DEngine.Shapes
{
    public class Cone : Mesh
    {
        private readonly float height = 1f;
        private readonly float bottomRadius = .25f;
        private readonly float topRadius = .05f;

        private readonly int nbsides = 18;
        private readonly int nbheightseg = 1;
        private readonly int nbverticescap = 1;

        public Cone(float height, float bottomRadius, float topRadius, int numberSides, int numberHeightSeg) : base(76, 219)
        {
            this.height = height;
            this.bottomRadius = bottomRadius;
            this.topRadius = topRadius;
            this.nbsides = numberSides;
            this.nbheightseg = numberHeightSeg;
            this.nbverticescap += this.nbsides;

            Vertices = GetVertices();
            Faces = GetFaces();
        }

        private Vector3[] GetVertices()
        {
            Vector3[] vertices = new Vector3[nbverticescap + nbverticescap + nbsides * nbheightseg * 2 + 2];
            int vert = 0;
            float _2pi = (float) Math.PI * 2f;

            // Bottom cap
            vertices[vert++] = new Vector3(0f, 0f, 0f);
            while (vert <= nbsides)
            {
                float rad = (float) vert / nbsides * _2pi;
                vertices[vert] = new Vector3((float) Math.Cos(rad) * bottomRadius, 0f, (float) Math.Sin(rad) * bottomRadius);
                vert++;
            }

            // Top cap
            vertices[vert++] = new Vector3(0f, height, 0f);
            while (vert < nbsides * 2 + 1)
            {
                float rad = (float) (vert - nbsides - 1) / nbsides * _2pi;
                vertices[vert] = new Vector3((float) Math.Cos(rad) * topRadius, height, (float) Math.Sin(rad) * topRadius);
                vert++;
            }

            // Sides
            int v = 0;
            while (vert <= vertices.Length - 4)
            {
                float rad = (float) v / nbsides * _2pi;
                vertices[vert] = new Vector3((float) Math.Cos(rad) * topRadius, height, (float) Math.Sin(rad) * topRadius);
                vertices[vert + 1] = new Vector3((float) Math.Cos(rad) * bottomRadius, 0, (float) Math.Sin(rad) * bottomRadius);
                vert += 2;
                v++;
            }

            vertices[vert] = vertices[nbsides * 2 + 2];
            vertices[vert + 1] = vertices[nbsides * 2 + 3];

            return vertices;
        }

        private Face[] GetFaces()
        {
            int nbtriangles = nbsides + nbsides + nbsides * 2;
            Face[] faces = new Face[nbtriangles * 3 + 3];

            // Bottom cap
            int tri = 0;
            int i = 0;
            while (tri < nbsides - 1)
            {
                faces[i] = new Face(0, tri + 1, tri + 2);
                tri++;
                i += 3;
            }

            faces[i] = new Face(0, tri + 1, 1);
            tri++;
            i += 3;

            // Top cap
            while (tri < nbsides * 2)
            {
                faces[i] = new Face(tri + 2, tri + 1, nbverticescap);
                tri++;
                i += 3;
            }

            faces[i] = new Face(nbverticescap + 1, tri + 1, nbverticescap);
            tri++;
            i += 3;
            tri++;

            // Sides
            while (tri <= nbtriangles)
            {
                faces[i] = new Face(tri + 2, tri + 1, tri);
                tri++;
                i += 3;

                faces[i] = new Face(tri + 1, tri + 2, tri);
                tri++;
                i += 3;
            }

            return faces;
        }
    }
}
