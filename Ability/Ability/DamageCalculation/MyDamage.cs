namespace Ability.DamageCalculation
{
    using System;

    using Ability.OnUpdate;

    using Ensage.Common;

    internal class MyDamage
    {
        #region Static Fields

        public static float BonusDamage;

        public static float MinDamage;

        #endregion

        #region Public Methods and Operators

        public static void Update(EventArgs args)
        {
            if (!Utils.SleepCheck("MyDamage.OnUpdate"))
            {
                return;
            }

            if (!OnUpdateChecks.CanUpdate())
            {
                return;
            }

            MinDamage = AbilityMain.Me.MinimumDamage;
            BonusDamage = AbilityMain.Me.BonusDamage;
            Utils.Sleep(500, "MyDamage.OnUpdate");
        }

        #endregion
    }
}