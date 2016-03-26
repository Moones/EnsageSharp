namespace BreakerSharp
{
    using System;

    using Ensage;
    using Ensage.Common;

    /// <summary>
    ///     The bootstrap.
    /// </summary>
    public class Bootstrap
    {
        #region Fields

        /// <summary>
        ///     The breaker sharp.
        /// </summary>
        private readonly BreakerSharp breakerSharp;

        #endregion

        #region Constructors and Destructors

        /// <summary>
        ///     Initializes a new instance of the <see cref="Bootstrap" /> class.
        /// </summary>
        public Bootstrap()
        {
            this.breakerSharp = new BreakerSharp();
        }

        #endregion

        #region Public Methods and Operators

        /// <summary>
        ///     The subscribe events.
        /// </summary>
        public void SubscribeEvents()
        {
            Events.OnLoad += this.Events_OnLoad;
            Events.OnClose += this.Events_OnClose;
            Game.OnUpdate += this.Game_OnUpdate;
            Game.OnWndProc += this.Game_OnWndProc;
            Drawing.OnDraw += this.Drawing_OnDraw;
            Player.OnExecuteOrder += this.Player_OnExecuteOrder;
        }

        #endregion

        #region Methods

        /// <summary>
        ///     The drawing_ on draw.
        /// </summary>
        /// <param name="args">
        ///     The args.
        /// </param>
        private void Drawing_OnDraw(EventArgs args)
        {
            this.breakerSharp.OnDraw();
        }

        /// <summary>
        ///     The events_ on close.
        /// </summary>
        /// <param name="sender">
        ///     The sender.
        /// </param>
        /// <param name="e">
        ///     The e.
        /// </param>
        private void Events_OnClose(object sender, EventArgs e)
        {
            this.breakerSharp.OnClose();
        }

        /// <summary>
        ///     The events_ on load.
        /// </summary>
        /// <param name="sender">
        ///     The sender.
        /// </param>
        /// <param name="e">
        ///     The e.
        /// </param>
        private void Events_OnLoad(object sender, EventArgs e)
        {
            this.breakerSharp.OnLoad();
        }

        /// <summary>
        ///     The game_ on update.
        /// </summary>
        /// <param name="args">
        ///     The args.
        /// </param>
        private void Game_OnUpdate(EventArgs args)
        {
            this.breakerSharp.OnUpdate();
        }

        /// <summary>
        ///     The game_ on wnd proc.
        /// </summary>
        /// <param name="args">
        ///     The args.
        /// </param>
        private void Game_OnWndProc(WndEventArgs args)
        {
            this.breakerSharp.OnWndProc(args);
        }

        /// <summary>
        ///     The player_ on execute order.
        /// </summary>
        /// <param name="sender">
        ///     The sender.
        /// </param>
        /// <param name="args">
        ///     The args.
        /// </param>
        private void Player_OnExecuteOrder(Player sender, ExecuteOrderEventArgs args)
        {
            if (sender.Equals(ObjectManager.LocalPlayer))
            {
                this.breakerSharp.Player_OnExecuteOrder(args);
            }
        }

        #endregion
    }
}