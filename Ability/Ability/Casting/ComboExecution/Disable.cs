namespace Ability.Casting.ComboExecution
{
    using System.Threading;

    using Ability.AbilityMenu.Menus.DisablesMenu;
    using Ability.ObjectManager;

    using Ensage;
    using Ensage.Common;
    using Ensage.Common.Extensions;
    using Ensage.Common.Menu;

    internal class Disable
    {
        #region Public Methods and Operators

        public static bool Cast(Ability ability, Unit target, string name)
        {
            if (AbilityMain.Me.ClassID == ClassID.CDOTA_Unit_Hero_Pudge && name == "pudge_dismember"
                && Utils.SleepCheck("rotToggle") && AbilityMain.Me.Distance2D(target) < 400
                && Utils.ChainStun(target, Game.Ping, null, false))
            {
                var rot = AbilityMain.Me.Spellbook.Spell2;
                if (!rot.IsToggled)
                {
                    rot.ToggleAbility();
                    Utils.Sleep(500, "rotToggle");
                }
            }

            var straightTime = Disables.DisablesMenuDictionary[name].Item(name + "minstraighttime") != null
                                   ? (float)
                                     Disables.DisablesMenuDictionary[name].Item(name + "minstraighttime")
                                         .GetValue<Slider>()
                                         .Value / 1000
                                   : 1;
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
                            ability.CastStun(
                                target, 
                                MyHeroInfo.Position, 
                                straightTime, 
                                abilityName: name, 
                                useSleep:
                                    name != "ancient_apparition_cold_feet" && name != "rattletrap_battery_assault"
                                    && name != "pudge_meat_hook" && name != "pudge_dismember" && name != "pudge_rot", 
                                soulRing: SoulRing.Check(ability) ? MyAbilities.SoulRing : null);
                        });

                return true;
            }

            var casted = ability.CastStun(
                target, 
                MyHeroInfo.Position, 
                straightTime, 
                abilityName: name, 
                useSleep:
                    name != "ancient_apparition_cold_feet" && name != "rattletrap_battery_assault"
                    && name != "pudge_meat_hook" && name != "pudge_dismember" && name != "pudge_rot", 
                soulRing: SoulRing.Check(ability) ? MyAbilities.SoulRing : null);
            if (!casted)
            {
                return false;
            }

            DelayAction.Add(new DelayActionItem(300, () => { AbilityMain.LaunchSnowball(); }, CancellationToken.None));

            if (name == "spirit_breaker_charge_of_darkness")
            {
                Utils.Sleep(1500, "Ability.Move");
                Utils.Sleep(1500, "cancelorder");
                Utils.Sleep(1500, "GlobalCasting");
            }

            return true;
        }

        #endregion
    }
}