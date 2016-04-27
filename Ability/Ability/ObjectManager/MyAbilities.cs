namespace Ability.ObjectManager
{
    using System.Collections.Generic;

    using Ensage;

    /// <summary>
    ///     The my abilities.
    /// </summary>
    internal class MyAbilities
    {
        #region Static Fields

        /// <summary>
        ///     The combo.
        /// </summary>
        public static IEnumerable<KeyValuePair<string, Ability>> Combo = new Dictionary<string, Ability>();

        /// <summary>
        ///     The nukes combo.
        /// </summary>
        public static List<Ability> NukesCombo = new List<Ability>();

        #endregion

        #region Public Properties

        /// <summary>
        ///     Gets or sets the blink.
        /// </summary>
        public static Ability Blink { get; set; }

        /// <summary>
        ///     Gets or sets the charge of darkness.
        /// </summary>
        public static Ability ChargeOfDarkness { get; set; }

        /// <summary>
        ///     Gets or sets the cyclone.
        /// </summary>
        public static Ability Cyclone { get; set; }

        /// <summary>
        ///     Gets or sets the defensive abilities.
        /// </summary>
        public static Dictionary<string, Ability> DefensiveAbilities { get; set; }

        /// <summary>
        ///     Gets or sets the force staff.
        /// </summary>
        public static Ability ForceStaff { get; set; }

        /// <summary>
        ///     Gets or sets the invoker e.
        /// </summary>
        public static Ability InvokerE { get; set; }

        /// <summary>
        ///     Gets or sets the invoker invoke.
        /// </summary>
        public static Ability InvokerInvoke { get; set; }

        /// <summary>
        ///     Gets or sets the invoker q.
        /// </summary>
        public static Ability InvokerQ { get; set; }

        /// <summary>
        ///     Gets or sets the invoker w.
        /// </summary>
        public static Ability InvokerW { get; set; }

        /// <summary>
        ///     Gets or sets the offensive abilities.
        /// </summary>
        public static Dictionary<string, Ability> OffensiveAbilities { get; set; }

        /// <summary>
        ///     Gets or sets the power treads.
        /// </summary>
        public static Ability PowerTreads { get; set; }

        /// <summary>
        ///     Gets or sets the soul ring.
        /// </summary>
        public static Ability SoulRing { get; set; }

        /// <summary>
        ///     Gets the spell 4.
        /// </summary>
        public static Ability Spell4
        {
            get
            {
                return AbilityMain.Me.Spellbook.Spell4;
            }
        }

        /// <summary>
        ///     Gets the spell 5.
        /// </summary>
        public static Ability Spell5
        {
            get
            {
                return AbilityMain.Me.Spellbook.Spell5;
            }
        }

        /// <summary>
        ///     Gets or sets the tinker rearm.
        /// </summary>
        public static Ability TinkerRearm { get; set; }

        #endregion
    }
}