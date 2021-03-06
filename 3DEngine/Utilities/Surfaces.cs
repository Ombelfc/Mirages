﻿using _3DEngine.Components;

namespace _3DEngine.Utilities
{
    public static class Surfaces
    {
        public static readonly Surface CheckerBoard = new CheckerBoardSurface();
        public static readonly Surface Shiny = new ShinySurface();

        private class CheckerBoardSurface : Surface
        {
            public CheckerBoardSurface() : base(roughness: 150) { }

            public override Color Diffuse(Vector3 position)
            {
                return (((int)(position.Z) + (int)(position.X)) & 1) != 0 ? new Color(1, 1, 1, 0) : new Color(0, 0, 0, 0);
            }

            public override double Reflect(Vector3 position)
            {
                return (((int)(position.Z) + (int)(position.X)) & 1) != 0 ? 0.1 : 0.7;
            }

            public override Color Specular(Vector3 position)
            {
                return new Color(1, 1, 1, 0);
            }
        }

        private class ShinySurface : Surface
        {
            public ShinySurface() : base(roughness: 50) { }

            public override Color Diffuse(Vector3 position)
            {
                return new Color(1, 1, 1, 0);
            }

            public override double Reflect(Vector3 position)
            {
                return 0.6;
            }

            public override Color Specular(Vector3 position)
            {
                return new Color(0.5, 0.5, 0.5, 0);
            }
        }
    }
}
