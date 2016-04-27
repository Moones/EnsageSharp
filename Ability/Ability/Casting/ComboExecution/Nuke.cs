namespace Ability.Casting.ComboExecution
{
    using System;
    using System.Threading;

    using Ability.AbilityMenu.Menus.NukesMenu;
    using Ability.AutoAttack;
    using Ability.ObjectManager;

    using Ensage;
    using Ensage.Common;
    using Ensage.Common.AbilityInfo;
    using Ensage.Common.Extensions;
    using Ensage.Common.Menu;

    internal class Nuke
    {
        #region Public Methods and Operators

        public static bool Cast(Ability ability, Unit target, string name, bool disabled = false)
        {
            if (!disabled)
            {
                disabled = target.IsStunned() || target.IsRooted() || target.IsHexed() || target.IsInvul();
            }

            if (target.HasModifier("modifier_item_blade_mail_reflect")
                && AbilityDamage.CalculateDamage(ability, AbilityMain.Me, target) > AbilityMain.Me.Health)
            {
                return false;
            }

            if (target.HasModifier("modifier_nyx_assassin_spiked_carapace"))
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

            if (ability.IsAbilityBehavior(AbilityBehavior.UnitTarget, name) && ability.Name != "lion_impale"
                && ability.Name != "lina_dragon_slave")
            {
                if (target.IsLinkensProtected())
                {
                    if (!Utils.SleepCheck("AbilitySharp.CancelLinkens"))
                    {
                        return false;
                    }

                    if (MyAbilities.Cyclone != null && MyAbilities.Cyclone.CanBeCasted())
                    {
                        MyAbilities.Cyclone.UseAbility(target);
                    }
                    else if (MyAbilities.ForceStaff != null && MyAbilities.ForceStaff.CanBeCasted())
                    {
                        MyAbilities.ForceStaff.UseAbility(target);
                    }

                    Utils.Sleep(1000, "AbilitySharp.CancelLinkens");

                    Game.ExecuteCommand("dota_player_units_auto_attack_mode 0");
                    ManageAutoAttack.AutoAttackDisabled = true;
                    SoulRing.Cast(ability);
                    ability.UseAbility(target, true);
                    return false;
                }

                Game.ExecuteCommand("dota_player_units_auto_attack_mode 0");
                ManageAutoAttack.AutoAttackDisabled = true;
                SoulRing.Cast(ability);
                ability.UseAbility(target);
                DelayAction.Add(
                    new DelayActionItem(300, () => { AbilityMain.LaunchSnowball(); }, CancellationToken.None));
                return true;
            }

            if ((ability.IsAbilityBehavior(AbilityBehavior.AreaOfEffect, name)
                 || ability.IsAbilityBehavior(AbilityBehavior.Point, name))
                && (disabled
                    || Prediction.StraightTime(target)
                    > Nukes.NukesMenuDictionary[name].Item(name + "minstraighttime").GetValue<Slider>().Value
                    || target.MovementSpeed < 200))
            {
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
                                Game.ExecuteCommand("dota_player_units_auto_attack_mode 0");
                                ManageAutoAttack.AutoAttackDisabled = true;
                                var casted2 = ability.CastSkillShot(
                                    target, 
                                    MyHeroInfo.Position, 
                                    name, 
                                    SoulRing.Check(ability) ? MyAbilities.SoulRing : null);
                                if (!casted2)
                                {
                                    return;
                                }

                                if (!disabled && Utils.SleepCheck(ability.Handle.ToString())
                                    && ability.GetCastDelay(AbilityMain.Me, target, true) * 1000 - Game.Ping > 0.1)
                                {
                                    DelayAction.Add(
                                        new DelayActionItem(
                                            (int)
                                            (ability.GetCastDelay(AbilityMain.Me, target, true) * 1000
                                             - Math.Max(50, Game.Ping)), 
                                            () =>
                                                {
                                                    if (Prediction.StraightTime(target)
                                                        < (600
                                                           + (ability.GetCastDelay(AbilityMain.Me, target, true) * 1000
                                                              - Math.Max(50, Game.Ping))) && target.MovementSpeed > 200)
                                                    {
                                                        AbilityMain.Me.Stop();
                                                    }
                                                }, 
                                            CancellationToken.None));
                                }
                            });
                    Utils.Sleep((Game.Ping * 2) + 200, "cancelorder");
                    Utils.Sleep((Game.Ping * 2) + 200, ability.Handle.ToString());
                    Utils.Sleep((Game.Ping * 2) + 200, "casting");
                }

                Game.ExecuteCommand("dota_player_units_auto_attack_mode 0");
                ManageAutoAttack.AutoAttackDisabled = true;
                var casted = ability.CastSkillShot(
                    target, 
                    MyHeroInfo.Position, 
                    name, 
                    SoulRing.Check(ability) ? MyAbilities.SoulRing : null);
                if (!casted)
                {
                    return false;
                }

                if (!disabled && Utils.SleepCheck(ability.Handle.ToString())
                    && ability.GetCastDelay(AbilityMain.Me, target, true) * 1000 - Game.Ping > 0.1)
                {
                    DelayAction.Add(
                        new DelayActionItem(
                            (int)(ability.GetCastDelay(AbilityMain.Me, target, true) * 1000 - Math.Max(50, Game.Ping)), 
                            () =>
                                {
                                    if (Prediction.StraightTime(target)
                                        < (600
                                           + (ability.GetCastDelay(AbilityMain.Me, target, true) * 1000
                                              - Math.Max(50, Game.Ping))) && target.MovementSpeed > 200)
                                    {
                                        AbilityMain.Me.Stop();
                                    }
                                }, 
                            CancellationToken.None));
                }

                return true;
            }

            if (ability.IsAbilityBehavior(AbilityBehavior.NoTarget, name))
            {
                if (ability.Name == "templar_assassin_meld")
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
                    DelayAction.Add(
                        new DelayActionItem(
                            (int)ability.GetCastDelay(AbilityMain.Me, target) * 1000, 
                            () => { AbilityMain.Me.Attack(target); }, 
                            CancellationToken.None));
                    Utils.Sleep(ability.GetCastDelay(AbilityMain.Me, target) * 1000, "GlobalCasting");
                    Utils.Sleep(ability.GetCastDelay(AbilityMain.Me, target) * 1000 + 200, "casting");
                    Utils.Sleep(ability.GetCastDelay(AbilityMain.Me, target) * 1000 + 200, "Ability.Move");
                    Utils.Sleep(ability.GetCastDelay(AbilityMain.Me, target) * 1000 + 200, "cancelorder");
                    return true;
                }

                if (ability.Name.Contains("nevermore_shadowraze"))
                {
                    Game.ExecuteCommand("dota_player_units_auto_attack_mode 0");
                    ManageAutoAttack.AutoAttackDisabled = true;
                    return ability.CastSkillShot(target, name);
                }

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