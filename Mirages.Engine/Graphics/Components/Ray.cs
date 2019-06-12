using Mirages.Infrastructure.Components;

namespace Mirages.Engine.Graphics.Components
{
    /// <summary>
    /// Struct representing a ray vector.
    /// </summary>
    public struct Ray
    {
        #region Fields

        /// <summary>
        /// 3D vector component indicating the start of the ray.
        /// </summary>
        public Vector3 Start { get; private set; }
        /// <summary>
        /// 3D vector component indicating the end of the ray.
        /// </summary>
        public Vector3 Direction { get; private set; }

        #endregion

        #region Constructors

        /// <summary>
        /// Creates a new ray instance with a start vector and an end vector.
        /// </summary>
        /// <param name="start"></param>
        /// <param name="direction"></param>
        public Ray(Vector3 start, Vector3 direction)
        {
            Start = start;
            Direction = direction;
        }

        #endregion
    }
}
