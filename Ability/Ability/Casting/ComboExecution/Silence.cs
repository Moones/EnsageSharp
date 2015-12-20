namespace Ability.Casting.ComboExecution
{
    using Ability.AutoAttack;

    using Ensage;
    using Ensage.Common;
    using Ensage.Common.Extensions;

    internal class Silence
    {
        #region Public Methods and Operators

        public static bool Cast(Ability ability, Unit target, string name)
        {
            if (target.IsSilenced())
            {
                return false;
            }
            if (ability.IsAbilityBehavior(AbilityBehavior.UnitTarget, name))
            {
                Game.ExecuteCommand("dota_player_units_auto_attack_after_spell 1");
                ManageAutoAttack.CurrentValue = true;
                ability.UseAbility(target);
                return true;
            }
            if ((ability.IsAbilityBehavior(AbilityBehavior.AreaOfEffect, name)
                 || ability.IsAbilityBehavior(AbilityBehavior.Point, name))
                && (Prediction.StraightTime(target) > 1000 || target.MovementSpeed < 200))
            {
                Game.ExecuteCommand("dota_player_units_auto_attack_after_spell 1");
                ManageAutoAttack.CurrentValue = true;
                return ability.CastSkillShot(target, name);
            }
            if (ability.IsAbilityBehavior(AbilityBehavior.NoTarget, name))
            {
                Game.ExecuteCommand("dota_player_units_auto_attack_after_spell 1");
                ManageAutoAttack.CurrentValue = true;
                ability.UseAbility();
                return true;
            }
            return false;
        }

        #endregion
    }
}