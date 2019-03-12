using _3DEngine.Components;
using _3DEngine.Utilities;
using System;
using System.Collections.Generic;
using System.Text;

namespace _3DEngine.Shapes
{
    public class Sphere : Mesh
    {
        private readonly float radius = 1f;
        private readonly int nblong = 24;
        private readonly int nblat = 16;

        public Sphere(float radius, int longtitudeNumber, int latitudeNumber) : base(25 * 16 + 2, 2412)
        {
            this.radius = radius;
            this.nblong = longtitudeNumber;
            this.nblat = latitudeNumber;

            Vertices = GetVertices();
            Faces = GetFaces();
        }

        private Vector3[] GetVertices()
        {
            Vector3[] vertices = new Vector3[(nblong + 1) * nblat + 2];
            float _pi = (float) Math.PI;
            float _2pi = _pi * 2f;

            vertices[0] = Vector3.UnitX * radius;

            for (int lat = 0; lat < nblat; lat++)
            {
                float a1 = _pi * (float) (lat + 1) / (nblat + 1);
                float sin1 = (float) Math.Sin(a1);
                float cos1 = (float) Math.Cos(a1);

                for (int lon = 0; lon <= nblong; lon++)
                {
                    float a2 = _2pi * (float) (lon == nblong ? 0 : lon) / nblong;
                    float sin2 = (float) Math.Sin(a2);
                    float cos2 = (float) Math.Cos(a2);

                    vertices[lon + lat * (nblong + 1) + 1] = new Vector3(sin1 * cos2, cos1, sin1 * sin2) * radius;
                }
            }

            vertices[vertices.Length - 1] = Vector3.UnitX * -radius;

            return vertices;
        }

        private Face[] GetFaces()
        {
            int nbfaces = Vertices.Length;
            int nbtriangles = nbfaces * 2;
            int nbindexes = nbtriangles * 3;

            Face[] faces = new Face[nbindexes];

            int i = 0;
            for (int lon = 0; lon < nblong; lon++)
            {
                faces[i++] = new Face(lon + 2, lon + 1, 0);
            }

            for (int lat = 0; lat < nblat - 1; lat++)
            {
                for (int lon = 0; lon < nblong; lon++)
                {
                    int current = lon + lat * (nblong + 1) + 1;
                    int next = current + nblong + 1;

                    faces[i++] = new Face(current, current + 1, next + 1);
                    faces[i++] = new Face(current, next + 1, next);
                }
            }

            for (int lon = 0; lon < nblong; lon++)
            {
                faces[i++] = new Face(Vertices.Length - 1, Vertices.Length - (lon + 2) - 1, Vertices.Length - (lon + 1) - 1);
            }

            return faces;
        }
    }
}
