namespace Mirages.Infrastructure.Components.Colors
{
    /// <summary>
    /// Generic color object.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class Color<T>
    {
        #region Fields

        /// <summary>
        /// Holds the RGBA values of the color.
        /// </summary>
        private T[] RGBA { get; }

        #endregion

        #region Constructors

        /// <summary>
        /// Creates a new color with all RGBA values equaling the given value.
        /// </summary>
        /// <param name="value"></param>
        public Color(T value)
        {
            RGBA = new[] { value, value, value, value };
        }

        /// <summary>
        /// Creates a new color with equal RGB values and the given alpha value.
        /// </summary>
        /// <param name="value"></param>
        /// <param name="alpha"></param>
        public Color(T value, T alpha)
        {
            RGBA = new[] { value, value, value, alpha };
        }

        /// <summary>
        /// Creates a new color with the given RGBA values.
        /// </summary>
        /// <param name="r"></param>
        /// <param name="g"></param>
        /// <param name="b"></param>
        /// <param name="a"></param>
        public Color(T r, T g, T b, T a)
        {
            RGBA = new[] { r, g, b, a };
        }

        #endregion

        #region Properties

        /// <summary>
        /// Red value of the color.
        /// </summary>
        public T R => RGBA[0];
        /// <summary>
        /// Green value of the color.
        /// </summary>
        public T G => RGBA[1];
        /// <summary>
        /// Blue value of the color.
        /// </summary>
        public T B => RGBA[2];
        /// <summary>
        /// Alpha value of the color.
        /// </summary>
        public T A => RGBA[3];

        #endregion
    }
}
