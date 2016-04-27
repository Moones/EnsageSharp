namespace Ability.Casting.ComboExecution
{
    using System;
    using System.Threading;

    using Ability.AutoAttack;
    using Ability.ObjectManager;

    using Ensage;
    using Ensage.Common;
    using Ensage.Common.Extensions;

    internal class Buff
    {
        #region Public Methods and Operators

        public static bool Cast(Ability ability, Unit target, Unit buffTarget, string name, bool togglearmlet = false)
        {
            if (name == "item_armlet")
            {
                if (buffTarget.HasModifier("modifier_ice_blast"))
                {
                    return false;
                }

                if (!togglearmlet && buffTarget.Distance2D(target) > Math.Max(buffTarget.GetAttackRange() + 150, 500))
                {
                    return false;
                }

                var armlettoggled = buffTarget.HasModifier("modifier_item_armlet_unholy_strength") && ability.IsToggled;
                Game.ExecuteCommand("dota_player_units_auto_attack_mode 0");
                ManageAutoAttack.AutoAttackDisabled = true;
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

            SoulRing.Cast(ability);
            if (ability.Name == "templar_assassin_refraction")
            {
                var meld = AbilityMain.Me.Spellbook.Spell2;
                if (meld != null && meld.CanBeCasted())
                {
                    if (
                        !(target.Distance2D(MyHeroInfo.Position)
                          < (AbilityMain.Me.GetAttackRange() + 50 + target.HullRadius + AbilityMain.Me.HullRadius))
                        || Orbwalking.AttackOnCooldown(target) || AbilityMain.Me.IsAttacking()
                        || (target.Predict(Game.Ping).Distance2D(MyHeroInfo.Position)
                            > (AbilityMain.Me.GetAttackRange() + 50 + target.HullRadius + AbilityMain.Me.HullRadius))
                        || !Utils.SleepCheck("GlobalCasting"))
                    {
                        return false;
                    }

                    Game.ExecuteCommand("dota_player_units_auto_attack_mode 0");
                    ManageAutoAttack.AutoAttackDisabled = true;
                    ability.UseAbility();
                    if (Nuke.Cast(meld, target, NameManager.Name(meld)))
                    {
                        DelayAction.Add(
                            new DelayActionItem(
                                (int)meld.GetCastDelay(AbilityMain.Me, target) * 1000 + 100, 
                                () => { AbilityMain.Me.Attack(target); }, 
                                CancellationToken.None));
                    }

                    Utils.Sleep(meld.GetCastDelay(AbilityMain.Me, target) * 1000, "GlobalCasting");
                    Utils.Sleep(meld.GetHitDelay(target, name) * 1000 + 200, "casting");
                    Utils.Sleep(meld.GetHitDelay(target, name) * 1000 + 200, ability.Handle.ToString());
                    Utils.Sleep(meld.GetCastDelay(AbilityMain.Me, target) * 1000 + 200, "cancelorder");
                    return true;
                }
            }

            if (AbilityMain.Me.ClassID == ClassID.CDOTA_Unit_Hero_Invoker && !ability.CanBeCasted())
            {
                var invoked = ability.Invoke();
                if (!invoked)
                {
                    return false;
                }

                DelayAction.Add(
                    Game.Ping * 2, 
                    () =>
                        {
                            if (ability.IsAbilityBehavior(AbilityBehavior.NoTarget, name))
                            {
                                Game.ExecuteCommand("dota_player_units_auto_attack_mode 0");
                                ManageAutoAttack.AutoAttackDisabled = true;
                                ability.UseAbility();
                            }

                            Game.ExecuteCommand("dota_player_units_auto_attack_mode 0");
                            ManageAutoAttack.AutoAttackDisabled = true;
                            ability.UseAbility(buffTarget);
                        });
                Utils.Sleep((Game.Ping * 2) + 200, "cancelorder");
                Utils.Sleep((Game.Ping * 2) + 200, ability.Handle.ToString());
                Utils.Sleep((Game.Ping * 2) + 200, "casting");
                return true;
            }

            if (ability.IsAbilityBehavior(AbilityBehavior.NoTarget, name))
            {
                Game.ExecuteCommand("dota_player_units_auto_attack_mode 0");
                ManageAutoAttack.AutoAttackDisabled = true;
                ability.UseAbility();
                return true;
            }

            Game.ExecuteCommand("dota_player_units_auto_attack_mode 0");
            ManageAutoAttack.AutoAttackDisabled = true;
            ability.UseAbility(buffTarget);
            return true;
        }

        #endregion
    }
}