namespace Ability.Casting.ComboExecution
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using Ability.AbilityMenu;
    using Ability.AbilityMenu.Menus.BuffsMenu;
    using Ability.AbilityMenu.Menus.DisablesMenu;
    using Ability.AbilityMenu.Menus.HarrasesMenu;
    using Ability.AbilityMenu.Menus.HealsMenu;
    using Ability.AbilityMenu.Menus.NukesMenu;
    using Ability.AbilityMenu.Menus.ShieldsMenu;
    using Ability.AbilityMenu.Menus.SilencesMenu;
    using Ability.AbilityMenu.Menus.SlowsMenu;
    using Ability.AbilityMenu.Menus.SpecialsMenu;
    using Ability.DamageCalculation;
    using Ability.ObjectManager;
    using Ability.ObjectManager.Heroes;

    using Ensage;
    using Ensage.Common;
    using Ensage.Common.AbilityInfo;
    using Ensage.Common.Extensions;
    using Ensage.Common.Menu;

    internal class FullCombo
    {
        #region Static Fields

        private static float dealtDamage;

        private static float etherealHitTime;

        private static float coldFeetLastUse;

        #endregion

        #region Public Methods and Operators

        public static bool AutoUsage(
            Hero enemyHero,
            Hero[] enemyHeroes,
            float meMissingHp,
            List<Modifier> meModifiers,
            float ping,
            Hero me)
        {
            var hero = enemyHero;
            var heroName = NameManager.Name(hero);
            var heroDistance = MyHeroInfo.Position.Distance2D(hero);
            var modifiers = hero.Modifiers.ToList();
            if (Utils.SleepCheck("GlobalCasting") && Utils.SleepCheck("casting"))
            {
                if (me.ClassID == ClassID.CDOTA_Unit_Hero_Tusk
                    && Dictionaries.InDamageDictionary.ContainsKey(hero.Handle)
                    && (Dictionaries.OutDamageDictionary[hero.Handle] + Dictionaries.InDamageDictionary[hero.Handle])
                    >= enemyHero.Health && Utils.SleepCheck("tusk_snowball") && heroDistance <= 1350
                    && me.FindSpell("tusk_snowball").CanBeCasted())
                {
                    if (!Nuke.Cast(me.FindSpell("tusk_snowball"), hero, "tusk_snowball"))
                    {
                        return false;
                    }
                    Utils.Sleep(100 + me.GetTurnTime(hero) * 1000 + ping, "tusk_snowball");
                    Utils.Sleep(me.GetTurnTime(hero) * 1000 + (heroDistance / 675) * 1000 + 100, "GlobalCasting");
                    Utils.Sleep(me.GetTurnTime(hero) * 1000 + (heroDistance / 675) * 1000 + 100, "calculate");
                    Utils.Sleep(me.GetTurnTime(hero) * 1000 + (heroDistance / 675) * 1000, "cancelorder");

                    return true;
                }

                foreach (var data in
                    MyAbilities.OffensiveAbilities.Where(
                        x =>
                        x.Value.IsValid && x.Value.CanBeCasted()
                        && ((x.Value is Item && me.CanUseItems()) || (!(x.Value is Item) && me.CanCast()))
                        && (Utils.SleepCheck(x.Value.Handle.ToString())
                            || (!x.Value.IsInAbilityPhase && x.Value.FindCastPoint() > 0)))
                        .OrderBy(x => ComboOrder.GetAbilityOrder(x.Value)))
                {
                    var ability = data.Value;
                    var name = NameManager.Name(ability);
                    var category = data.Key.Substring(name.Length);
                    var handleString = ability.Handle.ToString();
                    if (!CastingChecks.All(name, hero, modifiers))
                    {
                        continue;
                    }
                    var delay = ability.GetCastDelay(me, hero, useCastPoint: false, abilityName: name) * 1000;
                    var canHit = ability.CanHit(hero, MyHeroInfo.Position, name);
                    if (category == "nuke"
                        && MainMenu.Menu.Item("nukesToggler").GetValue<AbilityToggler>().IsEnabled(name)
                        && Nukes.NukesMenuDictionary[name].Item(name + "herotoggler")
                               .GetValue<HeroToggler>()
                               .IsEnabled(heroName) && canHit
                        && (etherealHitTime < (Environment.TickCount + ability.GetHitDelay(hero, name) * 1000))
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
                        if (Nuke.Cast(ability, hero, name))
                        {
                            if (name == "riki_blink_strike")
                            {
                                Utils.Sleep(MyHeroInfo.AttackRate() * 1000 + ping + 100, handleString);
                            }
                            if (name == "item_ethereal_blade")
                            {
                                //Utils.Sleep(
                                //    me.GetTurnTime(hero) * 1000
                                //    + Prediction.CalculateReachTime(hero, 1200, hero.Position - MyHeroInfo.Position),
                                //    "casting");
                                etherealHitTime =
                                    (float)
                                    (Environment.TickCount + me.GetTurnTime(hero) * 1000
                                     + Prediction.CalculateReachTime(hero, 1200, hero.Position - MyHeroInfo.Position)
                                     + ping);
                                Utils.Sleep(
                                    me.GetTurnTime(hero) * 1000 + 100
                                    + (MyHeroInfo.Position.Distance2D(hero) / 1200) * 1000 + ping,
                                    "calculate");
                            }
                            if (ability.ChannelTime(name) > 0)
                            {
                                Utils.Sleep(delay + (ability.ChannelTime(name) * 1000) / 3, "cancelorder");
                            }
                            Utils.Sleep(
                                ability.GetCastDelay(me, hero, abilityName: name) * 1000 + ping + 100,
                                handleString);
                            Utils.Sleep(delay, "GlobalCasting");
                            Utils.Sleep(delay, "cancelorder");
                            //Utils.Sleep(ping, "killsteal");
                            return true;
                        }
                        continue;
                    }
                    if (Dictionaries.InDamageDictionary.ContainsKey(hero.Handle)
                        && Dictionaries.InDamageDictionary[hero.Handle] >= hero.Health)
                    {
                        continue;
                    }
                    if (category == "disable"
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
                            }
                            Utils.Sleep(
                                ability.GetCastDelay(me, hero, abilityName: name) * 1000 + ping + 100,
                                handleString);
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
                            }
                            Utils.Sleep(
                                ability.GetCastDelay(me, hero, abilityName: name) * 1000 + ping + 100,
                                handleString);
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
                            }
                            Utils.Sleep(
                                ability.GetCastDelay(me, hero, abilityName: name) * 1000 + ping + 100,
                                handleString);
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
                            }
                            Utils.Sleep(
                                ability.GetCastDelay(me, hero, abilityName: name) * 1000 + ping + 100,
                                handleString);
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
                            }
                            Utils.Sleep(
                                ability.GetCastDelay(me, hero, abilityName: name) * 1000 + ping + 100,
                                handleString);
                            Utils.Sleep(delay, "GlobalCasting");
                            Utils.Sleep(delay, "cancelorder");
                            return true;
                        }
                        continue;
                    }
                    if (category == "slow"
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
                            }
                            Utils.Sleep(
                                ability.GetCastDelay(me, hero, abilityName: name) * 1000 + ping + 100,
                                handleString);
                            if (name == "item_ethereal_blade")
                            {
                                Utils.Sleep(
                                    me.GetTurnTime(hero) * 1000
                                    + Prediction.CalculateReachTime(hero, 1200, hero.Position - MyHeroInfo.Position),
                                    "casting");
                                Utils.Sleep(
                                    me.GetTurnTime(hero) * 1000 + 100
                                    + (MyHeroInfo.Position.Distance2D(hero) / 1200) * 1000 + ping,
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
                            }
                            if (name == "item_ethereal_blade")
                            {
                                Utils.Sleep(
                                    me.GetTurnTime(hero) * 1000
                                    + Prediction.CalculateReachTime(hero, 1200, hero.Position - MyHeroInfo.Position),
                                    "casting");
                                Utils.Sleep(
                                    me.GetTurnTime(hero) * 1000 + 100
                                    + (MyHeroInfo.Position.Distance2D(hero) / 1200) * 1000 + ping,
                                    "calculate");
                            }
                            Utils.Sleep(
                                ability.GetCastDelay(me, hero, abilityName: name) * 1000 + ping + 100,
                                handleString);
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
                            }
                            if (name == "item_ethereal_blade")
                            {
                                Utils.Sleep(
                                    me.GetTurnTime(hero) * 1000
                                    + Prediction.CalculateReachTime(hero, 1200, hero.Position - MyHeroInfo.Position),
                                    "casting");
                                Utils.Sleep(
                                    me.GetTurnTime(hero) * 1000 + 100
                                    + (MyHeroInfo.Position.Distance2D(hero) / 1200) * 1000 + ping,
                                    "calculate");
                            }
                            Utils.Sleep(
                                ability.GetCastDelay(me, hero, abilityName: name) * 1000 + ping + 100,
                                handleString);
                            Utils.Sleep(delay, "GlobalCasting");
                            Utils.Sleep(delay, "cancelorder");
                            return true;
                        }
                        continue;
                    }
                    if (category == "special"
                        && MainMenu.Menu.Item("specialsToggler").GetValue<AbilityToggler>().IsEnabled(name))
                    {
                        if (name == "rubick_spell_steal")
                        {
                            var spell4 = me.Spellbook.Spell4;
                            if ((!spell4.CanBeCasted() || spell4 == null) && Rubick.LastCasted(heroName) != null
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
                            }
                            Utils.Sleep(
                                ability.GetCastDelay(me, hero, abilityName: name) * 1000 + ping + 100,
                                handleString);
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
                            }
                            Utils.Sleep(
                                ability.GetCastDelay(me, hero, abilityName: name) * 1000 + ping + 100,
                                handleString);
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
                            }
                            Utils.Sleep(
                                ability.GetCastDelay(me, hero, abilityName: name) * 1000 + ping + 100,
                                handleString);
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
                            Utils.Sleep(
                                ability.GetCastDelay(me, hero, abilityName: name) * 1000 + ping + 100,
                                handleString);
                            Utils.Sleep(delay, "GlobalCasting");
                            Utils.Sleep(delay, "cancelorder");
                            return true;
                        }
                        continue;
                    }
                    if (category == "buff"
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
                                && Buff.Cast(ability, hero, me, name, meModifiers, true))
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
                            > meMissingHp
                            && Buffs.BuffsMenuDictionary[name].Item(name + "belowhpslider").GetValue<Slider>().Value
                            < me.Health)
                        {
                            continue;
                        }
                        if (
                            Buffs.BuffsMenuDictionary[name].Item(name + "onsighttoggler")
                                .GetValue<HeroToggler>()
                                .IsEnabled(heroName) && canHit && Buff.Cast(ability, hero, me, name, meModifiers))
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
                            }
                            Utils.Sleep(delay, "GlobalCasting");
                            Utils.Sleep(delay, "cancelorder");
                            return true;
                        }
                        if (
                            Buffs.BuffsMenuDictionary[name].Item(name + "onattacktoggler")
                                .GetValue<HeroToggler>()
                                .IsEnabled(heroName) && canHit && CastingChecks.Attacking(hero, me)
                            && Buff.Cast(ability, hero, me, name, meModifiers))
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
                            }
                            Utils.Sleep(delay, "GlobalCasting");
                            Utils.Sleep(delay, "cancelorder");
                            return true;
                        }
                        continue;
                    }
                    if (category == "silence"
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
                            }
                            Utils.Sleep(
                                ability.GetCastDelay(me, hero, abilityName: name) * 1000 + ping + 100,
                                handleString);
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
                            }
                            Utils.Sleep(
                                ability.GetCastDelay(me, hero, abilityName: name) * 1000 + ping + 100,
                                handleString);
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
                            }
                            Utils.Sleep(
                                ability.GetCastDelay(me, hero, abilityName: name) * 1000 + ping + 100,
                                handleString);
                            Utils.Sleep(delay, "GlobalCasting");
                            Utils.Sleep(delay, "cancelorder");
                            return true;
                        }
                        continue;
                    }
                    if (category == "harras"
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
                            }
                            Utils.Sleep(
                                ability.GetCastDelay(me, hero, abilityName: name) * 1000 + ping + 100,
                                handleString);
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
                            }
                            Utils.Sleep(
                                ability.GetCastDelay(me, hero, abilityName: name) * 1000 + ping + 100,
                                handleString);
                            Utils.Sleep(delay, "GlobalCasting");
                            Utils.Sleep(delay, "cancelorder");
                            return true;
                        }
                    }
                }
            }
            return false;
        }

        public static bool DeffensiveAutoUsage(Hero allyHero, Hero me, Hero[] enemyHeroes, float ping)
        {
            var hero = allyHero;
            var heroName = NameManager.Name(hero);
            //var heroDistance = position.Distance2D(hero);
            var modifiers = hero.Modifiers.ToList();
            var heroMissingHp = hero.MaximumHealth - hero.Health;
            var heroHpPercentage = ((float)hero.Health / hero.MaximumHealth) * 100;
            foreach (var data in
                MyAbilities.DeffensiveAbilities.Where(
                    x =>
                    (x.Value.IsValid && x.Value.CanBeCasted()
                     && ((x.Value is Item && me.CanUseItems()) || (!(x.Value is Item) && me.CanCast()))
                     && (Utils.SleepCheck(x.Value.Handle.ToString())
                         || (!x.Value.IsInAbilityPhase && x.Value.FindCastPoint() > 0)))))
            {
                var ability = data.Value;
                var name = NameManager.Name(ability);
                var category = data.Key.Substring(name.Length);
                var handleString = ability.Handle.ToString();
                if (!CastingChecks.All(name, hero, modifiers))
                {
                    continue;
                }
                var delay = ability.GetCastDelay(me, hero, abilityName: name) * 1000;
                var canHit = ability.CanHit(hero, MyHeroInfo.Position, name);
                if (category == "heal" && MainMenu.Menu.Item("healsToggler").GetValue<AbilityToggler>().IsEnabled(name)
                    && Heals.HealsMenuDictionary[name].Item(name + "useonallies")
                           .GetValue<HeroToggler>()
                           .IsEnabled(heroName) && canHit
                    && Heals.HealsMenuDictionary[name].Item(name + "missinghpmin").GetValue<Slider>().Value
                    < (heroMissingHp)
                    && Heals.HealsMenuDictionary[name].Item(name + "hppercentbelow").GetValue<Slider>().Value
                    > (heroHpPercentage)
                    && (enemyHeroes.Count(
                        x =>
                        (!Heals.HealsMenuDictionary[name].Item(name + "usenearbool").GetValue<bool>()
                         || Heals.HealsMenuDictionary[name].Item(name + "usenear")
                                .GetValue<HeroToggler>()
                                .IsEnabled(NameManager.Name(x)))
                        && (x.Distance2D(hero) < (Math.Max(x.GetAttackRange(), 700)))) - 1)
                    >= Heals.HealsMenuDictionary[name].Item(name + "minenemiesaround")
                           .GetValue<StringList>()
                           .SelectedIndex
                    && (!(name == "item_mekansm" || name == "item_guardian_greaves" || name == "chen_hand_of_god")
                        || (Heals.HealsMenuDictionary[name].Item(name + "minalliesheal")
                                .GetValue<StringList>()
                                .SelectedIndex
                            <= (AllyHeroes.Heroes.Count(
                                x =>
                                !x.Equals(me) && ability.CanHit(x, MyHeroInfo.Position, name)
                                && (x.MaximumHealth - x.Health)
                                > Heals.HealsMenuDictionary[name].Item(name + "missinghpmin").GetValue<Slider>().Value))
                            - 1)) && Heal.Cast(ability, hero, name))
                {
                    Utils.Sleep(ability.GetCastDelay(me, hero, abilityName: name) * 1000 + ping + 100, handleString);
                    if (name == "item_tango" || name == "item_tango_single")
                    {
                        Utils.Sleep(16000, handleString);
                    }
                    Utils.Sleep(delay, "GlobalCasting");
                    Utils.Sleep(delay, "casting");
                    Utils.Sleep(delay, "cancelorder");
                    return true;
                }
                if (category == "shield"
                    && MainMenu.Menu.Item("shieldsToggler").GetValue<AbilityToggler>().IsEnabled(name) && canHit
                    && (!(!ability.IsAbilityBehavior(AbilityBehavior.NoTarget, name)
                          || (name == "item_pipe" || name == "item_buckler" || name == "omniknight_guardian_angel"
                              || name == "item_crimson_guard"))
                        || (Shields.ShieldsMenuDictionary[name].Item(name + "useonallies")
                               .GetValue<HeroToggler>()
                               .IsEnabled(heroName)))
                    && Shields.ShieldsMenuDictionary[name].Item(name + "missinghpmin").GetValue<Slider>().Value
                    < (heroMissingHp)
                    && Shields.ShieldsMenuDictionary[name].Item(name + "hppercentbelow").GetValue<Slider>().Value
                    > (heroHpPercentage)
                    && (enemyHeroes.Count(
                        x =>
                        (!Shields.ShieldsMenuDictionary[name].Item(name + "usenearbool").GetValue<bool>()
                         || Shields.ShieldsMenuDictionary[name].Item(name + "usenear")
                                .GetValue<HeroToggler>()
                                .IsEnabled(NameManager.Name(x)))
                        && (x.Distance2D(hero) < (Math.Max(x.GetAttackRange(), 700)))) - 1)
                    >= Shields.ShieldsMenuDictionary[name].Item(name + "minenemiesaround")
                           .GetValue<StringList>()
                           .SelectedIndex
                    && (!(name == "item_pipe" || name == "item_buckler" || name == "omniknight_guardian_angel"
                          || name == "item_crimson_guard")
                        || (Shields.ShieldsMenuDictionary[name].Item(name + "minalliesaffect")
                                .GetValue<StringList>()
                                .SelectedIndex
                            <= (AllyHeroes.Heroes.Count(
                                x => !x.Equals(me) && ability.CanHit(x, MyHeroInfo.Position, name)) - 1)))
                    && Shield.Cast(ability, hero, name))
                {
                    Utils.Sleep(ability.GetCastDelay(me, hero, abilityName: name) * 1000 + ping + 100, handleString);
                    Utils.Sleep(delay, "GlobalCasting");
                    Utils.Sleep(delay, "casting");
                    Utils.Sleep(delay, "cancelorder");
                    return true;
                }
            }
            return false;
        }

        public static bool Execute(
            Hero target,
            Hero[] enemyHeroes,
            float ping,
            bool onlyDamage,
            bool onlyDisable,
            Hero me,
            List<Modifier> meModifiers)
        {
            if (Utils.SleepCheck("UpdateCombo"))
            {
                var toggler = MainMenu.ComboKeysMenu.Item("comboAbilitiesToggler").GetValue<AbilityToggler>();
                MyAbilities.Combo =
                    MyAbilities.OffensiveAbilities.Where(
                        x => x.Value.IsValid && x.Value.Owner.Equals(me) && toggler.IsEnabled(NameManager.Name(x.Value)))
                        .OrderBy(x => ComboOrder.GetComboOrder(x.Value, onlyDisable));
                Utils.Sleep(500, "UpdateCombo");
            }
            if (Utils.SleepCheck("casting") && MyAbilities.Combo != null)
            {
                if (target != null)
                {
                    if (Dictionaries.HitDamageDictionary.ContainsKey(target.Handle)
                        && Dictionaries.HitDamageDictionary[target.Handle] * 1.5 >= target.Health
                        && target.Distance2D(MyHeroInfo.Position) <= me.GetAttackRange() + 150)
                    {
                        return false;
                    }
                    var modifiers = target.Modifiers.ToList();
                    if (AbilityMain.Me.ClassID == ClassID.CDOTA_Unit_Hero_TemplarAssassin)
                    {
                        var r = MyAbilities.Combo.FirstOrDefault(x => x.Key == "templar_assassin_psionic_trapslow");
                        var modifier =
                            target.Modifiers.FirstOrDefault(x => x.Name == "modifier_templar_assassin_trap_slow");
                        if (modifier == null || modifier.RemainingTime < (r.Value.GetHitDelay(target)))
                        {
                            var closestTrap =
                                ObjectMgr.GetEntities<Unit>()
                                    .Where(
                                        x =>
                                        x.ClassID == ClassID.CDOTA_BaseNPC_Additive && x.Team == AbilityMain.Me.Team
                                        && x.IsAlive && x.IsVisible && x.Distance2D(target) < 400
                                        && x.FindSpell("templar_assassin_self_trap") != null
                                        && x.FindSpell("templar_assassin_self_trap").CanBeCasted())
                                    .MinOrDefault(x => x.Distance2D(target));
                            if (closestTrap != null)
                            {
                                closestTrap.FindSpell("templar_assassin_self_trap").CastStun(target, 1);
                            }
                        }
                    }
                    foreach (var data in
                        MyAbilities.Combo.Where(
                            x =>
                            x.Value.IsValid && x.Value.CanBeCasted()
                            && !x.Value.IsAbilityBehavior(AbilityBehavior.Hidden)
                            && ((x.Value is Item && me.CanUseItems()) || (!(x.Value is Item) && me.CanCast()))
                            && (Utils.SleepCheck(x.Value.Handle.ToString())
                                || (!x.Value.IsInAbilityPhase && x.Value.FindCastPoint() > 0))))
                    {
                        var ability = data.Value;
                        var name = NameManager.Name(ability);
                        var category = (name == "lion_impale") ? "disable" : data.Key.Substring(name.Length);
                        //if (category == "special" || category == "buff")
                        //{
                        //    continue;
                        //}
                        //if (onlyDamage && (category == "disable" || category == "slow" || category == "silence"))
                        //{
                        //    continue;
                        //}
                        //if (onlyDisable && (category == "nuke" || category == "harras"))
                        //{
                        //    continue;
                        //}
                        if (!CastingChecks.All(name, target, modifiers))
                        {
                            continue;
                        }
                        var handleString = ability.Handle.ToString();
                        if (etherealHitTime >= (Environment.TickCount + ability.GetHitDelay(target, name) * 1000))
                        {
                            continue;
                        }
                        if (!ability.CanHit(target, MyHeroInfo.Position, name) && category != "buff")
                        {
                            dealtDamage = 0;
                            if (name == "templar_assassin_meld")
                            {
                                AbilityMain.Me.Move(Game.MousePosition);
                                return true;
                            }
                            if (name == "pudge_rot")
                            {
                                continue;
                            }
                            if (target.Distance2D(MyHeroInfo.Position) > ability.GetCastRange(name) + 425)
                            {
                                continue;
                            }
                            return false;
                        }
                        if (name == "item_cyclone" && coldFeetLastUse - Environment.TickCount < 2500
                            && coldFeetLastUse - Environment.TickCount > -1000)
                        {
                            continue;
                        }
                        if (category == "nuke"
                            && ((name == "axe_culling_blade" && !CastingChecks.Killsteal(ability, target, name))
                                || (target.Health
                                    < Nukes.NukesMenuDictionary[name].Item(name + "minhealthslider")
                                          .GetValue<Slider>()
                                          .Value)
                                || (name == "zuus_thundergods_wrath"
                                    && (1
                                        + enemyHeroes.Count(
                                            x =>
                                            !x.Equals(target)
                                            && x.Health <= AbilityDamage.CalculateDamage(ability, me, x)))
                                    < Nukes.NukesMenuDictionary[name].Item(name + "minenemykill")
                                          .GetValue<Slider>()
                                          .Value) || !Nuke.Cast(ability, target, name)))
                        {
                            continue;
                        }
                        if (category == "disable" && !Disable.Cast(ability, target, name))
                        {
                            continue;
                        }
                        if (category == "slow" && !Slow.Cast(ability, target, name))
                        {
                            continue;
                        }
                        if (category == "harras" && !Harras.Cast(ability, target, name))
                        {
                            continue;
                        }
                        if (category == "silence" && !Silence.Cast(ability, target, name))
                        {
                            continue;
                        }
                        if (category == "special" && !Special.Cast(ability, target, name))
                        {
                            continue;
                        }
                        if (category == "buff"
                            && (name == "item_armlet" || name == "item_satanic"
                                || !Buff.Cast(ability, target, me, name, meModifiers)))
                        {
                            continue;
                        }
                        if (Utils.SleepCheck(ability.Handle.ToString()))
                        {
                            dealtDamage += AbilityDamage.CalculateDamage(ability, me, target);
                        }
                        var delay = Math.Max(ability.GetCastDelay(me, target, abilityName: name), 0.2) * 1000;
                        switch (name)
                        {
                            case "riki_blink_strike":
                                Utils.Sleep(MyHeroInfo.AttackRate() * 1000, handleString);
                                break;
                            case "necrolyte_reapers_scythe":
                                Utils.Sleep(delay + ping + 1500, "calculate");
                                break;
                            case "necrolyte_death_pulse":
                                Utils.Sleep(delay + ping + 200, "calculate");
                                break;
                            case "item_ethereal_blade":
                                etherealHitTime =
                                    (float)
                                    (Environment.TickCount + me.GetTurnTime(target) * 1000
                                     + Prediction.CalculateReachTime(target, 1200, target.Position - me.Position) + ping * 2);
                                Utils.Sleep(
                                    me.GetTurnTime(target) * 1000 + 100
                                    + (MyHeroInfo.Position.Distance2D(target) / 1200) * 1000 + ping,
                                    "calculate");
                                break;
                            case "tusk_snowball":
                                Utils.Sleep(
                                    me.GetTurnTime(target) * 1000 + (MyHeroInfo.Position.Distance2D(target) / 675) * 1000,
                                    "GlobalCasting");
                                break;
                            case "ancient_apparition_cold_feet":
                                coldFeetLastUse = Environment.TickCount+4000;
                                break;
                        }
                        if (ability.ChannelTime(name) > 0)
                        {
                            Utils.Sleep(delay + (ability.ChannelTime(name) * 1000) / 3, "cancelorder");
                            Utils.Sleep(delay + (ability.ChannelTime(name) * 1000) / 4, "casting");
                        }
                        Utils.Sleep(delay, handleString);
                        Utils.Sleep(delay, "GlobalCasting");
                        Utils.Sleep(ability.GetHitDelay(target, name) * 1000, "calculate");
                        Utils.Sleep(
                            Math.Max(ability.GetCastDelay(me, target, useCastPoint: false, abilityName: name), 0.15)
                            * 1000 + Game.Ping,
                            //+ (Math.Max(me.Distance2D(target) - ability.GetCastRange(name) - 50, 0)
                            //   / me.MovementSpeed) * 1000,
                            "casting");
                        Utils.Sleep(delay, "cancelorder");
                        if (name == "pudge_rot")
                        {
                            continue;
                        }
                        return true;
                    }
                }
            }
            return false;
        }

        public static bool KillSteal(Hero[] enemyHeroes, float ping, Hero me)
        {
            var possibleTarget =
                enemyHeroes.FirstOrDefault(
                    hero =>
                    Dictionaries.InDamageDictionary.ContainsKey(hero.Handle)
                    && Dictionaries.InDamageDictionary[hero.Handle] >= hero.Health);
            if (possibleTarget != null && possibleTarget.CanDie() && MyAbilities.NukesCombo.Any())
            {
                if (Dictionaries.InDamageDictionary[possibleTarget.Handle] >= possibleTarget.Health)
                {
                    foreach (var ability in (from ability in MyAbilities.NukesCombo.OrderBy(ComboOrder.GetAbilityOrder)
                                             where
                                                 ability != null && ability.CanBeCasted()
                                                 && (Utils.SleepCheck(ability.Handle.ToString())
                                                     || (!ability.IsInAbilityPhase && ability.FindCastPoint() > 0))
                                             select ability))
                    {
                        var name = NameManager.Name(ability);
                        var handleString = ability.Handle.ToString();
                        if (etherealHitTime
                            >= (Environment.TickCount + ability.GetHitDelay(possibleTarget, name) * 1000))
                        {
                            continue;
                        }
                        if (!ability.CanHit(possibleTarget, MyHeroInfo.Position, name)
                            || ((possibleTarget.Health - dealtDamage) > 0
                                && (possibleTarget.Health - dealtDamage)
                                < Nukes.NukesMenuDictionary[name].Item(name + "minhealthslider")
                                      .GetValue<Slider>()
                                      .Value)
                            || (name == "zuus_thundergods_wrath"
                                && (1
                                    + enemyHeroes.Count(
                                        x =>
                                        !x.Equals(possibleTarget)
                                        && x.Health <= AbilityDamage.CalculateDamage(ability, me, x)))
                                < Nukes.NukesMenuDictionary[name].Item(name + "minenemykill").GetValue<Slider>().Value)
                            || !Nuke.Cast(ability, possibleTarget, name))
                        {
                            dealtDamage = 0;
                            return false;
                        }
                        if (Utils.SleepCheck(ability.Handle.ToString()))
                        {
                            dealtDamage += AbilityDamage.CalculateDamage(ability, me, possibleTarget);
                        }
                        var delay = ability.GetCastDelay(me, possibleTarget, abilityName: name) * 1000;
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
                            //Utils.Sleep(
                            //    Me.GetTurnTime(possibleTarget) * 1000
                            //    + Prediction.CalculateReachTime(
                            //        possibleTarget,
                            //        1200,
                            //        possibleTarget.Position - MyHeroInfo.Position),
                            //    "casting");
                            etherealHitTime =
                                (float)
                                (Environment.TickCount + me.GetTurnTime(possibleTarget) * 1000
                                 + Prediction.CalculateReachTime(
                                     possibleTarget,
                                     1200,
                                     possibleTarget.Position - me.Position) + ping * 2);
                            Utils.Sleep(
                                me.GetTurnTime(possibleTarget) * 1000 + 100
                                + (MyHeroInfo.Position.Distance2D(possibleTarget) / 1200) * 1000 + ping,
                                "calculate");
                        }
                        if (name == "tusk_snowball")
                        {
                            Utils.Sleep(
                                me.GetTurnTime(possibleTarget) * 1000
                                + (MyHeroInfo.Position.Distance2D(possibleTarget) / 675) * 1000,
                                "GlobalCasting");
                        }
                        Utils.Sleep(delay, handleString);
                        Utils.Sleep(delay, "GlobalCasting");
                        Utils.Sleep(ability.GetHitDelay(possibleTarget, name) * 1000, "calculate");
                        Utils.Sleep(
                            ability.GetCastDelay(me, possibleTarget, useCastPoint: false, abilityName: name) * 1000,
                            //+ (Math.Max(Me.Distance2D(possibleTarget) - ability.GetCastRange(name) - 50, 0)
                            //   / Me.MovementSpeed) * 1000,
                            "casting");
                        Utils.Sleep(delay, "cancelorder");
                        return true;
                    }
                }
            }
            else if (MyAbilities.NukesCombo.Any())
            {
                MyAbilities.NukesCombo = new List<Ability>();
            }
            return false;
        }

        #endregion
    }
}