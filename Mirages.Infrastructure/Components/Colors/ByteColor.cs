namespace Mirages.Infrastructure.Components.Colors
{
    /// <summary>
    /// 32 bits color object.
    /// </summary>
    public class ByteColor : Color<byte>
    {
        #region Constructors

        /// <summary>
        /// Creates a black color instance.
        /// </summary>
        public ByteColor() : base(0) { }

        /// <summary>
        /// Creates a new color with all RGBA values equaling the given value.
        /// </summary>
        /// <param name="value"></param>
        public ByteColor(byte value) : base(value) { }

        /// <summary>
        /// Creates a new color with equal RGB values and the given Alpha value.
        /// </summary>
        /// <param name="value"></param>
        /// <param name="alpha"></param>
        public ByteColor(byte value, byte alpha) : base(value, alpha) { }

        /// <summary>
        /// Creates a color based on the given values.
        /// </summary>
        /// <param name="r"></param>
        /// <param name="g"></param>
        /// <param name="b"></param>
        /// <param name="a"></param>
        public ByteColor(byte r, byte g, byte b, byte a) : base(r, g, b, a) { }

        #endregion
    }
}
