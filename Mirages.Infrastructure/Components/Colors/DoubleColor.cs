using Mirages.Infrastructure.Extensions;

namespace Mirages.Infrastructure.Components.Colors
{
    /// <summary>
    /// 32 bits double-precision color object.
    /// </summary>
    public class DoubleColor : Color<double>
    {
        #region Constructors
        
        /// <summary>
        /// Creates a black color instance.
        /// </summary>
        public DoubleColor() : base(0) { }

        /// <summary>
        /// Creates a new color with all the RGBA values equaling the given value.
        /// </summary>
        /// <param name="value"></param>
        public DoubleColor(double value) : base(value) { }

        /// <summary>
        /// Creates a new color with equal RGB values and the given Alpha value.
        /// </summary>
        /// <param name="value"></param>
        /// <param name="alpha"></param>
        public DoubleColor(double value, double alpha) : base(value, alpha) { }

        /// <summary>
        /// Creates a color based on the given values.
        /// </summary>
        /// <param name="r"></param>
        /// <param name="g"></param>
        /// <param name="b"></param>
        /// <param name="a"></param>
        public DoubleColor(double r, double g, double b, double a) : base(r.Validate(), g.Validate(), b.Validate(), a.Validate()) { }

        #endregion

        #region Operators



        #endregion
    }
}
