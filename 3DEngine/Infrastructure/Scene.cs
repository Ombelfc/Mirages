using _3DEngine.Components;
using _3DEngine.Utilities;
using System.Collections.Generic;

namespace _3DEngine.Infrastructure
{
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
