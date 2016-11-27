namespace BreakerSharp
{
    /// <summary>
    ///     The program.
    /// </summary>
    public static class Program
    {
        #region Static Fields

        /// <summary>
        ///     The bootstrap.
        /// </summary>
        private static Bootstrap bootstrap;

        #endregion

        #region Methods

        /// <summary>
        ///     The main.
        /// </summary>
        internal static void Main()
        {
            bootstrap = new Bootstrap();
            bootstrap.SubscribeEvents();
        }

        #endregion
    }
}