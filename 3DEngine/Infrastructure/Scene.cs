﻿using _3DEngine.Components;
using System;
using System.Collections.Generic;
using System.Text;

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
    }
}