using _3DEngine.Helpers;
using System;
using System.Collections.Generic;
using System.Text;

namespace _3DEngine.Utilities
{
    public class Color<T>
    {
        public T R { get; }
        public T G { get; }
        public T B { get; }
        public T A { get; }

        public Color(T r, T g, T b, T a)
        {
            R = r;
            G = g;
            B = b;
            A = a;
        }
    }

    public class Color : Color<float>
    {
        public Color() : base(0, 0, 0, 0) { }

        public Color(float r, float g, float b, float a) : base(r.Saturate(), g.Saturate(), b.Saturate(), a.Saturate()) { }
    }

    public static class Colors
    {
        public static Color Black => new Color(0, 0, 0, 1);
        public static Color Yellow => new Color(1, 1, 0, 1);
        public static Color Red => new Color(1, 0, 0, 1);
        public static Color Blue => new Color(0, 0, 1, 1);
        public static Color Green => new Color(0, 1, 0, 1);
        public static Color DarkGrey => new Color(0.3f, 0.3f, 0.3f, 1);
    }
}
