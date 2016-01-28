namespace Techies
{
    using System;

    using Ensage;
    using Ensage.Common;

    using global::Techies.Utility;

    /// <summary>
    ///     The program.
    /// </summary>
    internal class Program
    {
        #region Methods

        /// <summary>
        ///     The event on close.
        /// </summary>
        /// <param name="sender">
        ///     The sender.
        /// </param>
        /// <param name="e">
        ///     The e.
        /// </param>
        private static void Events_OnClose(object sender, EventArgs e)
        {
            if (Variables.Instance == null)
            {
                return;
            }

            foreach (var module in Variables.Modules)
            {
                module.OnClose();
            }

            Game.OnUpdate -= Variables.Instance.Techies.Game_OnUpdate;
            Drawing.OnDraw -= Variables.Instance.Techies.Drawing_OnDraw;
            Game.OnWndProc -= Variables.Instance.Techies.Game_OnWndProc;
            Variables.Damage.OnClose();

            Variables.Techies = null;
            Variables.Instance = null;
        }

        /// <summary>
        ///     The events on load.
        /// </summary>
        /// <param name="sender">
        ///     The sender.
        /// </param>
        /// <param name="e">
        ///     The e.
        /// </param>
        private static void Events_OnLoad(object sender, EventArgs e)
        {
            Variables.Instance = new Bootstrap();
        }

        /// <summary>
        ///     The main.
        /// </summary>
        private static void Main()
        {
            Events.OnLoad += Events_OnLoad;
            Events.OnClose += Events_OnClose;
        }

        #endregion
    }
}