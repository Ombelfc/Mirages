namespace Mirages.Engine.Graphics.Components
{
    /// <summary>
    /// Struct representing part of a boundary of a solid object.
    /// </summary>
    public struct Face
    {
        #region Fields

        /// <summary>
        /// First point of a face.
        /// </summary>
        public int A { get; private set; }
        /// <summary>
        /// Second point of a face.
        /// </summary>
        public int B { get; private set; }
        /// <summary>
        /// Third point of a face.
        /// </summary>
        public int C { get; private set; }

        #endregion

        #region Constructors

        /// <summary>
        /// Creates a new face with the given values.
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <param name="c"></param>
        public Face(int a, int b, int c)
        {
            A = a;
            B = b;
            C = c;
        }

        #endregion

        /*public void Edges(Action<int, int> action)
        {
            action.Invoke(A, B);
            action.Invoke(B, C);
            action.Invoke(A, C);
        }*/
    }
}
