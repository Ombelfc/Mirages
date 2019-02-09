using System;
using System.Collections.Generic;
using System.Text;

namespace _3DEngine.Utilities
{
    public class Face
    {
        private readonly int A;
        private readonly int B;
        private readonly int C;

        public Face(int a, int b, int c)
        {
            A = a;
            B = b;
            C = c;
        }
    }
}
