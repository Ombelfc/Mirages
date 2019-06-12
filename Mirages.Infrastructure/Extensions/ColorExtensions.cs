using Mirages.Infrastructure.Components.Colors;

namespace Mirages.Infrastructure.Extensions
{
    /// <summary>
    /// Class containing extensions methods handling color creation.
    /// </summary>
    public static class ColorExtensions
    {
        /// <summary>
        /// Validates the color value (min : 0, max : 1).
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        public static double Validate(this double x)
        {
            if (x < 0) return 0;
            return x > 1 ? 1 : x;
        }

        /// <summary>
        /// Validates the RGBA values of the given color.
        /// </summary>
        /// <param name="vector"></param>
        /// <returns></returns>
        public static DoubleColor ValidateColor(this DoubleColor vector)
        {
            return new DoubleColor(
                Validate(vector.R), Validate(vector.G), Validate(vector.B), Validate(vector.A));
        }
    }
}
