namespace Mirages.Infrastructure.Components.Colors
{
    /// <summary>
    /// Class containing various colors used across the application.
    /// </summary>
    public static class Colors
    {
        /// <summary>
        /// Returns a black color (hex: #000000).
        /// </summary>
        public static ByteColor Black => new ByteColor(0, 1);

        // TODO : Add documentation when you find out about their use.
        public static ByteColor Yellow => new ByteColor(1, 1, 0, 1);
        public static ByteColor Red => new ByteColor(1, 0, 0, 1);
        public static ByteColor Blue => new ByteColor(0, 0, 1, 1);
        public static ByteColor Green => new ByteColor(0, 1, 0, 1);

        //public static ByteColor DarkGrey => new ByteColor(0.3, 0.3, 0.3, 1);
    }
}
