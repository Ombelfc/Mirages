namespace Mirages.Infrastructure.Components
{
    /// <summary>
    /// Struct representing a 2D vector.
    /// </summary>
    public struct Vector2
    {
        #region Fields
        
        /// <summary>
        /// X component of the vector.
        /// </summary>
        public double X { get; private set; }
        /// <summary>
        /// Y component of the vector.
        /// </summary>
        public double Y { get; private set; }

        #endregion

        #region Properties

        /// <summary>
        /// 2D vector whose components are equal to 0.
        /// </summary>
        public Vector2 Zero => new Vector2(0);
        /// <summary>
        /// 2D vector whose components are equal to 1.
        /// </summary>
        public Vector2 One => new Vector2(1);
        /// <summary>
        /// 2D vector (1, 0).
        /// </summary>
        public Vector2 UnitX => new Vector2(1, 0);
        /// <summary>
        /// 2D vector (0, 1).
        /// </summary>
        public Vector2 UnitY => new Vector2(0, 1);

        #endregion

        #region Constructors

        /// <summary>
        /// Creates a 2D vector instance with the same value for either component.
        /// </summary>
        /// <param name="value"></param>
        public Vector2(double value)
        {
            X = Y = value;
        }
        /// <summary>
        /// Creates a 2D vector instance with the specified values.
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        public Vector2(double x, double y)
        {
            X = x;
            Y = y;
        }

        #endregion

        #region Operators

        /// <summary>
        /// Adds 2 vectors together.
        /// </summary>
        /// <param name="left"></param>
        /// <param name="right"></param>
        /// <returns></returns>
        public static Vector2 operator +(Vector2 left, Vector2 right)
        {
            return new Vector2(left.X + right.X, left.Y + right.Y);
        }
        /// <summary>
        /// Substracts 2 vectors from each-other.
        /// </summary>
        /// <param name="left"></param>
        /// <param name="right"></param>
        /// <returns></returns>
        public static Vector2 operator -(Vector2 left, Vector2 right)
        {
            return new Vector2(left.X - right.X, left.Y - right.Y);
        }
        /// <summary>
        /// Multiplies the components of a vector by a scalar.
        /// </summary>
        /// <param name="left"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public static Vector2 operator *(Vector2 left, double value)
        {
            return new Vector2(left.X * value, left.Y * value);
        }

        #endregion
    }
}