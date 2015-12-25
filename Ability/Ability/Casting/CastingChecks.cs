namespace Ability.Casting
{
    using System.Collections.Generic;
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

        public static bool All(string name, Unit hero, List<Modifier> modifiers, Ability ability = null)
        {
            if ((name == "item_diffusal_blade" || name == "item_diffusal_blade_2")
                && modifiers.Any(x => x.Name == "modifier_item_diffusal_blade_slow"))
            {
                return false;
            }
            if (name == "slardar_amplify_damage" && modifiers.Any(x => x.Name == "modifier_slardar_amplify_damage"))
            {
                return false;
            }
            if (name == "bloodseeker_bloodrage" && modifiers.Any(x => x.Name == "modifier_bloodseeker_bloodrage"))
            {
                return false;
            }
            if (name == "axe_battle_hunger" && modifiers.Any(x => x.Name == "modifier_axe_battle_hunger"))
            {
                return false;
            }
            if (name == "bounty_hunter_track" && modifiers.Any(x => x.Name == "modifier_bounty_hunter_track"))
            {
                return false;
            }
            if (name == "visage_soul_assumption" && ability != null)
            {
                var modif = AbilityMain.Me.Modifiers.FirstOrDefault(x => x.Name == "modifier_visage_soul_assumption");
                if (modif == null || modif.StackCount < ability.GetAbilityData("stack_limit"))
                {
                    return false;
                }
            }
            return true;
        }

        public static bool Attacking(Hero target, Hero me)
        {
            return me.IsAttacking() && me.Distance2D(target) <= MyHeroInfo.AttackRange() + 150
                   && me.GetTurnTime(target.Position) < 0.1;
        }

        public static bool Cast(Hero target, Ability ability)
        {
            return
                (target.Spellbook.Spells.Any(x => x.IsInAbilityPhase && x.FindCastPoint() > ability.GetHitDelay(target))
                 || (target.IsChanneling() && !target.GetChanneledAbility().Name.Contains("item")));
        }

        public static bool ChainStun(Ability ability, Hero target, string name)
        {
            return Utils.ChainStun(target, ability.GetHitDelay(target), null, true, name);
        }

        public static bool Channel(Hero target)
        {
            return target.IsChanneling();
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
            var closestTower = EnemyTowers.GetClosest;
            return (closestTower != null
                    && target.Distance2D(closestTower)
                    < (closestTower.AttackRange + closestTower.HullRadius + target.HullRadius));
        }

        #endregion
    }
}