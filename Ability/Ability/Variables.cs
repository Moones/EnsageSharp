namespace Ability
{
    using Ability.Casting.ComboExecution;

    /// <summary>
    ///     The variables.
    /// </summary>
    public static class Variables
    {
        #region Public Properties

        /// <summary>
        ///     Gets or sets the dealt damage.
        /// </summary>
        public static float DealtDamage { get; set; }

        /// <summary>
        ///     Gets or sets the ethereal hit time.
        /// </summary>
        public static float EtherealHitTime { get; set; }

        /// <summary>
        ///     Gets or sets the kill steal.
        /// </summary>
        public static Killsteal Killsteal { get; set; }

        #endregion
    }
}