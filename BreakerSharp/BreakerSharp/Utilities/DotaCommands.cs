namespace BreakerSharp.Utilities
{
    using Ensage;

    using SharpDX;

    /// <summary>
    ///     The dota commands.
    /// </summary>
    public static class DotaCommands
    {
        #region Public Methods and Operators

        /// <summary>
        ///     The move camera.
        /// </summary>
        /// <param name="position">
        ///     The position.
        /// </param>
        public static void MoveCamera(Vector3 position)
        {
            Game.ExecuteCommand("dota_camera_set_lookatpos " + position.X + " " + position.Y);
        }

        #endregion
    }
}