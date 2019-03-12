using System;
using System.Collections.Generic;
using System.Text;

namespace _3DEngine.Utilities
{
    public struct Face
    {
        public readonly int A;
        public readonly int B;
        public readonly int C;

        public Face(int a, int b, int c)
        {
            A = a;
            B = b;
            C = c;
        }

        public void Edges(Action<int, int> action)
        {
            action.Invoke(A, B);
            action.Invoke(B, C);
            action.Invoke(A, C);
        }
    }
}
