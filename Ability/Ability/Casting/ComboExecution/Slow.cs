namespace Ability.Casting.ComboExecution
{
    using System.Linq;

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
                var modifier = target.Modifiers.FirstOrDefault(x => x.Name == "modifier_templar_assassin_trap_slow");
                if (modifier != null && modifier.RemainingTime > (ability.GetHitDelay(target, name)))
                {
                    return false;
                }
                var closestTrap =
                    ObjectMgr.GetEntities<Unit>()
                        .Where(
                            x =>
                            x.ClassID == ClassID.CDOTA_BaseNPC_Additive && x.Team == AbilityMain.Me.Team && x.IsAlive
                            && x.IsVisible && x.Distance2D(target) < 400
                            && x.FindSpell("templar_assassin_self_trap") != null
                            && x.FindSpell("templar_assassin_self_trap").CanBeCasted())
                        .MinOrDefault(x => x.Distance2D(target));
                if (closestTrap != null)
                {
                    return closestTrap.FindSpell("templar_assassin_self_trap").CastStun(target, 1);
                }
                return ability.CastSkillShot(target, MyHeroInfo.Position, name);
            }
            return ability.CastStun(target, 1, abilityName: name);
        }

        #endregion
    }
}