namespace Ability.Casting.ComboExecution
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using Ability.AutoAttack;
    using Ability.ObjectManager;

    using Ensage;
    using Ensage.Common.Extensions;

    internal class Buff
    {
        #region Public Methods and Operators

        public static bool Cast(Ability ability, Unit target, Unit buffTarget, string name, List<Modifier> modifiers)
        {
            if (name == "item_armlet")
            {
                if (buffTarget.Distance2D(target) > Math.Max(target.GetAttackRange(), 500))
                {
                    return false;
                }
                var armlettoggled = modifiers.Any(x => x.Name == "modifier_item_armlet_unholy_strength")
                                    && ability.IsToggled;
                Game.ExecuteCommand("dota_player_units_auto_attack_after_spell 1");
                ManageAutoAttack.CurrentValue = true;
                if (armlettoggled)
                {
                    ability.ToggleAbility();
                    ability.ToggleAbility();
                    return true;
                }
                ability.ToggleAbility();
                return true;
            }
            if (!(buffTarget.Distance2D(target) < MyHeroInfo.AttackRange() + 150))
            {
                return false;
            }
            if (ability.IsAbilityBehavior(AbilityBehavior.NoTarget, name))
            {
                Game.ExecuteCommand("dota_player_units_auto_attack_after_spell 1");
                ManageAutoAttack.CurrentValue = true;
                ability.UseAbility();
                return true;
            }
            Game.ExecuteCommand("dota_player_units_auto_attack_after_spell 1");
            ManageAutoAttack.CurrentValue = true;
            ability.UseAbility(buffTarget);
            return true;
        }

        #endregion
    }
}