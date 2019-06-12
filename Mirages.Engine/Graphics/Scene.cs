using Mirages.Engine.Graphics.Components;
using System.Collections.Generic;

namespace Mirages.Engine.Graphics
{
    /// <summary>
    /// Object representing a 3D scene.
    /// </summary>
    public class Scene
    {
        /// <summary>
        /// Camera observing the scene.
        /// </summary>
        public Camera Camera { get; set; }

        /// <summary>
        /// List of objects appearing on the stage.
        /// </summary>
        public List<Mesh> Meshes { get; set; }

        /// <summary>
        /// List of light vectors in the scene
        /// </summary>
        public List<Light> Lights { get; set; }
    }
}
