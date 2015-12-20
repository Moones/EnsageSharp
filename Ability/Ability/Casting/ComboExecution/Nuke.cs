namespace Ability.Casting.ComboExecution
{
    using System.Linq;

    using Ability.AutoAttack;
    using Ability.ObjectManager;

    using Ensage;
    using Ensage.Common;
    using Ensage.Common.AbilityInfo;
    using Ensage.Common.Extensions;

    internal class Nuke
    {
        #region Public Methods and Operators

        public static bool Cast(Ability ability, Unit target, string name)
        {
            if (target.Modifiers.Any(x => x.Name == "modifier_item_blade_mail_reflect")
                && AbilityDamage.CalculateDamage(ability, AbilityMain.Me, target) > AbilityMain.Me.Health)
            {
                return false;
            }
            if (target.Modifiers.Any(x => x.Name == "modifier_nyx_assassin_spiked_carapace"))
            {
                return false;
            }
            if (ability.Name == "sniper_assassinate")
            {
                if (AbilityMain.Me.Distance2D(target) <= AbilityMain.Me.GetAttackRange() + 100)
                {
                    return false;
                }
            }
            if (ability.IsAbilityBehavior(AbilityBehavior.UnitTarget, name) && ability.Name != "lion_impale")
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
                if (ability.Name == "templar_assassin_meld")
                {
                    if (!(target.Distance2D(MyHeroInfo.Position) < (AbilityMain.Me.GetAttackRange() + 50))
                        || Orbwalking.AttackOnCooldown(target))
                    {
                        return false;
                    }
                    Game.ExecuteCommand("dota_player_units_auto_attack_after_spell 1");
                    ManageAutoAttack.CurrentValue = true;
                    ability.UseAbility();
                    AbilityMain.Me.Attack(target);
                    return true;
                }
                if (ability.Name.Contains("nevermore_shadowraze"))
                {
                    Game.ExecuteCommand("dota_player_units_auto_attack_after_spell 1");
                    ManageAutoAttack.CurrentValue = true;
                    return ability.CastSkillShot(target, name);
                }
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