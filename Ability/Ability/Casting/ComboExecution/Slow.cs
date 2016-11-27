namespace Ability.Casting.ComboExecution
{
    using System.Linq;
    using System.Threading;

    using Ability.ObjectManager;

    using Ensage;
    using Ensage.Common;
    using Ensage.Common.Extensions;

    internal class Slow
    {
        #region Public Methods and Operators

        public static bool Cast(Ability ability, Unit target, string name)
        {
            if (name == "pudge_rot" && Utils.SleepCheck("rotToggle"))
            {
                if (!ability.IsToggled)
                {
                    ability.ToggleAbility();
                    Utils.Sleep(500, "rotToggle");
                    return true;
                }

                return false;
            }

            if (name == "templar_assassin_psionic_trap")
            {
                var modifier = target.FindModifier("modifier_templar_assassin_trap_slow");
                if (modifier != null && modifier.RemainingTime > ability.GetHitDelay(target, name))
                {
                    return false;
                }

                if (TemplarAssasinUseTrap(target))
                {
                    return false;
                }

                var casted = ability.CastSkillShot(target, MyHeroInfo.Position, name);
                if (casted)
                {
                    DelayAction.Add(
                        new DelayActionItem(
                            (int)ability.GetHitDelay(target, name) * 1000 + 700, 
                            delegate { TemplarAssasinUseTrap(target); }, 
                            CancellationToken.None));
                }

                return casted;
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
                            ability.CastStun(
                                target, 
                                MyHeroInfo.Position, 
                                1, 
                                abilityName: name, 
                                useSleep: false, 
                                soulRing: SoulRing.Check(ability) ? MyAbilities.SoulRing : null);
                        });
                return true;
            }

            return ability.CastStun(
                target, 
                MyHeroInfo.Position, 
                1, 
                abilityName: name, 
                useSleep: false, 
                soulRing: SoulRing.Check(ability) ? MyAbilities.SoulRing : null);
        }

        public static bool TemplarAssasinUseTrap(Unit target)
        {
            if (!Utils.SleepCheck("Ability.TemplarTrap"))
            {
                return false;
            }

            var closestTrap =
                ObjectManager.GetEntities<Unit>()
                    .Where(
                        x =>
                        x.ClassID == ClassID.CDOTA_BaseNPC_Additive && x.Team == AbilityMain.Me.Team && x.IsAlive
                        && x.IsVisible && x.Distance2D(target) < 400
                        && x.FindSpell("templar_assassin_self_trap") != null
                        && x.FindSpell("templar_assassin_self_trap").CanBeCasted())
                    .MinOrDefault(x => x.Distance2D(target));
            if (closestTrap != null)
            {
                Utils.Sleep(250, "Ability.TemplarTrap");
            }

            return closestTrap != null && closestTrap.FindSpell("templar_assassin_self_trap").CastStun(target, 1);
        }

        #endregion
    }
}