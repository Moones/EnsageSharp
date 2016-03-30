namespace Ability.Casting.ComboExecution
{
    using System.Threading;

    using Ability.ObjectManager;

    using Ensage;
    using Ensage.Common;
    using Ensage.Common.Extensions;

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

            var casted = ability.CastStun(
                target, 
                1, 
                abilityName: name, 
                useSleep: name != "ancient_apparition_cold_feet" && name != "rattletrap_battery_assault", 
                soulRing: SoulRing.Check(ability) ? MyAbilities.SoulRing : null);
            if (casted)
            {
                DelayAction.Add(
                    new DelayActionItem(300, () => { AbilityMain.LaunchSnowball(); }, CancellationToken.None));

                if (name == "spirit_breaker_charge_of_darkness")
                {
                    Utils.Sleep(1500, "Ability.Move");
                    Utils.Sleep(1500, "cancelorder");
                    Utils.Sleep(1500, "GlobalCasting");
                }
            }

            return casted;
        }

        #endregion
    }
}