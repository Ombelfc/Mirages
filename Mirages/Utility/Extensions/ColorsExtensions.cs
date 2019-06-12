using Mirages.Infrastructure.Components.Colors;

namespace Mirages.Utility.Extensions
{
    /// <summary>
    /// Static class containing various colors used across the application.
    /// </summary>
    public static class ColorsExtensions
    {
        /// <summary>
        /// Returns a black color (hex: #000000).
        /// </summary>
        public static ByteColor Black => new ByteColor(0, 1);
        
        /// <summary>
        /// Returns a light-gray color (hex: #FFD3D3D3)
        /// </summary>
        public static ByteColor LightGray => new ByteColor(211, 211, 211, 255);
        /// <summary>
        /// Returns an alice-blue color (hex: #FFF0F8FF)
        /// </summary>
        public static ByteColor AliceBlue => new ByteColor(240, 248, 255, 255);
        /// <summary>
        /// Returns an orange-red color (hex: #FFFF4500)
        /// </summary>
        public static ByteColor OrangeRed => new ByteColor(255, 69, 0, 1);
        /// <summary>
        /// Returns a firebrick color (hex: #FFB22222)
        /// </summary>
        public static ByteColor Firebrick => new ByteColor(178, 34, 34, 255);

        // TODO : Add documentation when you find out about their use.
        //public static ByteColor Yellow => new ByteColor(1, 1, 0, 1);
        //public static ByteColor Red => new ByteColor(1, 0, 0, 1);
        //public static ByteColor Blue => new ByteColor(0, 0, 1, 1);
        //public static ByteColor Green => new ByteColor(0, 1, 0, 1);
        //public static ByteColor DarkGrey => new ByteColor(0.3, 0.3, 0.3, 1);
    }
}
