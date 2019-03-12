using _3DEngine.Components;
using _3DEngine.Utilities;
using System;
using System.Collections.Generic;
using System.Text;

namespace _3DEngine.Shapes
{
    public class Cylinder : Mesh
    {
        private readonly float height = 1f;
        private readonly int nbsides = 24;

        // Outer shell is at radius1 + radius2/2, inner shell is at radius 1 - radius 2/2;
        private readonly float bottomRadius1 = .5f;
        private readonly float bottomRadius2 = .15f;
        private readonly float topRadius1 = .5f;
        private readonly float topRadius2 = .15f;

        private readonly int nbVerticesCap = 2;
        private readonly int nbVerticesSides = 2;

        public Cylinder(float height, float bottomRadius1, float bottomRadius2, float topRadius1, float topRadius2, int numberSides) : base(200, 576)
        {
            this.height = height;
            this.bottomRadius1 = bottomRadius1;
            this.bottomRadius2 = bottomRadius2;
            this.topRadius1 = topRadius1;
            this.topRadius2 = topRadius2;
            this.nbsides = numberSides;

            this.nbVerticesCap += this.nbsides * 2;
            this.nbVerticesSides += this.nbsides * 2;

            Vertices = GetVertices();
            Faces = GetFaces();
        }

        private Vector3[] GetVertices()
        {
            Vector3[] vertices = new Vector3[nbVerticesCap * 2 + nbVerticesSides * 2];

            int vert = 0;
            float _2pi = (float) Math.PI * 2f;

            // Bottom cap
            int sideCounter = 0;
            while (vert < nbVerticesCap)
            {
                sideCounter = sideCounter == nbsides ? 0 : sideCounter;

                float r1 = (float) (sideCounter++) / nbsides * _2pi;
                float cos = (float) Math.Cos(r1);
                float sin = (float) Math.Sin(r1);
                vertices[vert] = new Vector3(cos * (bottomRadius1 - bottomRadius2 * .5f), 0f, sin * (bottomRadius1 - bottomRadius2 * .5f));
                vertices[vert + 1] = new Vector3(cos * (bottomRadius1 + bottomRadius2 * .5f), 0f, sin * (bottomRadius1 + bottomRadius2 * .5f));
                vert += 2;
            }

            // Top cap
            sideCounter = 0;
            while (vert < nbVerticesCap * 2)
            {
                sideCounter = sideCounter == nbsides ? 0 : sideCounter;

                float r1 = (float) (sideCounter++) / nbsides * _2pi;
                float cos = (float) Math.Cos(r1);
                float sin = (float) Math.Sin(r1);
                vertices[vert] = new Vector3(cos * (topRadius1 - topRadius2 * .5f), height, sin * (topRadius1 - topRadius2 * .5f));
                vertices[vert + 1] = new Vector3(cos * (topRadius1 + topRadius2 * .5f), height, sin * (topRadius1 + topRadius2 * .5f));
                vert += 2;
            }

            // Sides (out)
            sideCounter = 0;
            while (vert < nbVerticesCap * 2 + nbVerticesSides)
            {
                sideCounter = sideCounter == nbsides ? 0 : sideCounter;

                float r1 = (float) (sideCounter++) / nbsides * _2pi;
                float cos = (float) Math.Cos(r1);
                float sin = (float) Math.Sin(r1);

                vertices[vert] = new Vector3(cos * (topRadius1 + topRadius2 * .5f), height, sin * (topRadius1 + topRadius2 * .5f));
                vertices[vert + 1] = new Vector3(cos * (bottomRadius1 + bottomRadius2 * .5f), 0, sin * (bottomRadius1 + bottomRadius2 * .5f));
                vert += 2;
            }

            // Sides (in)
            sideCounter = 0;
            while (vert < vertices.Length)
            {
                sideCounter = sideCounter == nbsides ? 0 : sideCounter;

                float r1 = (float)(sideCounter++) / nbsides * _2pi;
                float cos = (float)Math.Cos(r1);
                float sin = (float)Math.Sin(r1);

                vertices[vert] = new Vector3(cos * (topRadius1 - topRadius2 * .5f), height, sin * (topRadius1 - topRadius2 * .5f));
                vertices[vert + 1] = new Vector3(cos * (bottomRadius1 - bottomRadius2 * .5f), 0, sin * (bottomRadius1 - bottomRadius2 * .5f));
                vert += 2;
            }

            return vertices;
        }

        private Face[] GetFaces()
        {
            int nbFace = nbsides * 4;
            int nbTriangles = nbFace * 2;
            int nbIndexes = nbTriangles * 3;
            Face[] faces = new Face[nbIndexes];

            // Bottom cap
            int i = 0;
            int sideCounter = 0;
            while (sideCounter < nbsides)
            {
                int current = sideCounter * 2;
                int next = sideCounter * 2 + 2;

                faces[i++] = new Face(next + 1, next, current);
                faces[i++] = new Face(current + 1, next + 1, current);

                sideCounter++;
            }

            // Top cap
            while (sideCounter < nbsides * 2)
            {
                int current = sideCounter * 2 + 2;
                int next = sideCounter * 2 + 4;

                faces[i++] = new Face(current, next, next + 1);
                faces[i++] = new Face(current, next + 1, current + 1);

                sideCounter++;
            }

            // Sides (out)
            while (sideCounter < nbsides * 3)
            {
                int current = sideCounter * 2 + 4;
                int next = sideCounter * 2 + 6;

                faces[i] = new Face(current, next, next + 1);
                faces[i] = new Face(current, next + 1, current + 1);

                sideCounter++;
            }


            // Sides (in)
            while (sideCounter < nbsides * 4)
            {
                int current = sideCounter * 2 + 6;
                int next = sideCounter * 2 + 8;

                faces[i++] = new Face(next + 1, next, current);
                faces[i++] = new Face(current + 1, next + 1, current);

                sideCounter++;
            }

            return faces;
        }
    }
}
