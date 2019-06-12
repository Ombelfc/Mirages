namespace Mirages.Common.Extensions
{
    public static class ColorSaturationExtensions
    {
        /// <summary>
        /// Saturates the given value (min : 0, max : 1).
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        public static double Saturate(this double x)
        {
            if (x < 0) return 0;
            return x > 1 ? 1 : x;
        }

        /// <summary>
        /// Saturates the RGBA values of the given color.
        /// </summary>
        /// <param name="vector"></param>
        /// <returns></returns>
        public static Color SaturateColor(this Color vector)
        {
            return new Color(
                Saturate(vector.R), Saturate(vector.G), Saturate(vector.B), Saturate(vector.A));
        }
    }
}
