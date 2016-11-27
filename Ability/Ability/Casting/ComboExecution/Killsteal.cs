namespace Ability.Casting.ComboExecution
{
    using System;
    using System.Linq;

    using Ability.AbilityMenu;
    using Ability.AbilityMenu.Menus.NukesMenu;
    using Ability.DamageCalculation;
    using Ability.Extensions;
    using Ability.ObjectManager;
    using Ability.Utilities;

    using Ensage;
    using Ensage.Common;
    using Ensage.Common.AbilityInfo;
    using Ensage.Common.Extensions;
    using Ensage.Common.Menu;

    /// <summary>
    ///     The kill steal.
    /// </summary>
    public class Killsteal
    {
        #region Fields

        /// <summary>
        ///     The target find sleeper.
        /// </summary>
        private readonly Sleeper targetFindSleeper;

        /// <summary>
        ///     The possible target.
        /// </summary>
        private Hero possibleTarget;

        #endregion

        #region Constructors and Destructors

        /// <summary>
        ///     Initializes a new instance of the <see cref="Killsteal" /> class.
        /// </summary>
        public Killsteal()
        {
            this.targetFindSleeper = new Sleeper();
        }

        #endregion

        #region Public Methods and Operators

        /// <summary>
        ///     The find target.
        /// </summary>
        /// <param name="enemyHeroes">
        ///     The enemy heroes.
        /// </param>
        /// <param name="me">
        ///     The me.
        /// </param>
        public void FindTarget(Hero[] enemyHeroes, Hero me)
        {
            if (this.targetFindSleeper.Sleeping)
            {
                return;
            }

            this.possibleTarget =
                enemyHeroes.Where(
                    hero =>
                    Utils.SleepCheck(hero.Handle + "KillSteal")
                    && Dictionaries.InDamageDictionary.ContainsKey(hero.Handle)
                    && Dictionaries.InDamageDictionary[hero.Handle] >= hero.Health)
                    .MinOrDefault(
                        hero =>
                        hero.Distance2D(MyHeroInfo.Position) + me.GetTurnTime(hero)
                        + Math.Min(Dictionaries.InDamageDictionary[hero.Handle], hero.Health));
        }

        /// <summary>
        ///     The try kill steal.
        /// </summary>
        /// <param name="me">
        ///     The me.
        /// </param>
        /// <param name="ping">
        ///     The ping.
        /// </param>
        /// <param name="enemyHeroes">
        ///     The enemy heroes.
        /// </param>
        /// <returns>
        ///     The <see cref="bool" />.
        /// </returns>
        public bool TryKillsteal(Hero me, float ping, Hero[] enemyHeroes)
        {
            if (this.possibleTarget == null || !this.possibleTarget.IsValid || !this.possibleTarget.CanDie()
                || !MyAbilities.NukesCombo.Any())
            {
                return false;
            }

            if (!(Dictionaries.InDamageDictionary[this.possibleTarget.Handle] >= this.possibleTarget.Health))
            {
                return false;
            }

            foreach (var ability in from ability in MyAbilities.NukesCombo.OrderBy(ComboOrder.GetAbilityOrder)
                                    where
                                        ability != null
                                        && (ability.CanBeCasted()
                                            || (ability.CanBeCasted(SoulRing.ManaGained) && SoulRing.Check(ability))
                                            || ability.CanInvoke())
                                        && (Utils.SleepCheck(ability.Handle.ToString())
                                            || (!ability.IsInAbilityPhase && ability.FindCastPoint() > 0))
                                    select ability)
            {
                var name = NameManager.Name(ability);
                var handleString = ability.Handle.ToString();
                if (Variables.EtherealHitTime
                    >= (Utils.TickCount + (ability.GetHitDelay(this.possibleTarget, name) * 1000)))
                {
                    continue;
                }

                if (name == "omniknight_purification")
                {
                    if (this.PurificationUsage(name, ability, me, ping, handleString))
                    {
                        return true;
                    }
                }

                if (this.possibleTarget.Health - Variables.DealtDamage <= 0
                    || this.possibleTarget.Health - Variables.DealtDamage
                    < Nukes.NukesMenuDictionary[name].Item(name + "minhealthslider").GetValue<Slider>().Value)
                {
                    Utils.Sleep(500, this.possibleTarget.Handle + "KillSteal");
                    Variables.DealtDamage = 0;
                    return false;
                }

                if (name == "item_urn_of_shadows" && this.possibleTarget.HasModifier("modifier_item_urn_damage"))
                {
                    continue;
                }

                if (!ability.CanHit(this.possibleTarget, MyHeroInfo.Position, name)
                    || (name == "zuus_thundergods_wrath"
                        && (1
                            + enemyHeroes.Count(
                                x =>
                                !x.Equals(this.possibleTarget)
                                && x.Health <= AbilityDamage.CalculateDamage(ability, me, x)))
                        < Nukes.NukesMenuDictionary[name].Item(name + "minenemykill").GetValue<Slider>().Value)
                    || !MainMenu.Menu.Item("nukesToggler").GetValue<AbilityToggler>().IsEnabled(name)
                    || !Nukes.NukesMenuDictionary[name].Item(name + "herotoggler")
                            .GetValue<HeroToggler>()
                            .IsEnabled(NameManager.Name(this.possibleTarget))
                    || !Nuke.Cast(ability, this.possibleTarget, name))
                {
                    return false;
                }

                if (Utils.SleepCheck(handleString))
                {
                    Variables.DealtDamage += AbilityDamage.CalculateDamage(ability, me, this.possibleTarget);
                    if (this.possibleTarget.Health - Variables.DealtDamage <= 0)
                    {
                        Utils.Sleep(
                            (ability.GetHitDelay(this.possibleTarget, name) * 1000) + 500, 
                            this.possibleTarget.Handle + "KillSteal");
                    }
                }

                var delay = ability.GetCastDelay(me, this.possibleTarget, abilityName: name) * 1000;
                if (name == "riki_blink_strike")
                {
                    Utils.Sleep(MyHeroInfo.AttackRate() * 1000, handleString);
                }

                if (name == "necrolyte_reapers_scythe")
                {
                    Utils.Sleep(delay + ping + 1500, "calculate");
                }

                if (name == "necrolyte_death_pulse")
                {
                    Utils.Sleep(delay + ping + 200, "calculate");
                }

                if (name == "item_ethereal_blade")
                {
                    // Variables.EtherealHitTime =
                    // (float)
                    // (Utils.TickCount + (me.GetTurnTime(this.possibleTarget) * 1000)
                    // + Prediction.CalculateReachTime(
                    // this.possibleTarget, 
                    // 1200, 
                    // this.possibleTarget.Position - MyHeroInfo.Position) + (ping * 2));
                    Variables.LastEtherealTarget = this.possibleTarget;
                    Variables.LastEtherealCastPosition = MyHeroInfo.Position;
                    Variables.LastEtherealCastTime =
                        (float)(Utils.TickCount + (me.GetTurnTime(this.possibleTarget) * 1000) + ping);
                    Utils.Sleep(
                        (me.GetTurnTime(this.possibleTarget) * 1000) + 100
                        + ((MyHeroInfo.Position.Distance2D(this.possibleTarget) / 1200) * 1000) + ping, 
                        "calculate");
                }

                if (name == "tusk_snowball")
                {
                    Utils.Sleep(
                        (me.GetTurnTime(this.possibleTarget) * 1000)
                        + ((MyHeroInfo.Position.Distance2D(this.possibleTarget) / 675) * 1000), 
                        "GlobalCasting");
                }

                var hitDelay = ability.GetHitDelay(this.possibleTarget, name);
                Utils.Sleep(delay, handleString);
                Utils.Sleep(
                    ability.GetCastDelay(me, this.possibleTarget, useCastPoint: false, abilityName: name) * 1000, 
                    "GlobalCasting");
                Utils.Sleep((hitDelay * 1000) + 200, "calculate");
                Utils.Sleep(
                    ability.GetCastDelay(me, this.possibleTarget, useCastPoint: false, abilityName: name) * 1000, 
                    "casting");
                Utils.Sleep(delay, "cancelorder");
                this.targetFindSleeper.Sleep((float)((hitDelay * 1000) + 400));
                return true;
            }

            return true;
        }

        #endregion

        #region Methods

        /// <summary>
        ///     The purification usage.
        /// </summary>
        /// <param name="name">
        ///     The name.
        /// </param>
        /// <param name="ability">
        ///     The ability.
        /// </param>
        /// <param name="me">
        ///     The me.
        /// </param>
        /// <param name="ping">
        ///     The ping.
        /// </param>
        /// <param name="handleString">
        ///     The handle string.
        /// </param>
        /// <returns>
        ///     The <see cref="bool" />.
        /// </returns>
        private bool PurificationUsage(string name, Ability ability, Hero me, float ping, string handleString)
        {
            if (!MainMenu.Menu.Item("nukesToggler").GetValue<AbilityToggler>().IsEnabled(name)
                || !Nukes.NukesMenuDictionary[name].Item(name + "herotoggler")
                        .GetValue<HeroToggler>()
                        .IsEnabled(NameManager.Name(this.possibleTarget))
                || (!(Variables.EtherealHitTime
                      < (Utils.TickCount + (ability.GetHitDelay(this.possibleTarget, name) * 1000))))
                || this.possibleTarget.Health
                <= Nukes.NukesMenuDictionary[name].Item(NameManager.Name(ability) + "minhealthslider")
                       .GetValue<Slider>()
                       .Value)
            {
                return false;
            }

            var target = FullCombo.FindPurificationTarget(this.possibleTarget);
            if (target == null
                || !(target.PredictedPosition().Distance2D(this.possibleTarget.PredictedPosition())
                     < ability.GetRadius(name))
                || !(target.PredictedPosition()
                         .Distance2D(
                             this.possibleTarget.PredictedPosition(ability.FindCastPoint(NameManager.Name(ability))))
                     < ability.GetRadius(name)))
            {
                return false;
            }

            if (!Nuke.Cast(ability, target, name))
            {
                return false;
            }

            Utils.Sleep(
                (ability.GetCastDelay(me, this.possibleTarget, abilityName: name) * 1000) + ping + 100, 
                handleString);
            Utils.Sleep(ability.GetCastDelay(me, this.possibleTarget, abilityName: name) * 1000, "GlobalCasting");
            Utils.Sleep(ability.GetHitDelay(this.possibleTarget, name) * 1000, "calculate");
            Utils.Sleep(
                ability.GetCastDelay(me, this.possibleTarget, useCastPoint: false, abilityName: name) * 1000, 
                "casting");
            Utils.Sleep(ability.GetCastDelay(me, this.possibleTarget, abilityName: name) * 1000, "cancelorder");
            return true;
        }

        #endregion
    }
}