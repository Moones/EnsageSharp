namespace Ability
{
    using Ability.Casting.ComboExecution;

    using Ensage;
    using Ensage.Common.Extensions;

    using SharpDX;

    /// <summary>
    ///     The variables.
    /// </summary>
    public static class Variables
    {
        #region Public Properties

        /// <summary>
        ///     Gets or sets the auto usage.
        /// </summary>
        public static AutoUsage AutoUsage { get; set; }

        /// <summary>
        ///     Gets or sets the dealt damage.
        /// </summary>
        public static float DealtDamage { get; set; }

        /// <summary>
        ///     Gets the ethereal hit time.
        /// </summary>
        public static float EtherealHitTime
        {
            get
            {
                if (LastEtherealTarget == null)
                {
                    return 0;
                }

                return LastEtherealCastTime
                       + ((LastEtherealCastPosition.Distance2D(LastEtherealTarget.NetworkPosition) / 1200) * 1000) + 100;
            }
        }

        /// <summary>
        ///     Gets or sets the kill steal.
        /// </summary>
        public static Killsteal Killsteal { get; set; }

        /// <summary>
        ///     Gets or sets the last ethereal cast position.
        /// </summary>
        public static Vector3 LastEtherealCastPosition { get; set; }

        /// <summary>
        ///     Gets or sets the last ethereal cast time.
        /// </summary>
        public static float LastEtherealCastTime { get; set; }

        /// <summary>
        ///     Gets or sets the last ethereal target.
        /// </summary>
        public static Unit LastEtherealTarget { get; set; }

        #endregion
    }
}