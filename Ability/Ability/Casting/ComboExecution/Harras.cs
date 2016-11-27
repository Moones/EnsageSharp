namespace Ability.Casting.ComboExecution
{
    using Ability.AutoAttack;
    using Ability.ObjectManager;

    using Ensage;
    using Ensage.Common;
    using Ensage.Common.Extensions;

    internal class Harras
    {
        #region Public Methods and Operators

        public static bool Cast(Ability ability, Unit target, string name)
        {
            if (ability.IsAbilityBehavior(AbilityBehavior.UnitTarget, name))
            {
                Game.ExecuteCommand("dota_player_units_auto_attack_mode 0");
                ManageAutoAttack.AutoAttackDisabled = true;
                SoulRing.Cast(ability);
                ability.UseAbility(target);
                return true;
            }

            if ((ability.IsAbilityBehavior(AbilityBehavior.AreaOfEffect, name)
                 || ability.IsAbilityBehavior(AbilityBehavior.Point, name))
                && (Prediction.StraightTime(target) > 1000 || target.MovementSpeed < 200))
            {
                Game.ExecuteCommand("dota_player_units_auto_attack_mode 0");
                ManageAutoAttack.AutoAttackDisabled = true;
                return ability.CastSkillShot(
                    target, 
                    MyHeroInfo.Position, 
                    name, 
                    SoulRing.Check(ability) ? MyAbilities.SoulRing : null);
            }

            if (ability.IsAbilityBehavior(AbilityBehavior.NoTarget, name))
            {
                SoulRing.Cast(ability);
                Game.ExecuteCommand("dota_player_units_auto_attack_mode 0");
                ManageAutoAttack.AutoAttackDisabled = true;
                ability.UseAbility();
                return true;
            }

            return false;
        }

        #endregion
    }
}