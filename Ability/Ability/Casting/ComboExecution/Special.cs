namespace Ability.Casting.ComboExecution
{
    using System.Linq;

    using Ability.AutoAttack;
    using Ability.ObjectManager;
    using Ability.ObjectManager.Towers;

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
                if (ability.ClassID == ClassID.CDOTA_Item_SentryWard)
                {
                    var sentry =
                        ObjectManager.GetEntities<Entity>()
                            .FirstOrDefault(
                                x =>
                                x.ClassID == ClassID.CDOTA_NPC_Observer_Ward_TrueSight && x.Distance2D(target) < 800);
                    if (sentry != null)
                    {
                        return false;
                    }

                    var tower =
                        AllyTowers.Towers.FirstOrDefault(x => x.IsValid && x.IsAlive && x.Distance2D(target) < 800);
                    if (tower != null)
                    {
                        return false;
                    }

                    tower = EnemyTowers.Towers.FirstOrDefault(x => x.IsValid && x.IsAlive && x.Distance2D(target) < 800);
                    if (tower != null)
                    {
                        return false;
                    }
                }

                var casted = ability.CastSkillShot(target, name, SoulRing.Check(ability) ? MyAbilities.SoulRing : null);
                if (!casted)
                {
                    return false;
                }

                if (ability.ClassID == ClassID.CDOTA_Item_SentryWard)
                {
                    Utils.Sleep(20000, ability.Handle.ToString());
                }

                return true;
            }

            if (ability.IsAbilityBehavior(AbilityBehavior.NoTarget, name))
            {
                Game.ExecuteCommand("dota_player_units_auto_attack_mode 0");
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