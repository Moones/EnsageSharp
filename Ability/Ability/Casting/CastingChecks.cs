namespace Ability.Casting
{
    using System.Linq;

    using Ability.ObjectManager;
    using Ability.ObjectManager.Towers;

    using Ensage;
    using Ensage.Common;
    using Ensage.Common.AbilityInfo;
    using Ensage.Common.Extensions;

    internal class CastingChecks
    {
        #region Public Methods and Operators

        public static bool All(string name, Unit hero, Ability ability = null)
        {
            if ((name == "item_diffusal_blade" || name == "item_diffusal_blade_2")
                && hero.HasModifier("modifier_item_diffusal_blade_slow"))
            {
                return false;
            }

            if (name == "slardar_amplify_damage" && hero.HasModifier("modifier_slardar_amplify_damage"))
            {
                return false;
            }

            if (name == "bloodseeker_bloodrage" && hero.HasModifier("modifier_bloodseeker_bloodrage"))
            {
                return false;
            }

            if (name == "axe_battle_hunger" && hero.HasModifier("modifier_axe_battle_hunger"))
            {
                return false;
            }

            if (name == "bounty_hunter_track" && hero.HasModifier("modifier_bounty_hunter_track"))
            {
                return false;
            }

            if (name == "visage_soul_assumption" && ability != null)
            {
                var modif = AbilityMain.Me.FindModifier("modifier_visage_soul_assumption");
                if (modif == null || modif.StackCount < ability.GetAbilityData("stack_limit"))
                {
                    return false;
                }
            }

            return true;
        }

        public static bool Attacking(Hero target, Hero me)
        {
            var attacking = me.IsAttacking() && me.Distance2D(target) <= MyHeroInfo.AttackRange() + 150
                            && me.GetTurnTime(target.Position) < 0.1;
            return attacking;
        }

        public static bool Cast(Hero target, Ability ability)
        {
            return
                target.Spellbook.Spells.Any(x => x.IsInAbilityPhase && x.FindCastPoint() > ability.GetHitDelay(target))
                || (target.IsChanneling() && !target.GetChanneledAbility().Name.Contains("item"));
        }

        public static bool ChainStun(Ability ability, Hero target, string name)
        {
            var chainStun = Utils.ChainStun(target, ability.GetHitDelay(target), null, true, name);
            return chainStun;
        }

        public static bool Channel(Hero target)
        {
            return target.IsChanneling();
        }

        public static bool DisabledAlly(Hero target)
        {
            return target.IsStunned() || target.IsHexed() || target.IsRooted();
        }

        public static bool Invisible(Hero target)
        {
            return target.CanGoInvis() || target.IsInvisible();
        }

        public static bool Killsteal(Ability ability, Hero target, string name)
        {
            return AbilityDamage.CalculateDamage(ability, AbilityMain.Me, target) >= target.Health
                   && target.CanDie(name)
                   && (!target.IsInvul() || Utils.ChainStun(target, ability.GetHitDelay(target), null, true, name));
        }

        public static bool Purgable(Hero target)
        {
            return target.IsPurgable();
        }

        public static bool UnderTower(Ability ability, Hero target)
        {
            var closestTower = AllyTowers.GetClosest;
            return closestTower != null
                   && target.Distance2D(closestTower)
                   < (closestTower.AttackRange + closestTower.HullRadius + target.HullRadius);
        }

        #endregion
    }
}