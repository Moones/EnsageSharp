namespace Ability.Casting.ComboExecution
{
    using Ability.AutoAttack;
    using Ability.ObjectManager;

    using Ensage;
    using Ensage.Common;
    using Ensage.Common.Extensions;

    internal class Special
    {
        #region Public Methods and Operators

        public static bool Cast(Ability ability, Unit target, string name)
        {
            if (name == "razor_static_link" && target.Distance2D(AbilityMain.Me) > (ability.GetCastRange(name) * 0.75))
            {
                return false;
            }
            if (ability.IsAbilityBehavior(AbilityBehavior.UnitTarget, name))
            {
                Game.ExecuteCommand("dota_player_units_auto_attack_after_spell 0");
                ManageAutoAttack.AutoAttackDisabled = true;
                SoulRing.Cast(ability);
                ability.UseAbility(target);
                return true;
            }
            if ((ability.IsAbilityBehavior(AbilityBehavior.AreaOfEffect, name)
                 || ability.IsAbilityBehavior(AbilityBehavior.Point, name))
                && (Prediction.StraightTime(target) > 1000 || target.MovementSpeed < 200))
            {
                Game.ExecuteCommand("dota_player_units_auto_attack_after_spell 0");
                ManageAutoAttack.AutoAttackDisabled = true;
                return ability.CastSkillShot(target, name, SoulRing.Check(ability) ? MyAbilities.SoulRing : null);
            }
            if (ability.IsAbilityBehavior(AbilityBehavior.NoTarget, name))
            {
                Game.ExecuteCommand("dota_player_units_auto_attack_after_spell 0");
                ManageAutoAttack.AutoAttackDisabled = true;
                SoulRing.Cast(ability);
                ability.UseAbility();
                return true;
            }
            return false;
        }

        #endregion
    }
}