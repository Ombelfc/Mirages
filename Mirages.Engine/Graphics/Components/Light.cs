using Mirages.Infrastructure.Components;
using Mirages.Infrastructure.Components.Colors;

namespace Mirages.Engine.Graphics.Components
{
    /// <summary>
    /// Struct representing a light vector.
    /// </summary>
    public struct Light
    {
        #region Fields

        /// <summary>
        /// 3D vector component of the light.
        /// </summary>
        public Vector3 Position { get; private set; }
        /// <summary>
        /// Color component of the light.
        /// </summary>
        public ByteColor Color { get; private set; }

        #endregion

        #region Constructors

        /// <summary>
        /// Creates a new light instance from a 3D vector and a color.
        /// </summary>
        /// <param name="vector"></param>
        /// <param name="color"></param>
        public Light(Vector3 vector, ByteColor color)
        {
            Position = vector;
            Color = color;
        }

        #endregion
    }
}
