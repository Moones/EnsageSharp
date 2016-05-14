namespace Techies
{
    using Ensage;
    using Ensage.Common;

    using global::Techies.Utility;

    /// <summary>
    ///     The bootstrap.
    /// </summary>
    internal class Bootstrap
    {
        #region Constructors and Destructors

        /// <summary>
        ///     Initializes a new instance of the <see cref="Bootstrap" /> class.
        /// </summary>
        public Bootstrap()
        {
            var hero = ObjectManager.LocalHero;
            if (hero.ClassID != ClassID.CDOTA_Unit_Hero_Techies)
            {
                Variables.Instance = null;
                return;
            }

            Variables.Techies = hero;

            this.Techies = new Techies();

            foreach (var module in Variables.Modules)
            {
                module.OnLoad();
            }

            Events.OnUpdate += this.Techies.Game_OnUpdate;
            Drawing.OnDraw += this.Techies.Drawing_OnDraw;
            Game.OnWndProc += this.Techies.Game_OnWndProc;

            PrintOnLoadMessage();
        }

        #endregion

        #region Public Properties

        /// <summary>
        ///     Gets or sets the techies.
        /// </summary>
        public Techies Techies { get; set; }

        #endregion

        #region Methods

        /// <summary>
        ///     The print on load message.
        /// </summary>
        private static void PrintOnLoadMessage()
        {
            Game.PrintMessage("#Techies <font color='#00ff00'>loaded</font>!", MessageType.LogMessage);
        }

        #endregion
    }
}