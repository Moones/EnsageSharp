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
            if (Utils.SleepCheck("cancelorder") && AutoAttackDisabled)
            {
                Game.ExecuteCommand("dota_player_units_auto_attack_mode 1");
                AutoAttackDisabled = false;
            }
            else if (!Utils.SleepCheck("cancelorder") && !AutoAttackDisabled)
            {
                Game.ExecuteCommand("dota_player_units_auto_attack_mode 0");
                AutoAttackDisabled = true;
            }
        }

        #endregion
    }
}