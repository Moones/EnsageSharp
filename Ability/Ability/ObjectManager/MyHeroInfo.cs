namespace Ability.ObjectManager
{
    using Ensage;
    using Ensage.Common;
    using Ensage.Common.Extensions;

    using SharpDX;

    internal class MyHeroInfo
    {
        #region Static Fields

        public static Vector3 Position;

        private static float attackRange;

        private static double attackRate;

        #endregion

        #region Public Methods and Operators

        /// <summary>
        ///     The attack range.
        /// </summary>
        /// <returns>
        ///     The <see cref="float" />.
        /// </returns>
        public static float AttackRange()
        {
            if (!Utils.SleepCheck("MyHeroInfo.AttackRange"))
            {
                return attackRange;
            }

            attackRange = AbilityMain.Me.GetAttackRange();
            Utils.Sleep(1000, "MyHeroInfo.AttackRange");
            return attackRange;
        }

        /// <summary>
        ///     The attack rate.
        /// </summary>
        /// <returns>
        ///     The <see cref="double" />.
        /// </returns>
        public static double AttackRate()
        {
            if (!Utils.SleepCheck("MyAttackRate"))
            {
                return attackRate;
            }

            attackRate = UnitDatabase.GetAttackRate(AbilityMain.Me);
            Utils.Sleep(500, "MyAttackRate");
            return attackRate;
        }

        /// <summary>
        ///     The update position.
        /// </summary>
        public static void UpdatePosition()
        {
            if (Utils.SleepCheck("mePosition"))
            {
                Position = AbilityMain.Me.NetworkActivity == NetworkActivity.Move
                               ? Prediction.InFront(AbilityMain.Me, AbilityMain.Me.MovementSpeed * (Game.Ping / 1000))
                               : AbilityMain.Me.Position;
            }
        }

        #endregion
    }
}