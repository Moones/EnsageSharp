namespace Ability.AutoAttack
{
    using Ensage;
    using Ensage.Common;

    internal class ManageAutoAttack
    {
        #region Static Fields

        public static bool AutoAttackDisabled;

        #endregion

        #region Public Methods and Operators

        public static void UpdateAutoAttack()
        {
            if (Utils.SleepCheck("GlobalCasting") && AutoAttackDisabled)
            {
                Game.ExecuteCommand("dota_player_units_auto_attack_after_spell 1");
                AutoAttackDisabled = false;
            }
            else if (!Utils.SleepCheck("GlobalCasting") && !AutoAttackDisabled)
            {
                Game.ExecuteCommand("dota_player_units_auto_attack_after_spell 0");
                AutoAttackDisabled = true;
            }
        }

        #endregion
    }
}