namespace Ability.Casting.ComboExecution
{
    using Ability.AutoAttack;

    using Ensage;
    using Ensage.Common.Extensions;

    internal class Shield
    {
        #region Public Methods and Operators

        public static bool Cast(Ability ability, Unit target, string name)
        {
            if (ability.IsAbilityBehavior(AbilityBehavior.NoTarget, name))
            {
                Game.ExecuteCommand("dota_player_units_auto_attack_mode 0");
                ManageAutoAttack.AutoAttackDisabled = true;
                SoulRing.Cast(ability);
                ability.UseAbility();
                return true;
            }

            if ((name == "item_solar_crest" || name == "item_medallion_of_courage") && target.Equals(AbilityMain.Me))
            {
                return false;
            }

            SoulRing.Cast(ability);
            Game.ExecuteCommand("dota_player_units_auto_attack_mode 0");
            ManageAutoAttack.AutoAttackDisabled = true;
            ability.UseAbility(target);
            return true;
        }

        #endregion
    }
}