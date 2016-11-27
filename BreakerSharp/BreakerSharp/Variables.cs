namespace BreakerSharp
{
    using System;

    using Ensage;

    using global::BreakerSharp.Abilities;
    using global::BreakerSharp.Utilities;

    /// <summary>
    ///     The variables.
    /// </summary>
    public static class Variables
    {
        #region Public Properties

        /// <summary>
        ///     Gets or sets the armlet toggler.
        /// </summary>
        public static ArmletToggler ArmletToggler { get; set; }

        /// <summary>
        ///     Gets a value indicating whether charge away.
        /// </summary>
        public static bool ChargeAway
        {
            get
            {
                return MenuManager.ChargeAwayKeyPressed;
            }
        }

        /// <summary>
        ///     Gets or sets the charge of darkness.
        /// </summary>
        public static ChargeOfDarkness ChargeOfDarkness { get; set; }

        /// <summary>
        ///     Gets a value indicating whether combo.
        /// </summary>
        public static bool Combo
        {
            get
            {
                return MenuManager.ComboKeyPressed;
            }
        }

        /// <summary>
        ///     Gets or sets the enemy team.
        /// </summary>
        public static Team EnemyTeam { get; set; }

        /// <summary>
        ///     Gets or sets the hero.
        /// </summary>
        public static Hero Hero { get; set; }

        /// <summary>
        ///     Gets or sets the menu manager.
        /// </summary>
        public static MenuManager MenuManager { get; set; }

        /// <summary>
        ///     Gets or sets the nether strike.
        /// </summary>
        public static NetherStrike NetherStrike { get; set; }

        /// <summary>
        ///     Gets or sets the power treads switcher.
        /// </summary>
        public static PowerTreadsSwitcher PowerTreadsSwitcher { get; set; }

        /// <summary>
        ///     Gets the tick count.
        /// </summary>
        public static float TickCount
        {
            get
            {
                return Environment.TickCount & int.MaxValue;
            }
        }

        #endregion
    }
}