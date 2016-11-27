namespace Ability.Casting.ComboExecution
{
    using System.Linq;
    using System.Threading;

    using Ability.AbilityMenu;
    using Ability.AbilityMenu.Menus.BuffsMenu;
    using Ability.AbilityMenu.Menus.DisablesMenu;
    using Ability.AbilityMenu.Menus.HarrasesMenu;
    using Ability.AbilityMenu.Menus.NukesMenu;
    using Ability.AbilityMenu.Menus.SilencesMenu;
    using Ability.AbilityMenu.Menus.SlowsMenu;
    using Ability.AbilityMenu.Menus.SpecialsMenu;
    using Ability.DamageCalculation;
    using Ability.Extensions;
    using Ability.ObjectManager;

    using Ensage;
    using Ensage.Common;
    using Ensage.Common.AbilityInfo;
    using Ensage.Common.Extensions;
    using Ensage.Common.Menu;

    /// <summary>
    ///     The auto usage.
    /// </summary>
    public class AutoUsage
    {
        #region Constructors and Destructors

        /// <summary>
        ///     Initializes a new instance of the <see cref="AutoUsage" /> class.
        /// </summary>
        public AutoUsage()
        {
        }

        #endregion

        #region Public Methods and Operators

        /// <summary>
        ///     The try.
        /// </summary>
        /// <param name="enemyHero">
        ///     The enemy hero.
        /// </param>
        /// <param name="enemyHeroes">
        ///     The enemy heroes.
        /// </param>
        /// <param name="mineMissingHp">
        ///     The mine missing health.
        /// </param>
        /// <param name="ping">
        ///     The ping.
        /// </param>
        /// <param name="me">
        ///     The me.
        /// </param>
        /// <param name="mana">
        ///     The mana.
        /// </param>
        /// <returns>
        ///     The <see cref="bool" />.
        /// </returns>
        public bool Try(Hero enemyHero, Hero[] enemyHeroes, float mineMissingHp, float ping, Hero me, float mana)
        {
            var hero = enemyHero;
            var heroName = NameManager.Name(hero);
            var heroDistance = MyHeroInfo.Position.Distance2D(hero);
            if (!Utils.SleepCheck("GlobalCasting") || !Utils.SleepCheck("casting"))
            {
                return false;
            }

            Ability tuskSnowBall;
            if (me.ClassID == ClassID.CDOTA_Unit_Hero_Tusk && Dictionaries.InDamageDictionary.ContainsKey(hero.Handle)
                && (Dictionaries.OutDamageDictionary[hero.Handle] + Dictionaries.InDamageDictionary[hero.Handle])
                >= enemyHero.Health && Utils.SleepCheck("tusk_snowball") && heroDistance <= 1350
                && me.FindSpell("tusk_snowball", out tuskSnowBall).CanBeCasted())
            {
                if (!Nuke.Cast(tuskSnowBall, hero, "tusk_snowball"))
                {
                    return false;
                }

                DelayAction.Add(
                    new DelayActionItem(300, () => { AbilityMain.LaunchSnowball(); }, CancellationToken.None));
                Utils.Sleep(100 + (me.GetTurnTime(hero) * 1000) + ping, "tusk_snowball");
                var delay = (me.GetTurnTime(hero) * 1000) + ((heroDistance / 675) * 1000);
                Utils.Sleep(delay + 100, "GlobalCasting");
                Utils.Sleep(delay + 100, "calculate");
                Utils.Sleep(delay, "cancelorder");
                return true;
            }

            foreach (var data in
                MyAbilities.OffensiveAbilities.Where(
                    x =>
                    x.Value.IsValid
                    && (x.Value.CanBeCasted() || (x.Value.CanBeCasted(SoulRing.ManaGained) && SoulRing.Check(x.Value))
                        || x.Value.CanInvoke())
                    && ((x.Value is Item && me.CanUseItems()) || (!(x.Value is Item) && me.CanCast()))
                    && (Utils.SleepCheck(x.Value.Handle.ToString())
                        || (!x.Value.IsInAbilityPhase && x.Value.FindCastPoint() > 0)))
                    .OrderBy(x => ComboOrder.GetAbilityOrder(x.Value)))
            {
                var ability = data.Value;
                var name = NameManager.Name(ability);
                var category = data.Key.Substring(name.Length);
                var handleString = ability.Handle.ToString();
                if (category != "buff" && hero.IsMagicImmune() && ability.ImmunityType != (ImmunityType)3)
                {
                    continue;
                }

                if (!CastingChecks.All(name, hero))
                {
                    continue;
                }

                var delay = (ability.GetCastDelay(me, hero, true, abilityName: name) * 1000) + 100;
                var canHit = ability.CanHit(hero, MyHeroInfo.Position, name);
                if (name == "omniknight_purification")
                {
                    if (!MainMenu.Menu.Item("Ability#.EnableAutoKillSteal").GetValue<bool>())
                    {
                        continue;
                    }

                    if (!(Nukes.NukesMenuDictionary[name].Item(name + "minManaCheck").GetValue<Slider>().Value < mana)
                        || !MainMenu.Menu.Item("nukesToggler").GetValue<AbilityToggler>().IsEnabled(name)
                        || !Nukes.NukesMenuDictionary[name].Item(name + "herotoggler")
                                .GetValue<HeroToggler>()
                                .IsEnabled(heroName)
                        || (!(Variables.EtherealHitTime < (Utils.TickCount + (ability.GetHitDelay(hero, name) * 1000))))
                        || hero.Health
                        <= Nukes.NukesMenuDictionary[name].Item(NameManager.Name(ability) + "minhealthslider")
                               .GetValue<Slider>()
                               .Value || !CastingChecks.Killsteal(ability, hero, name))
                    {
                        return false;
                    }

                    var target = FullCombo.FindPurificationTarget(hero);
                    if (target == null || !ability.CanHit(target, MyHeroInfo.Position, name)
                        || !(target.PredictedPosition().Distance2D(hero.PredictedPosition()) < ability.GetRadius(name))
                        || !(target.PredictedPosition()
                                 .Distance2D(hero.PredictedPosition(ability.FindCastPoint(NameManager.Name(ability))))
                             < ability.GetRadius(name)))
                    {
                        return false;
                    }

                    if (!Nuke.Cast(ability, target, name))
                    {
                        return false;
                    }

                    Utils.Sleep((ability.GetCastDelay(me, hero, abilityName: name) * 1000) + ping + 100, handleString);
                    Utils.Sleep(delay, "GlobalCasting");
                    Utils.Sleep(delay, "cancelorder");
                    return true;
                }

                if (category == "nuke" && !MainMenu.Menu.Item("Ability#.EnableAutoKillSteal").GetValue<bool>())
                {
                    continue;
                }

                if (name == "item_urn_of_shadows" && hero.HasModifier("modifier_item_urn_damage"))
                {
                    continue;
                }

                if (category == "nuke" && Nukes.NukesMenuDictionary[name].Item(name + "ondisabletoggler") != null
                    && Nukes.NukesMenuDictionary[name].Item(name + "ondisabletoggler")
                           .GetValue<HeroToggler>()
                           .IsEnabled(heroName)
                    && Nukes.NukesMenuDictionary[name].Item(name + "minManaCheck").GetValue<Slider>().Value < mana
                    && MainMenu.Menu.Item("nukesToggler").GetValue<AbilityToggler>().IsEnabled(name) && canHit
                    && (Variables.EtherealHitTime < (Utils.TickCount + (ability.GetHitDelay(hero, name) * 1000)))
                    && hero.Health
                    > Nukes.NukesMenuDictionary[name].Item(NameManager.Name(ability) + "minhealthslider")
                          .GetValue<Slider>()
                          .Value)
                {
                    var duration = Utils.DisableDuration(hero);
                    if (duration > 0
                        && duration
                        > ability.GetHitDelay(hero, name)
                        - ((ability.EndRadius() + hero.HullRadius) / hero.MovementSpeed) - (Game.Ping / 1000)
                        && (!hero.IsInvul() || duration < ability.GetHitDelay(hero, name))
                        && Nuke.Cast(ability, hero, name, true))
                    {
                        if (ability.ChannelTime(name) > 0)
                        {
                            Utils.Sleep(delay + ((ability.ChannelTime(name) * 1000) / 3), "cancelorder");
                            Utils.Sleep(delay + ((ability.ChannelTime(name) * 1000) / 3), "casting");
                        }

                        Utils.Sleep(
                            (ability.GetCastDelay(me, hero, abilityName: name) * 1000) + ping + 100, 
                            handleString);
                        Utils.Sleep(delay, "GlobalCasting");
                        Utils.Sleep(delay, "cancelorder");
                        return true;
                    }
                }

                if (category == "nuke" && Utils.SleepCheck(hero.Handle + "KillSteal")
                    && Nukes.NukesMenuDictionary[name].Item(name + "minManaCheck").GetValue<Slider>().Value < mana
                    && MainMenu.Menu.Item("nukesToggler").GetValue<AbilityToggler>().IsEnabled(name)
                    && Nukes.NukesMenuDictionary[name].Item(name + "herotoggler")
                           .GetValue<HeroToggler>()
                           .IsEnabled(heroName) && canHit
                    && (Variables.EtherealHitTime < (Utils.TickCount + (ability.GetHitDelay(hero, name) * 1000)))
                    && hero.Health
                    > Nukes.NukesMenuDictionary[name].Item(NameManager.Name(ability) + "minhealthslider")
                          .GetValue<Slider>()
                          .Value && CastingChecks.Killsteal(ability, hero, name)
                    && (name != "zuus_thundergods_wrath"
                        || (1
                            + enemyHeroes.Count(
                                x => !x.Equals(hero) && x.Health <= AbilityDamage.CalculateDamage(ability, me, x)))
                        >= Nukes.NukesMenuDictionary[name].Item(name + "minenemykill").GetValue<Slider>().Value))
                {
                    if ((hero.Health - Variables.DealtDamage) <= 0
                        || (hero.Health - Variables.DealtDamage)
                        < Nukes.NukesMenuDictionary[name].Item(name + "minhealthslider").GetValue<Slider>().Value)
                    {
                        Variables.DealtDamage = 0;
                        return false;
                    }

                    if (Nuke.Cast(ability, hero, name))
                    {
                        if (Utils.SleepCheck(handleString)
                            && ability.GetCastDelay(AbilityMain.Me, hero, true) * 1000 - Game.Ping > 0.1)
                        {
                            DelayAction.Add(
                                new DelayActionItem(
                                    (int)(ability.GetCastDelay(AbilityMain.Me, hero, true) * 1000 - Game.Ping - 50), 
                                    () =>
                                        {
                                            if (!CastingChecks.Killsteal(ability, hero, name) || !hero.IsAlive
                                                || hero.Health <= 0)
                                            {
                                                AbilityMain.Me.Stop();
                                            }
                                        }, 
                                    CancellationToken.None));
                            Utils.Sleep(ability.GetHitDelay(hero, name) * 1000 + 500, hero.Handle + "KillSteal");
                        }

                        if (name == "riki_blink_strike")
                        {
                            Utils.Sleep(MyHeroInfo.AttackRate() * 1000 + ping + 100, handleString);
                        }

                        if (name == "item_ethereal_blade")
                        {
                            Variables.LastEtherealTarget = hero;
                            Variables.LastEtherealCastPosition = me.NetworkPosition;
                            Variables.LastEtherealCastTime =
                                (float)(Utils.TickCount + (me.GetTurnTime(hero) * 1000) + ping);
                            Utils.Sleep(
                                me.GetTurnTime(hero) * 1000 + 100 + (MyHeroInfo.Position.Distance2D(hero) / 1200) * 1000
                                + ping, 
                                "calculate");
                        }

                        if (ability.ChannelTime(name) > 0)
                        {
                            Utils.Sleep(delay + (ability.ChannelTime(name) * 1000) / 3, "cancelorder");
                            Utils.Sleep(delay + (ability.ChannelTime(name) * 1000) / 3, "casting");
                        }

                        Utils.Sleep(ability.GetCastDelay(me, hero, abilityName: name) * 1000 + ping + 100, handleString);
                        Utils.Sleep(delay, "GlobalCasting");
                        Utils.Sleep(delay, "cancelorder");

                        // Utils.Sleep(ping, "killsteal");
                        return true;
                    }

                    continue;
                }

                if (Dictionaries.HitDamageDictionary.ContainsKey(hero.Handle)
                    && Dictionaries.HitDamageDictionary[hero.Handle] * 1.5 >= hero.Health
                    && hero.Distance2D(MyHeroInfo.Position) <= me.GetAttackRange() + 150)
                {
                    continue;
                }

                if (category == "disable"
                    && Disables.DisablesMenuDictionary[name].Item(name + "minManaCheck").GetValue<Slider>().Value < mana
                    && MainMenu.Menu.Item("disablesToggler").GetValue<AbilityToggler>().IsEnabled(name))
                {
                    if (
                        Disables.DisablesMenuDictionary[name].Item(name + "onsighttoggler")
                            .GetValue<HeroToggler>()
                            .IsEnabled(heroName) && canHit && Disable.Cast(ability, hero, name))
                    {
                        if (ability.ChannelTime(name) > 0)
                        {
                            Utils.Sleep(delay + (ability.ChannelTime(name) * 1000) / 3, "cancelorder");
                            Utils.Sleep(delay + (ability.ChannelTime(name) * 1000) / 3, "casting");
                        }

                        Utils.Sleep(ability.GetCastDelay(me, hero, abilityName: name) * 1000 + ping + 100, handleString);
                        Utils.Sleep(delay, "GlobalCasting");
                        Utils.Sleep(delay, "cancelorder");
                        return true;
                    }

                    if (
                        Disables.DisablesMenuDictionary[name].Item(name + "onchainstuntoggler")
                            .GetValue<HeroToggler>()
                            .IsEnabled(heroName) && canHit && CastingChecks.ChainStun(ability, hero, name)
                        && Disable.Cast(ability, hero, name))
                    {
                        if (ability.ChannelTime(name) > 0)
                        {
                            Utils.Sleep(delay + (ability.ChannelTime(name) * 1000) / 3, "cancelorder");
                            Utils.Sleep(delay + (ability.ChannelTime(name) * 1000) / 3, "casting");
                        }

                        Utils.Sleep(ability.GetCastDelay(me, hero, abilityName: name) * 1000 + ping + 100, handleString);
                        Utils.Sleep(delay, "GlobalCasting");
                        Utils.Sleep(delay, "cancelorder");
                        return true;
                    }

                    if (CastingChecks.UnderTower(ability, hero)
                        && Disables.DisablesMenuDictionary[name].Item(name + "undertowertoggler")
                               .GetValue<HeroToggler>()
                               .IsEnabled(heroName) && canHit && Disable.Cast(ability, hero, name))
                    {
                        if (ability.ChannelTime(name) > 0)
                        {
                            Utils.Sleep(delay + (ability.ChannelTime(name) * 1000) / 3, "cancelorder");
                            Utils.Sleep(delay + (ability.ChannelTime(name) * 1000) / 3, "casting");
                        }

                        Utils.Sleep(ability.GetCastDelay(me, hero, abilityName: name) * 1000 + ping + 100, handleString);
                        Utils.Sleep(delay, "GlobalCasting");
                        Utils.Sleep(delay, "cancelorder");
                        return true;
                    }

                    if (
                        Disables.DisablesMenuDictionary[name].Item(name + "onchanneltoggler")
                            .GetValue<HeroToggler>()
                            .IsEnabled(heroName) && CastingChecks.Channel(hero) && canHit
                        && Disable.Cast(ability, hero, name))
                    {
                        if (ability.ChannelTime(name) > 0)
                        {
                            Utils.Sleep(delay + (ability.ChannelTime(name) * 1000) / 3, "cancelorder");
                            Utils.Sleep(delay + (ability.ChannelTime(name) * 1000) / 3, "casting");
                        }

                        Utils.Sleep(ability.GetCastDelay(me, hero, abilityName: name) * 1000 + ping + 100, handleString);
                        Utils.Sleep(delay, "GlobalCasting");
                        Utils.Sleep(delay, "cancelorder");
                        return true;
                    }

                    if (
                        Disables.DisablesMenuDictionary[name].Item(name + "oncasttoggler")
                            .GetValue<HeroToggler>()
                            .IsEnabled(heroName) && canHit && CastingChecks.Cast(hero, ability)
                        && Disable.Cast(ability, hero, name))
                    {
                        if (ability.ChannelTime(name) > 0)
                        {
                            Utils.Sleep(delay + (ability.ChannelTime(name) * 1000) / 3, "cancelorder");
                            Utils.Sleep(delay + (ability.ChannelTime(name) * 1000) / 3, "casting");
                        }

                        Utils.Sleep(ability.GetCastDelay(me, hero, abilityName: name) * 1000 + ping + 100, handleString);
                        Utils.Sleep(delay, "GlobalCasting");
                        Utils.Sleep(delay, "cancelorder");
                        return true;
                    }

                    continue;
                }

                if (Dictionaries.InDamageDictionary.ContainsKey(hero.Handle)
                    && Dictionaries.InDamageDictionary[hero.Handle] >= hero.Health
                    && MainMenu.Menu.Item("Ability#.EnableAutoKillSteal").GetValue<bool>())
                {
                    continue;
                }

                if (category == "slow"
                    && Slows.SlowsMenuDictionary[name].Item(name + "minManaCheck").GetValue<Slider>().Value < mana
                    && MainMenu.Menu.Item("slowsToggler").GetValue<AbilityToggler>().IsEnabled(name))
                {
                    if (
                        Slows.SlowsMenuDictionary[name].Item(name + "onsighttoggler")
                            .GetValue<HeroToggler>()
                            .IsEnabled(heroName) && canHit && Slow.Cast(ability, hero, name))
                    {
                        if (ability.ChannelTime(name) > 0)
                        {
                            Utils.Sleep(delay + (ability.ChannelTime(name) * 1000) / 3, "cancelorder");
                            Utils.Sleep(delay + (ability.ChannelTime(name) * 1000) / 3, "casting");
                        }

                        Utils.Sleep(ability.GetCastDelay(me, hero, abilityName: name) * 1000 + ping + 100, handleString);
                        if (name == "item_ethereal_blade")
                        {
                            Utils.Sleep(
                                me.GetTurnTime(hero) * 1000
                                + Prediction.CalculateReachTime(hero, 1200, hero.Position - MyHeroInfo.Position), 
                                "casting");
                            Utils.Sleep(
                                me.GetTurnTime(hero) * 1000 + 100 + (MyHeroInfo.Position.Distance2D(hero) / 1200) * 1000
                                + ping, 
                                "calculate");
                        }

                        Utils.Sleep(delay, "GlobalCasting");
                        Utils.Sleep(delay, "cancelorder");
                        return true;
                    }

                    if (
                        Slows.SlowsMenuDictionary[name].Item(name + "onchainstuntoggler")
                            .GetValue<HeroToggler>()
                            .IsEnabled(heroName) && canHit && CastingChecks.ChainStun(ability, hero, name)
                        && Slow.Cast(ability, hero, name))
                    {
                        if (ability.ChannelTime(name) > 0)
                        {
                            Utils.Sleep(delay + (ability.ChannelTime(name) * 1000) / 3, "cancelorder");
                            Utils.Sleep(delay + (ability.ChannelTime(name) * 1000) / 3, "casting");
                        }

                        if (name == "item_ethereal_blade")
                        {
                            Utils.Sleep(
                                me.GetTurnTime(hero) * 1000
                                + Prediction.CalculateReachTime(hero, 1200, hero.Position - MyHeroInfo.Position), 
                                "casting");
                            Utils.Sleep(
                                me.GetTurnTime(hero) * 1000 + 100 + (MyHeroInfo.Position.Distance2D(hero) / 1200) * 1000
                                + ping, 
                                "calculate");
                        }

                        Utils.Sleep(ability.GetCastDelay(me, hero, abilityName: name) * 1000 + ping + 100, handleString);
                        Utils.Sleep(delay, "GlobalCasting");
                        Utils.Sleep(delay, "cancelorder");
                        return true;
                    }

                    if (CastingChecks.UnderTower(ability, hero)
                        && Slows.SlowsMenuDictionary[name].Item(name + "undertowertoggler")
                               .GetValue<HeroToggler>()
                               .IsEnabled(heroName) && canHit && Slow.Cast(ability, hero, name))
                    {
                        if (ability.ChannelTime(name) > 0)
                        {
                            Utils.Sleep(delay + (ability.ChannelTime(name) * 1000) / 3, "cancelorder");
                            Utils.Sleep(delay + (ability.ChannelTime(name) * 1000) / 3, "casting");
                        }

                        if (name == "item_ethereal_blade")
                        {
                            Utils.Sleep(
                                me.GetTurnTime(hero) * 1000
                                + Prediction.CalculateReachTime(hero, 1200, hero.Position - MyHeroInfo.Position), 
                                "casting");
                            Utils.Sleep(
                                me.GetTurnTime(hero) * 1000 + 100 + (MyHeroInfo.Position.Distance2D(hero) / 1200) * 1000
                                + ping, 
                                "calculate");
                        }

                        Utils.Sleep(ability.GetCastDelay(me, hero, abilityName: name) * 1000 + ping + 100, handleString);
                        Utils.Sleep(delay, "GlobalCasting");
                        Utils.Sleep(delay, "cancelorder");
                        return true;
                    }

                    continue;
                }

                if (category == "special"
                    && (name == "rubick_spell_steal"
                        || Specials.SpecialsMenuDictionary[name].Item(name + "minManaCheck").GetValue<Slider>().Value
                        < mana) && MainMenu.Menu.Item("specialsToggler").GetValue<AbilityToggler>().IsEnabled(name))
                {
                    if (name == "rubick_spell_steal")
                    {
                        var spell4 = me.Spellbook.Spell4;
                        if ((!spell4.CanBeCasted()) && Rubick.LastCasted(heroName) != null
                            && NameManager.Name(spell4) != NameManager.Name(Rubick.LastCasted(heroName))
                            && Specials.SpecialsMenuDictionary[name].Item(heroName)
                                   .GetValue<AbilityToggler>()
                                   .IsEnabled(NameManager.Name(Rubick.LastCasted(heroName))) && canHit
                            && Special.Cast(ability, hero, name))
                        {
                            Utils.Sleep(
                                ability.GetCastDelay(me, hero, abilityName: name) * 1000 + ping + 100, 
                                handleString);
                            Utils.Sleep(delay, "GlobalCasting");
                            Utils.Sleep(delay, "cancelorder");
                            return true;
                        }

                        continue;
                    }

                    if (
                        Specials.SpecialsMenuDictionary[name].Item(name + "onsighttoggler")
                            .GetValue<HeroToggler>()
                            .IsEnabled(heroName) && canHit && Special.Cast(ability, hero, name))
                    {
                        if (ability.ChannelTime(name) > 0)
                        {
                            Utils.Sleep(delay + (ability.ChannelTime(name) * 1000) / 3, "cancelorder");
                            Utils.Sleep(delay + (ability.ChannelTime(name) * 1000) / 3, "casting");
                        }

                        Utils.Sleep(ability.GetCastDelay(me, hero, abilityName: name) * 1000 + ping + 100, handleString);
                        Utils.Sleep(delay, "GlobalCasting");
                        Utils.Sleep(delay, "cancelorder");
                        return true;
                    }

                    if (Specials.SpecialsMenuDictionary[name].Item(name + "onpurgetoggler") != null
                        && Specials.SpecialsMenuDictionary[name].Item(name + "onpurgetoggler")
                               .GetValue<HeroToggler>()
                               .IsEnabled(heroName) && canHit && CastingChecks.Purgable(hero)
                        && Special.Cast(ability, hero, name))
                    {
                        if (ability.ChannelTime(name) > 0)
                        {
                            Utils.Sleep(delay + (ability.ChannelTime(name) * 1000) / 3, "cancelorder");
                            Utils.Sleep(delay + (ability.ChannelTime(name) * 1000) / 3, "casting");
                        }

                        Utils.Sleep(ability.GetCastDelay(me, hero, abilityName: name) * 1000 + ping + 100, handleString);
                        Utils.Sleep(delay, "GlobalCasting");
                        Utils.Sleep(delay, "cancelorder");
                        return true;
                    }

                    if (Specials.SpecialsMenuDictionary[name].Item(name + "oninvistoggler") != null
                        && Specials.SpecialsMenuDictionary[name].Item(name + "oninvistoggler")
                               .GetValue<HeroToggler>()
                               .IsEnabled(heroName) && canHit && CastingChecks.Invisible(hero)
                        && Special.Cast(ability, hero, name))
                    {
                        if (ability.ChannelTime(name) > 0)
                        {
                            Utils.Sleep(delay + (ability.ChannelTime(name) * 1000) / 3, "cancelorder");
                            Utils.Sleep(delay + (ability.ChannelTime(name) * 1000) / 3, "casting");
                        }

                        Utils.Sleep(ability.GetCastDelay(me, hero, abilityName: name) * 1000 + ping + 100, handleString);
                        Utils.Sleep(delay, "GlobalCasting");
                        Utils.Sleep(delay, "cancelorder");
                        return true;
                    }

                    if (Specials.SpecialsMenuDictionary[name].Item(name + "onattacktoggler") != null
                        && Specials.SpecialsMenuDictionary[name].Item(name + "onattacktoggler")
                               .GetValue<HeroToggler>()
                               .IsEnabled(heroName) && canHit && CastingChecks.Attacking(hero, me)
                        && Special.Cast(ability, hero, name))
                    {
                        Utils.Sleep(ability.GetCastDelay(me, hero, abilityName: name) * 1000 + ping + 100, handleString);
                        Utils.Sleep(delay, "GlobalCasting");
                        Utils.Sleep(delay, "cancelorder");
                        return true;
                    }

                    continue;
                }

                if (category == "buff"
                    && Buffs.BuffsMenuDictionary[name].Item(name + "minManaCheck").GetValue<Slider>().Value < mana
                    && MainMenu.Menu.Item("buffsToggler").GetValue<AbilityToggler>().IsEnabled(name))
                {
                    if (name == "item_armlet")
                    {
                        if (ability.IsToggled
                            && Buffs.BuffsMenuDictionary[name].Item(name + "belowhpslider").GetValue<Slider>().Value
                            < me.Health)
                        {
                            continue;
                        }

                        if (Buffs.BuffsMenuDictionary[name].Item(name + "alwaystoggle").GetValue<bool>()
                            && Buffs.BuffsMenuDictionary[name].Item(name + "belowhpslider").GetValue<Slider>().Value
                            > me.Health && AbilityMain.Me.Distance2D(Base.Position()) > 1300
                            && Buff.Cast(ability, hero, me, name, true))
                        {
                            Utils.Sleep(
                                Buffs.BuffsMenuDictionary[name].Item(name + "armletdelay").GetValue<Slider>().Value, 
                                handleString);
                            Utils.Sleep(ping + 200, "GlobalCasting");
                            Utils.Sleep(ping + 200, "casting");
                            return true;
                        }
                    }

                    if (name == "item_satanic"
                        && Buffs.BuffsMenuDictionary[name].Item(name + "missinghpslider").GetValue<Slider>().Value
                        > mineMissingHp
                        && Buffs.BuffsMenuDictionary[name].Item(name + "belowhpslider").GetValue<Slider>().Value
                        < me.Health)
                    {
                        continue;
                    }

                    if (
                        Buffs.BuffsMenuDictionary[name].Item(name + "onsighttoggler")
                            .GetValue<HeroToggler>()
                            .IsEnabled(heroName) && canHit && Buff.Cast(ability, hero, me, name))
                    {
                        if (name == "item_armlet")
                        {
                            Utils.Sleep(
                                Buffs.BuffsMenuDictionary[name].Item(name + "armletdelay").GetValue<Slider>().Value, 
                                handleString);
                            Utils.Sleep(ping + 200, "GlobalCasting");
                            Utils.Sleep(ping + 200, "casting");
                        }
                        else
                        {
                            Utils.Sleep(
                                ability.GetCastDelay(me, hero, abilityName: name) * 1000 + ping + 100, 
                                handleString);
                        }

                        if (ability.ChannelTime(name) > 0)
                        {
                            Utils.Sleep(delay + (ability.ChannelTime(name) * 1000) / 3, "cancelorder");
                            Utils.Sleep(delay + (ability.ChannelTime(name) * 1000) / 3, "casting");
                        }

                        Utils.Sleep(delay, "GlobalCasting");
                        Utils.Sleep(delay, "cancelorder");
                        return true;
                    }

                    if (
                        Buffs.BuffsMenuDictionary[name].Item(name + "onattacktoggler")
                            .GetValue<HeroToggler>()
                            .IsEnabled(heroName) && canHit && CastingChecks.Attacking(hero, me)
                        && Buff.Cast(ability, hero, me, name))
                    {
                        if (name == "item_armlet")
                        {
                            Utils.Sleep(
                                Buffs.BuffsMenuDictionary[name].Item(name + "armletdelay").GetValue<Slider>().Value, 
                                handleString);
                            Utils.Sleep(ping + 200, "GlobalCasting");
                            Utils.Sleep(ping + 200, "casting");
                        }
                        else
                        {
                            Utils.Sleep(
                                ability.GetCastDelay(me, hero, abilityName: name) * 1000 + ping + 100, 
                                handleString);
                        }

                        if (ability.ChannelTime(name) > 0)
                        {
                            Utils.Sleep(delay + (ability.ChannelTime(name) * 1000) / 3, "cancelorder");
                            Utils.Sleep(delay + (ability.ChannelTime(name) * 1000) / 3, "casting");
                        }

                        Utils.Sleep(delay, "GlobalCasting");
                        Utils.Sleep(delay, "cancelorder");
                        return true;
                    }

                    continue;
                }

                if (category == "silence"
                    && Silences.SilencesMenuDictionary[name].Item(name + "minManaCheck").GetValue<Slider>().Value < mana
                    && MainMenu.Menu.Item("silencesToggler").GetValue<AbilityToggler>().IsEnabled(name))
                {
                    if (
                        Silences.SilencesMenuDictionary[name].Item(name + "onsighttoggler")
                            .GetValue<HeroToggler>()
                            .IsEnabled(heroName) && canHit && Silence.Cast(ability, hero, name))
                    {
                        if (ability.ChannelTime(name) > 0)
                        {
                            Utils.Sleep(delay + (ability.ChannelTime(name) * 1000) / 3, "cancelorder");
                            Utils.Sleep(delay + (ability.ChannelTime(name) * 1000) / 3, "casting");
                        }

                        Utils.Sleep(ability.GetCastDelay(me, hero, abilityName: name) * 1000 + ping + 100, handleString);
                        Utils.Sleep(delay, "GlobalCasting");
                        Utils.Sleep(delay, "cancelorder");
                        return true;
                    }

                    if (
                        Silences.SilencesMenuDictionary[name].Item(name + "oncasttoggler")
                            .GetValue<HeroToggler>()
                            .IsEnabled(heroName) && canHit && CastingChecks.Cast(hero, ability)
                        && Silence.Cast(ability, hero, name))
                    {
                        if (ability.ChannelTime(name) > 0)
                        {
                            Utils.Sleep(delay + (ability.ChannelTime(name) * 1000) / 3, "cancelorder");
                            Utils.Sleep(delay + (ability.ChannelTime(name) * 1000) / 3, "casting");
                        }

                        Utils.Sleep(ability.GetCastDelay(me, hero, abilityName: name) * 1000 + ping + 100, handleString);
                        Utils.Sleep(delay, "GlobalCasting");
                        Utils.Sleep(delay, "cancelorder");
                        return true;
                    }

                    if (
                        Silences.SilencesMenuDictionary[name].Item(name + "oninvistoggler")
                            .GetValue<HeroToggler>()
                            .IsEnabled(heroName) && canHit && CastingChecks.Invisible(hero)
                        && Silence.Cast(ability, hero, name))
                    {
                        if (ability.ChannelTime(name) > 0)
                        {
                            Utils.Sleep(delay + (ability.ChannelTime(name) * 1000) / 3, "cancelorder");
                            Utils.Sleep(delay + (ability.ChannelTime(name) * 1000) / 3, "casting");
                        }

                        Utils.Sleep(ability.GetCastDelay(me, hero, abilityName: name) * 1000 + ping + 100, handleString);
                        Utils.Sleep(delay, "GlobalCasting");
                        Utils.Sleep(delay, "cancelorder");
                        return true;
                    }

                    continue;
                }

                if (category == "harras"
                    && Harrases.HarrasesMenuDictionary[name].Item(name + "minManaCheck").GetValue<Slider>().Value < mana
                    && MainMenu.Menu.Item("harrasesToggler").GetValue<AbilityToggler>().IsEnabled(name))
                {
                    if (
                        Harrases.HarrasesMenuDictionary[name].Item(name + "onsighttoggler")
                            .GetValue<HeroToggler>()
                            .IsEnabled(heroName) && canHit && Harras.Cast(ability, hero, name))
                    {
                        if (ability.ChannelTime(name) > 0)
                        {
                            Utils.Sleep(delay + (ability.ChannelTime(name) * 1000) / 3, "cancelorder");
                            Utils.Sleep(delay + (ability.ChannelTime(name) * 1000) / 3, "casting");
                        }

                        Utils.Sleep(ability.GetCastDelay(me, hero, abilityName: name) * 1000 + ping + 100, handleString);
                        Utils.Sleep(delay, "GlobalCasting");
                        Utils.Sleep(delay, "cancelorder");
                        return true;
                    }

                    if (
                        Harrases.HarrasesMenuDictionary[name].Item(name + "onattacktoggler")
                            .GetValue<HeroToggler>()
                            .IsEnabled(heroName) && canHit && CastingChecks.Attacking(hero, me)
                        && Harras.Cast(ability, hero, name))
                    {
                        if (ability.ChannelTime(name) > 0)
                        {
                            Utils.Sleep(delay + (ability.ChannelTime(name) * 1000) / 3, "cancelorder");
                            Utils.Sleep(delay + (ability.ChannelTime(name) * 1000) / 3, "casting");
                        }

                        Utils.Sleep(ability.GetCastDelay(me, hero, abilityName: name) * 1000 + ping + 100, handleString);
                        Utils.Sleep(delay, "GlobalCasting");
                        Utils.Sleep(delay, "cancelorder");
                        return true;
                    }
                }
            }

            return false;
        }

        #endregion
    }
}