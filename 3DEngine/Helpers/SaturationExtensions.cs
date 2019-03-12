using _3DEngine.Utilities;

namespace _3DEngine.Helpers
{
    public static class SaturationExtensions
    {
        public static float Saturate(this float x)
        {
            if (x < 0) return 0;
            return x > 1 ? 1 : x; 
        }

        public static double Saturate(this double x)
        {
            if (x < 0) return 0;
            return x > 1 ? 1 : x;
        }

        public static Color SaturateColor(this Color vector)
        {
            return new Color(
                Saturate(vector.R), Saturate(vector.G), Saturate(vector.B), Saturate(vector.A));
        }
    }
}
