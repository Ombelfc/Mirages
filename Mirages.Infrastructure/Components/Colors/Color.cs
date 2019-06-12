namespace Mirages.Infrastructure.Components.Colors
{
    /// <summary>
    /// Generic color object.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class Color<T>
    {
        #region Properties

        /// <summary>
        /// Red value of the color.
        /// </summary>
        public T R { get; private set; }
        /// <summary>
        /// Green value of the color.
        /// </summary>
        public T G { get; private set; }
        /// <summary>
        /// Blue value of the color.
        /// </summary>
        public T B { get; private set; }
        /// <summary>
        /// Alpha value of the color.
        /// </summary>
        public T A { get; private set; }

        #endregion

        #region Constructors

        /// <summary>
        /// Creates a new color with all RGBA values equaling the given value.
        /// </summary>
        /// <param name="value"></param>
        public Color(T value)
        {
            R = G = B = A = value;
        }
        /// <summary>
        /// Creates a new color with equal RGB values and the given alpha value.
        /// </summary>
        /// <param name="value"></param>
        /// <param name="alpha"></param>
        public Color(T value, T alpha)
        {
            R = G = B = value;
            A = alpha;
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
            R = r;
            G = g;
            B = b;
            A = a;
        }

        #endregion
    }
}
