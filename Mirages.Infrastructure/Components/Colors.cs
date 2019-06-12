using Mirages.Infrastructure.Components.Colors;

namespace Mirages.Infrastructure.Components
{
    public class Color : Color<double>
    {
        public Color() : base(0, 0, 0, 0) { }

        /*public Color(double r, double g, double b, double a) : base(r.Saturate(), g.Saturate(), b.Saturate(), a.Saturate()) { }

        protected static Color32 Map(Color32 left, Color32 right, Func<double, double, double> func)
        {
            return new Color32(
                func.Invoke(left.R, right.R),
                func.Invoke(left.G, right.G),
                func.Invoke(left.B, right.B),
                func.Invoke(left.A, right.A));
        }

        protected static Color32 Mix(Color32 left, Color32 right)
        {
            var func = new Func<double, double, double>((l, r) => l * left.A * (1 - right.A) + r * right.A);

            return new Color32(
                func.Invoke(left.R, right.R),
                func.Invoke(left.G, right.G),
                func.Invoke(left.B, right.B),
                func.Invoke(1, 1));
        }

        protected static Color32 Map(double left, Color32 right, Func<double, double, double> func)
        {
            return new Color32(
                func.Invoke(left, right.R),
                func.Invoke(left, right.G),
                func.Invoke(left, right.B),
                func.Invoke(left, right.A));
        }

        public static Color32 operator * (Color32 left, Color32 right) => Map(left, right, (a, b) => a * b);
        public static Color32 operator + (Color32 left, Color32 right) => Map(left, right, (a, b) => a + b);
        public static Color32 operator - (Color32 left, Color32 right) => Map(left, right, (a, b) => a - b);
        public static Color32 operator * (double left, Color32 right) => Map(left, right, (a, b) => a * b);
        public static Color32 operator + (double left, Color32 right) => Map(left, right, (a, b) => a * b);
        public static Color32 operator - (double left, Color32 right) => Map(left, right, (a, b) => a * b);

        public Color32 ToColor32()
        {
            return new Color32(
                (byte)(byte.MaxValue * R.Saturate()),
                (byte)(byte.MaxValue * G.Saturate()),
                (byte)(byte.MaxValue * B.Saturate()),
                (byte)(byte.MaxValue * A.Saturate()));
        }*/
    }
}
