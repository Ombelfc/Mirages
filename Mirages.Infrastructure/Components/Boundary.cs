namespace Mirages.Core.Clipping.Utilities
{
    /// <summary>
    /// Object representing the boundary of a shape in the clipping scene.
    /// </summary>
    public class Boundary
    {
        #region Properties

        /// <summary>
        /// Minimum x of the boundary.
        /// </summary>
        public double XMin { get; set; } = int.MaxValue;
        /// <summary>
        /// Maximum x of the boundary.
        /// </summary>
        public double XMax { get; set; } = int.MinValue;
        /// <summary>
        /// Minimum y of the boundary.
        /// </summary>
        public double YMin { get; set; } = int.MaxValue;
        /// <summary>
        /// Maximum y of the boundary.
        /// </summary>
        public double YMax { get; set; } = int.MinValue;

        #endregion

        #region Constructors

        /// <summary>
        /// Creates an empty boundary instance.
        /// </summary>
        public Boundary() { }
        /// <summary>
        /// Creates a boundary instance with equal X values and Y values.
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        public Boundary(double x, double y)
        {
            XMin = XMax = x;
            YMin = YMax = y;
        }
        /// <summary>
        /// Creates a boundary instance with the given values.
        /// </summary>
        /// <param name="xMin"></param>
        /// <param name="xMax"></param>
        /// <param name="yMin"></param>
        /// <param name="yMax"></param>
        public Boundary(double xMin, double xMax, double yMin, double yMax)
        {
            XMin = xMin;
            XMax = xMax;
            YMin = yMin;
            YMax = yMax;
        }

        #endregion

        /*public void UpdateBoundary(double x, double y)
        {
            if (x <= XMin) XMin = x;
            if (x >= XMax) XMax = x;
            if (y <= YMin) YMin = y;
            if (y >= YMax) YMax = y;
        }

        public void Reset()
        {
            XMin = int.MaxValue;
            XMax = int.MinValue;
            YMin = int.MaxValue;
            YMax = int.MinValue;
        }

        public bool Contains(double x, double y)
        {
            if ((x > XMax) || (x < XMin))
                return false;

            return y <= YMax && y >= YMin;
        }*/
    }
}
