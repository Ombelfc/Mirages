using Mirages.Infrastructure.Components.Colors;
using System.Windows.Media;

namespace Mirages.Utility.Extensions
{
    /// <summary>
    /// Static class containing methods used to operate on colors.
    /// </summary>
    public static class ColorExtensions
    {
        /// <summary>
        /// Returns a System.Windows.Media.Color from the given byte color.
        /// </summary>
        /// <param name="color"></param>
        /// <returns></returns>
        public static Color FromByteColor(ByteColor color)
        {
            return Color.FromArgb(color.A, color.R, color.G, color.B);
        }
    }
}
