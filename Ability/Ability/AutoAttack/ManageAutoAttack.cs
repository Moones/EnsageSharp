namespace Ability.AutoAttack
{
    using Ensage;
    using Ensage.Common;

    internal class ManageAutoAttack
    {
        #region Static Fields

        public static bool CurrentValue;

        #endregion

        #region Public Methods and Operators

        public static void UpdateAutoAttack()
        {
            if (Utils.SleepCheck("GlobalCasting") && !CurrentValue)
            {
                Game.ExecuteCommand("dota_player_units_auto_attack_after_spell 1");
                CurrentValue = true;
            }
            else if (!Utils.SleepCheck("GlobalCasting") && CurrentValue)
            {
                Game.ExecuteCommand("dota_player_units_auto_attack_after_spell 0");
                CurrentValue = false;
            }
        }

        #endregion
    }
}