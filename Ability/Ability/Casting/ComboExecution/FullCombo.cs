namespace Ability.Casting.ComboExecution
{
    using System;
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
    using Ability.Extensions;
    using Ability.ObjectManager;
    using Ability.ObjectManager.Heroes;

    using Ensage;
    using Ensage.Common;
    using Ensage.Common.AbilityInfo;
    using Ensage.Common.Extensions;
    using Ensage.Common.Menu;
    using Ensage.Common.Objects;

    /// <summary>
    ///     The full combo.
    /// </summary>
    internal class FullCombo
    {
        #region Static Fields

        private static float coldFeetLastUse;

        #endregion

        #region Public Methods and Operators

        public static bool DeffensiveAutoUsage(Hero allyHero, Hero me, Hero[] enemyHeroes, float ping)
        {
            var hero = allyHero;
            var heroName = NameManager.Name(hero);

            // var heroDistance = position.Distance2D(hero);
            var heroMissingHp = hero.MaximumHealth - hero.Health;
            var heroMissingMana = hero.MaximumMana - hero.Mana;
            var heroManaPercentage = (hero.Mana / hero.MaximumMana) * 100;
            var heroHpPercentage = ((float)hero.Health / hero.MaximumHealth) * 100;
            foreach (var data in
                MyAbilities.DeffensiveAbilities.Where(
                    x =>
                    (x.Value.IsValid
                     && (x.Value.CanBeCasted() || (x.Value.CanBeCasted(SoulRing.ManaGained) && SoulRing.Check(x.Value)))
                     && ((x.Value is Item && me.CanUseItems()) || (!(x.Value is Item) && me.CanCast()))
                     && (Utils.SleepCheck(x.Value.Handle.ToString())
                         || (!x.Value.IsInAbilityPhase && x.Value.FindCastPoint() > 0)))))
            {
                var ability = data.Value;
                var name = NameManager.Name(ability);
                if (name == "item_soul_ring")
                {
                    continue;
                }

                if (hero.IsMagicImmune() && ability.ImmunityType != (ImmunityType)1)
                {
                    continue;
                }

                var category = data.Key.Substring(name.Length);
                var handleString = ability.Handle.ToString();
                if (!CastingChecks.All(name, hero))
                {
                    continue;
                }

                var delay = ability.GetCastDelay(me, hero, abilityName: name) * 1000;
                var canHit = ability.CanHit(hero, MyHeroInfo.Position, name);
                if (category == "heal" && !hero.HasModifier("modifier_ice_blast")
                    && MainMenu.Menu.Item("healsToggler").GetValue<AbilityToggler>().IsEnabled(name)
                    && ((!(name == "item_soul_ring" || name == "item_magic_wand" || name == "item_magic_stick")
                         && Heals.HealsMenuDictionary[name].Item(name + "useonallies")
                                .GetValue<HeroToggler>()
                                .IsEnabled(heroName)) || hero.Equals(AbilityMain.Me)) && canHit
                    && (name == "item_arcane_boots"
                            ? (Heals.HealsMenuDictionary[name].Item(name + "missingmanamin").GetValue<Slider>().Value
                               < heroMissingMana
                               && Heals.HealsMenuDictionary[name].Item(name + "manapercentbelow")
                                      .GetValue<Slider>()
                                      .Value > heroManaPercentage
                               && (!AllyHeroes.Heroes.Any(
                                   x =>
                                   !x.Equals(hero)
                                   && Heals.HealsMenuDictionary[name].Item(name + "useonallies")
                                          .GetValue<HeroToggler>()
                                          .IsEnabled(x.Name)
                                   && x.Distance2D(AbilityMain.Me)
                                   < Heals.HealsMenuDictionary[name].Item(name + "waitrange").GetValue<Slider>().Value
                                   && !ability.CanHit(x, MyHeroInfo.Position))))
                            : (Heals.HealsMenuDictionary[name].Item(name + "missinghpmin").GetValue<Slider>().Value
                               < heroMissingHp
                               && Heals.HealsMenuDictionary[name].Item(name + "hppercentbelow").GetValue<Slider>().Value
                               > heroHpPercentage))
                    && (name == "item_urn_of_shadows"
                            ? (!enemyHeroes.Any(x => x.Distance2D(hero) < Math.Max(x.GetAttackRange(), 500))
                               && !hero.HasModifiers(
                                   new[]
                                       {
                                           "modifier_doom_bringer_doom", "modifier_axe_battle_hunger", 
                                           "modifier_queenofpain_shadow_strike", "modifier_phoenix_fire_spirit_burn", 
                                           "modifier_venomancer_poison_nova", "modifier_venomancer_venomous_gale", 
                                           "modifier_huskar_burning_spear_debuff"
                                       }, 
                                   false))
                            : (name == "item_arcane_boots"
                               || (enemyHeroes.Count(
                                   x =>
                                   (!Heals.HealsMenuDictionary[name].Item(name + "usenearbool").GetValue<bool>()
                                    || Heals.HealsMenuDictionary[name].Item(name + "usenear")
                                           .GetValue<HeroToggler>()
                                           .IsEnabled(NameManager.Name(x)))
                                   && (x.Distance2D(hero) < Math.Max(x.GetAttackRange(), 700))) - 1
                                   >= Heals.HealsMenuDictionary[name].Item(name + "minenemiesaround")
                                          .GetValue<StringList>()
                                          .SelectedIndex)))
                    && (name != "omniknight_purification" || hero.Health < hero.MaximumHealth * 0.22
                        || enemyHeroes.Any(
                            x => x.Predict(Game.Ping).Distance2D(hero.Predict(Game.Ping)) <= ability.GetRadius(name)))
                    && (!(name == "item_mekansm" || name == "item_guardian_greaves" || name == "chen_hand_of_god")
                        || ((!AllyHeroes.Heroes.Any(
                            x =>
                            !x.Equals(hero)
                            && Heals.HealsMenuDictionary[name].Item(name + "useonallies")
                                   .GetValue<HeroToggler>()
                                   .IsEnabled(x.Name)
                            && x.Distance2D(AbilityMain.Me)
                            < Heals.HealsMenuDictionary[name].Item(name + "waitrange").GetValue<Slider>().Value
                            && !ability.CanHit(x, MyHeroInfo.Position)))
                            && Heals.HealsMenuDictionary[name].Item(name + "minalliesheal")
                                   .GetValue<StringList>()
                                   .SelectedIndex
                            <= AllyHeroes.Heroes.Count(
                                x =>
                                !x.Equals(me) && ability.CanHit(x, MyHeroInfo.Position, name)
                                && (x.MaximumHealth - x.Health)
                                > Heals.HealsMenuDictionary[name].Item(name + "missinghpmin").GetValue<Slider>().Value)
                            - 1)) && Heal.Cast(ability, hero, name))
                {
                    // if (name == "item_urn_of_shadows")
                    // {
                    // Console.WriteLine(
                    // hero.HasModifiers(
                    // new[]
                    // {
                    // "modifier_doom_bringer_doom", "modifier_axe_battle_hunger", 
                    // "modifier_queenofpain_shadow_strike", "modifier_phoenix_fire_spirit_burn", 
                    // "modifier_venomancer_poison_nova", "modifier_venomancer_venomous_gale"
                    // }, 
                    // false));
                    // }
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
                        || Shields.ShieldsMenuDictionary[name].Item(name + "useonallies")
                               .GetValue<HeroToggler>()
                               .IsEnabled(heroName))
                    && (Shields.ShieldsMenuDictionary[name].Item(name + "missinghpmin").GetValue<Slider>().Value
                        < heroMissingHp
                        && Shields.ShieldsMenuDictionary[name].Item(name + "hppercentbelow").GetValue<Slider>().Value
                        > heroHpPercentage
                        || Shields.ShieldsMenuDictionary[name].Item(name + "ondisabletoggler")
                               .GetValue<HeroToggler>()
                               .IsEnabled(heroName) && CastingChecks.DisabledAlly(hero))
                    && (enemyHeroes.Count(
                        x =>
                        (!Shields.ShieldsMenuDictionary[name].Item(name + "usenearbool").GetValue<bool>()
                         || Shields.ShieldsMenuDictionary[name].Item(name + "usenear")
                                .GetValue<HeroToggler>()
                                .IsEnabled(NameManager.Name(x)))
                        && (x.Distance2D(hero) < Math.Max(x.GetAttackRange(), 700))) - 1)
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
            float mana)
        {
            var toggler = MainMenu.ComboKeysMenu.Item("comboAbilitiesToggler").GetValue<AbilityToggler>();
            if (Utils.SleepCheck("UpdateCombo"))
            {
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

                    if (!Utils.SleepCheck(target.Handle + "KeyCombo"))
                    {
                        return false;
                    }

                    if (AbilityMain.Me.ClassID == ClassID.CDOTA_Unit_Hero_TemplarAssassin)
                    {
                        var r = MyAbilities.Combo.FirstOrDefault(x => x.Key == "templar_assassin_psionic_trapslow");
                        var modifier = target.FindModifier("modifier_templar_assassin_trap_slow");
                        if (modifier == null || modifier.RemainingTime < r.Value.GetHitDelay(target))
                        {
                            Slow.TemplarAssasinUseTrap(target);
                        }
                    }

                    if (me.ClassID == ClassID.CDOTA_Unit_Hero_Tinker && toggler.IsEnabled("tinker_rearm")
                        && MyAbilities.TinkerRearm.CanBeCasted() && Utils.SleepCheck("Ability.TinkerRearm")
                        && !MyAbilities.Combo.Any(
                            x =>
                            x.Value.CanBeCasted()
                            || (x.Value.CanBeCasted(SoulRing.ManaGained) && SoulRing.Check(x.Value))))
                    {
                        MyAbilities.TinkerRearm.UseAbility();
                        Utils.Sleep(
                            MyAbilities.TinkerRearm.FindCastPoint() * 1000 + Game.Ping
                            + MyAbilities.TinkerRearm.GetChannelTime(MyAbilities.TinkerRearm.Level - 1) * 1000 + 500, 
                            "Ability.TinkerRearm");
                        Utils.Sleep(
                            MyAbilities.TinkerRearm.FindCastPoint() * 1000 + Game.Ping
                            + MyAbilities.TinkerRearm.GetChannelTime(MyAbilities.TinkerRearm.Level - 1) * 1000, 
                            "GlobalCasting");
                        Utils.Sleep(
                            MyAbilities.TinkerRearm.FindCastPoint() * 1000 + Game.Ping
                            + MyAbilities.TinkerRearm.GetChannelTime(MyAbilities.TinkerRearm.Level - 1) * 1000, 
                            "casting");
                        Utils.Sleep(
                            MyAbilities.TinkerRearm.FindCastPoint() * 1000 + Game.Ping
                            + MyAbilities.TinkerRearm.GetChannelTime(MyAbilities.TinkerRearm.Level - 1) * 1000, 
                            "cancelorder");
                        return true;
                    }

                    foreach (var data in
                        MyAbilities.Combo.Where(
                            x =>
                            x.Value.IsValid
                            && (x.Value.CanBeCasted()
                                || (x.Value.CanBeCasted(SoulRing.ManaGained) && SoulRing.Check(x.Value)))
                            && !x.Value.IsAbilityBehavior(AbilityBehavior.Hidden)
                            && ((x.Value is Item && me.CanUseItems()) || (!(x.Value is Item) && me.CanCast()))
                            && (Utils.SleepCheck(x.Value.Handle.ToString())
                                || (!x.Value.IsInAbilityPhase && x.Value.FindCastPoint() > 0))))
                    {
                        var ability = data.Value;
                        var name = NameManager.Name(ability);
                        var category = (name == "lion_impale") ? "disable" : data.Key.Substring(name.Length);

                        // if (category == "special" || category == "buff")
                        // {
                        // continue;
                        // }
                        // if (onlyDamage && (category == "disable" || category == "slow" || category == "silence"))
                        // {
                        // continue;
                        // }
                        // if (onlyDisable && (category == "nuke" || category == "harras"))
                        // {
                        // continue;
                        // }
                        if (category != "buff" && target.IsMagicImmune() && ability.ImmunityType != (ImmunityType)3)
                        {
                            continue;
                        }

                        if (!CastingChecks.All(name, target, ability))
                        {
                            continue;
                        }

                        var handleString = ability.Handle.ToString();
                        if (Variables.EtherealHitTime >= (Utils.TickCount + ability.GetHitDelay(target, name) * 1000))
                        {
                            continue;
                        }

                        if (name == "omniknight_purification")
                        {
                            if (Nukes.NukesMenuDictionary[name].Item(name + "minManaCheck").GetValue<Slider>().Value
                                < mana && MainMenu.Menu.Item("nukesToggler").GetValue<AbilityToggler>().IsEnabled(name)
                                && Nukes.NukesMenuDictionary[name].Item(name + "herotoggler")
                                       .GetValue<HeroToggler>()
                                       .IsEnabled(NameManager.Name(target))
                                && (Variables.EtherealHitTime
                                    < (Utils.TickCount + ability.GetHitDelay(target, name) * 1000))
                                && target.Health
                                > Nukes.NukesMenuDictionary[name].Item(NameManager.Name(ability) + "minhealthslider")
                                      .GetValue<Slider>()
                                      .Value)
                            {
                                var target1 =
                                    AllyHeroes.UsableHeroes.Where(x => !x.IsMagicImmune())
                                        .MinOrDefault(x => x.Distance2D(target));
                                if (target1 != null && ability.CanHit(target1, MyHeroInfo.Position, name)
                                    && target1.PredictedPosition().Distance2D(target.PredictedPosition())
                                    < ability.GetRadius(name)
                                    && target1.PredictedPosition()
                                           .Distance2D(
                                               target.PredictedPosition(
                                                   ability.FindCastPoint(NameManager.Name(ability))))
                                    < ability.GetRadius(name))
                                {
                                    if (Nuke.Cast(ability, target1, name))
                                    {
                                        Utils.Sleep(
                                            Math.Max(ability.GetCastDelay(me, target1, abilityName: name), 0.2) * 1000, 
                                            "GlobalCasting");
                                        Utils.Sleep(ability.GetHitDelay(target1, name) * 1000, "calculate");
                                        Utils.Sleep(
                                            Math.Max(
                                                ability.GetCastDelay(
                                                    me, 
                                                    target1, 
                                                    useCastPoint: false, 
                                                    abilityName: name), 
                                                0.15) * 1000 + Game.Ping, 

                                            // + (Math.Max(me.Distance2D(target) - ability.GetCastRange(name) - 50, 0)
                                            // / me.MovementSpeed) * 1000,
                                            "casting");
                                        Utils.Sleep(
                                            Math.Max(ability.GetCastDelay(me, target1, abilityName: name), 0.2) * 1000, 
                                            "cancelorder");
                                        return true;
                                    }
                                }
                                else if (target1 != null && target1.Equals(me))
                                {
                                    if (Utils.SleepCheck("Ability.Move"))
                                    {
                                        me.Move(Game.MousePosition);
                                        Utils.Sleep(100, "Ability.Move");
                                    }

                                    Utils.Sleep(200, "GlobalCasting");
                                    return true;
                                }
                            }

                            return false;
                        }

                        if (!ability.CanHit(target, MyHeroInfo.Position, name) && category != "buff")
                        {
                            Variables.DealtDamage = 0;
                            if (name == "templar_assassin_meld")
                            {
                                if (!Nuke.Cast(ability, target, name) && Utils.SleepCheck("Ability.Move"))
                                {
                                    AbilityMain.Me.Move(Game.MousePosition);
                                    Utils.Sleep(100, "Ability.Move");
                                }

                                Utils.Sleep(200, "GlobalCasting");
                                return true;
                            }

                            if (name == "pudge_rot")
                            {
                                continue;
                            }

                            if (ability.IsAbilityBehavior(AbilityBehavior.NoTarget, NameManager.Name(ability))
                                && target.PredictedPosition().Distance2D(MyHeroInfo.Position)
                                < ability.GetRadius() + 150)
                            {
                                if (Utils.SleepCheck("Ability.Move"))
                                {
                                    me.Move(Game.MousePosition);
                                    Utils.Sleep(100, "Ability.Move");
                                }

                                Utils.Sleep(200, "GlobalCasting");
                                return true;
                            }

                            if (target.Distance2D(MyHeroInfo.Position) > ability.GetCastRange(name) + 250)
                            {
                                continue;
                            }

                            return false;
                        }

                        if (name == "item_cyclone" && coldFeetLastUse - Utils.TickCount < 2500
                            && coldFeetLastUse - Utils.TickCount > -1000)
                        {
                            continue;
                        }

                        if (((Dictionaries.InDamageDictionary.ContainsKey(target.Handle)
                              && Dictionaries.InDamageDictionary[target.Handle] >= target.Health)
                             || (Dictionaries.OutDamageDictionary.ContainsKey(target.Handle)
                                 && Dictionaries.InDamageDictionary.ContainsKey(target.Handle)
                                 && (Dictionaries.InDamageDictionary[target.Handle]
                                     + Dictionaries.OutDamageDictionary[target.Handle]) >= target.Health))
                            && MainMenu.Menu.Item("Ability#.EnableAutoKillSteal").GetValue<bool>())
                        {
                            return false;
                        }

                        if (category == "nuke"
                            && (Nukes.NukesMenuDictionary[name].Item(name + "minManaCheckCombo")
                                    .GetValue<Slider>()
                                    .Value > mana || !Utils.SleepCheck(target.Handle + "KillSteal")
                                || (name == "axe_culling_blade" && !CastingChecks.Killsteal(ability, target, name))
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
                                          .Value)))
                        {
                            continue;
                        }

                        if (name == "item_urn_of_shadows" && target.HasModifier("modifier_item_urn_damage"))
                        {
                            continue;
                        }

                        if (category == "nuke")
                        {
                            Nuke.Cast(ability, target, name);
                            if (AbilityDamage.CalculateDamage(ability, me, target) >= target.Health)
                            {
                                Utils.Sleep(ability.GetHitDelay(target, name) * 1000 + 500, target.Handle + "KillSteal");
                            }
                        }

                        if (category == "disable"
                            && Disables.DisablesMenuDictionary[name].Item(name + "minManaCheckCombo")
                                   .GetValue<Slider>()
                                   .Value < mana && !Disable.Cast(ability, target, name))
                        {
                            continue;
                        }

                        if (category == "slow"
                            && Slows.SlowsMenuDictionary[name].Item(name + "minManaCheckCombo").GetValue<Slider>().Value
                            < mana && !Slow.Cast(ability, target, name))
                        {
                            continue;
                        }

                        if (category == "harras"
                            && Harrases.HarrasesMenuDictionary[name].Item(name + "minManaCheckCombo")
                                   .GetValue<Slider>()
                                   .Value < mana && !Harras.Cast(ability, target, name))
                        {
                            continue;
                        }

                        if (category == "silence"
                            && Silences.SilencesMenuDictionary[name].Item(name + "minManaCheckCombo")
                                   .GetValue<Slider>()
                                   .Value < mana && !Silence.Cast(ability, target, name))
                        {
                            continue;
                        }

                        if (category == "special"
                            && Specials.SpecialsMenuDictionary[name].Item(name + "minManaCheckCombo")
                                   .GetValue<Slider>()
                                   .Value < mana && !Special.Cast(ability, target, name))
                        {
                            continue;
                        }

                        if (category == "buff"
                            && Buffs.BuffsMenuDictionary[name].Item(name + "minManaCheckCombo").GetValue<Slider>().Value
                            < mana
                            && (name == "item_armlet" || name == "item_satanic" || !Buff.Cast(ability, target, me, name)))
                        {
                            continue;
                        }

                        if (Utils.SleepCheck(ability.Handle.ToString()))
                        {
                            Variables.DealtDamage += AbilityDamage.CalculateDamage(ability, me, target);
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
                                Variables.EtherealHitTime =
                                    (float)
                                    (Utils.TickCount + me.GetTurnTime(target) * 1000
                                     + Prediction.CalculateReachTime(target, 1200, target.Position - me.Position)
                                     + ping * 2);
                                Utils.Sleep(
                                    me.GetTurnTime(target) * 1000 + 100
                                    + (MyHeroInfo.Position.Distance2D(target) / 1200) * 1000 + ping, 
                                    "calculate");
                                break;
                            case "tusk_snowball":
                                Utils.Sleep(
                                    me.GetTurnTime(target) * 1000
                                    + (MyHeroInfo.Position.Distance2D(target) / 675) * 1000, 
                                    "GlobalCasting");
                                break;
                            case "ancient_apparition_cold_feet":
                                coldFeetLastUse = Utils.TickCount + 4000;
                                break;
                        }

                        if (ability.ChannelTime(name) > 0)
                        {
                            Utils.Sleep(delay + (ability.ChannelTime(name) * 1000) / 3, "cancelorder");
                            Utils.Sleep(delay + (ability.ChannelTime(name) * 1000) / 4, "GlobalCasting");
                            Utils.Sleep(delay + (ability.ChannelTime(name) * 1000) / 4, "casting");
                        }

                        Utils.Sleep(delay, handleString);
                        Utils.Sleep(
                            ability.GetCastDelay(me, target, abilityName: name, useCastPoint: false) * 1000, 
                            "GlobalCasting");
                        Utils.Sleep(ability.GetHitDelay(target, name) * 1000, "calculate");
                        Utils.Sleep(
                            Math.Max(ability.GetCastDelay(me, target, useCastPoint: false, abilityName: name), 0.15)
                            * 1000 + Game.Ping, 

                            // + (Math.Max(me.Distance2D(target) - ability.GetCastRange(name) - 50, 0)
                            // / me.MovementSpeed) * 1000,
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

        /// <summary>
        ///     The find purification target.
        /// </summary>
        /// <param name="hero">
        ///     The hero.
        /// </param>
        /// <returns>
        ///     The <see cref="Unit" />.
        /// </returns>
        public static Unit FindPurificationTarget(Hero hero)
        {
            var target = AllyHeroes.UsableHeroes.Where(x => !x.IsMagicImmune()).MinOrDefault(x => x.Distance2D(hero));
            var creep = Creeps.All.Where(x => x.Team == AbilityMain.Me.Team).MinOrDefault(x => x.Distance2D(hero));
            return target != null && (creep == null || target.Distance2D(hero) < creep.Distance2D(hero))
                       ? (Unit)target
                       : creep;
        }

        #endregion
    }
}