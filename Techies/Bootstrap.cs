namespace Techies
{
    using Ensage;

    using global::Techies.Utility;

    /// <summary>
    ///     The bootstrap.
    /// </summary>
    internal class Bootstrap
    {
        #region Fields

        /// <summary>
        ///     The techies.
        /// </summary>
        public Techies Techies;

        #endregion

        #region Constructors and Destructors

        /// <summary>
        ///     Initializes a new instance of the <see cref="Bootstrap" /> class.
        /// </summary>
        public Bootstrap()
        {
            var hero = ObjectMgr.LocalHero;
            if (hero.ClassID != ClassID.CDOTA_Unit_Hero_Techies)
            {
                return;
            }

            if (Variables.Instance != null)
            {
                return;
            }

            Variables.Techies = hero;

            this.Techies = new Techies();

            PrintOnLoadMessage();
        }

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