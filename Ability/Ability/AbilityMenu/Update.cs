namespace Ability.AbilityMenu
{
    using System;
    using System.Collections.Generic;

    using Ability.AbilityMenu.Menus.BuffsMenu;
    using Ability.AbilityMenu.Menus.DisablesMenu;
    using Ability.AbilityMenu.Menus.HarrasesMenu;
    using Ability.AbilityMenu.Menus.HealsMenu;
    using Ability.AbilityMenu.Menus.NukesMenu;
    using Ability.AbilityMenu.Menus.ShieldsMenu;
    using Ability.AbilityMenu.Menus.SilencesMenu;
    using Ability.AbilityMenu.Menus.SlowsMenu;
    using Ability.AbilityMenu.Menus.SpecialsMenu;
    using Ability.Drawings;
    using Ability.ObjectManager;
    using Ability.ObjectManager.Heroes;
    using Ability.OnUpdate;

    using Ensage;
    using Ensage.Common;
    using Ensage.Common.AbilityInfo;
    using Ensage.Common.Menu;
    using Ensage.Common.Objects;

    internal class Update
    {
        #region Public Methods and Operators

        public static void ObjectMgr_OnRemoveEntity(EntityEventArgs args)
        {
            try
            {
                var a = args.Entity;
                if (a == null || NameManager.Name(a) == null || MainMenu.Menu == null)
                {
                    return;
                }

                var name = NameManager.Name(a);
                var data = AbilityDatabase.Find(name);
                if (data == null || a.Owner == null || !a.Owner.Equals(AbilityMain.Me))
                {
                    return;
                }

                MainMenu.ComboKeysMenu.Item("comboAbilitiesToggler").GetValue<AbilityToggler>().Remove(name);
                MainMenu.ComboKeysMenu.Item("Ability#.ComboOrder").GetValue<PriorityChanger>().Remove(name);

                // if (AbilityMain.Me.ClassID == ClassID.CDOTA_Unit_Hero_Rubick)
                // {
                // if (!Rubick.MyCdDictionary.ContainsKey(name))
                // {
                // Rubick.MyCdDictionary.Add(name, Utils.TickCount + ((a as Ability).Cooldown * 1000));
                // }
                // }
                RangeDrawing.RemoveRange(a as Ability);
                if (data.IsPurge || data.WeakensEnemy || data.TrueSight)
                {
                    MainMenu.Menu.Item("specialsToggler").GetValue<AbilityToggler>().Remove(name);
                    Specials.SpecialsMenuDictionary.Remove(name);
                    Specials.SpecialsMenu.RemoveSubMenu(name);
                    MyAbilities.OffensiveAbilities.Remove(name + "special");
                }

                if (data.IsBuff)
                {
                    MainMenu.Menu.Item("buffsToggler").GetValue<AbilityToggler>().Remove(name);
                    Buffs.BuffsMenuDictionary.Remove(name);
                    Buffs.BuffsMenu.RemoveSubMenu(name);
                    MyAbilities.OffensiveAbilities.Remove(name + "buff");
                }

                if (data.IsNuke || name == "item_urn_of_shadows")
                {
                    MainMenu.Menu.Item("nukesToggler").GetValue<AbilityToggler>().Remove(name);
                    Nukes.NukesMenuDictionary.Remove(name);
                    Nukes.NukesMenu.RemoveSubMenu(name);
                    MyAbilities.OffensiveAbilities.Remove(name + "nuke");
                }

                if (data.IsDisable)
                {
                    MainMenu.Menu.Item("disablesToggler").GetValue<AbilityToggler>().Remove(name);
                    Disables.DisablesMenuDictionary.Remove(name);
                    Disables.DisablesMenu.RemoveSubMenu(name);
                    MyAbilities.OffensiveAbilities.Remove(name + "disable");
                }

                if (data.IsSlow)
                {
                    MainMenu.Menu.Item("slowsToggler").GetValue<AbilityToggler>().Remove(name);
                    Slows.SlowsMenuDictionary.Remove(name);
                    Slows.SlowsMenu.RemoveSubMenu(name);
                    MyAbilities.OffensiveAbilities.Remove(name + "slow");
                }

                if (data.IsHarras)
                {
                    MainMenu.Menu.Item("harrasesToggler").GetValue<AbilityToggler>().Remove(name);
                    Harrases.HarrasesMenuDictionary.Remove(name);
                    Harrases.HarrasesMenu.RemoveSubMenu(name);
                    MyAbilities.OffensiveAbilities.Remove(name + "harras");
                }

                if (data.IsSilence)
                {
                    MainMenu.Menu.Item("silencesToggler").GetValue<AbilityToggler>().Remove(name);
                    Silences.SilencesMenuDictionary.Remove(name);
                    Silences.SilencesMenu.RemoveSubMenu(name);
                    MyAbilities.OffensiveAbilities.Remove(name + "silence");
                }

                if (data.IsHeal)
                {
                    MainMenu.Menu.Item("healsToggler").GetValue<AbilityToggler>().Remove(name);
                    Heals.HealsMenuDictionary.Remove(name);
                    Heals.HealsMenu.RemoveSubMenu(name);
                    MyAbilities.DefensiveAbilities.Remove(name + "heal");
                }

                if (data.IsShield)
                {
                    MainMenu.Menu.Item("shieldsToggler").GetValue<AbilityToggler>().Remove(name);
                    Shields.ShieldsMenuDictionary.Remove(name);
                    Shields.ShieldsMenu.RemoveSubMenu(name);
                    MyAbilities.DefensiveAbilities.Remove(name + "shield");
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
        }

        public static void UpdateItems(EventArgs args)
        {
            if (!OnUpdateChecks.CanUpdate())
            {
                return;
            }

            try
            {
                if (!Utils.SleepCheck("checkitems"))
                {
                    return;
                }

                foreach (var item in
                    AbilityMain.Me.Inventory.Items)
                {
                    switch (NameManager.Name(item))
                    {
                        case "item_gem":
                            continue;
                        case "item_soul_ring":
                            MyAbilities.SoulRing = item;
                            break;
                        case "item_aether_lens":

                            RangeDrawing.Update();
                            break;
                        case "item_cyclone":
                            MyAbilities.Cyclone = item;
                            break;
                        case "item_force_staff":
                            MyAbilities.ForceStaff = item;
                            break;
                        case "item_blink":
                            if (MyAbilities.Blink != null && MyAbilities.Blink.IsValid)
                            {
                                continue;
                            }

                            ComboMenu.AddAbility(item.StoredName(), item);
                            RangeDrawing.AddRange(item);
                            MyAbilities.Blink = item;
                            continue;
                    }

                    var data = AbilityDatabase.Find(NameManager.Name(item));

                    if (data == null)
                    {
                        continue;
                    }

                    if (!MyAbilities.OffensiveAbilities.ContainsValue(item))
                    {
                        var added = false;
                        if (data.IsSlow)
                        {
                            AddNewSlow(item);
                            added = true;
                        }

                        if (data.IsHarras)
                        {
                            AddNewHarras(item);
                            added = true;
                        }

                        if (data.IsBuff)
                        {
                            AddNewBuff(item);
                            added = true;
                        }

                        if (data.IsSilence)
                        {
                            AddNewSilence(item);
                            added = true;
                        }

                        if (data.TrueSight || data.WeakensEnemy || data.IsPurge)
                        {
                            AddNewSpecial(item, data);
                            added = true;
                        }

                        if (data.IsDisable)
                        {
                            AddNewDisable(item);
                            added = true;
                        }

                        if (data.IsNuke || item.Name == "item_urn_of_shadows")
                        {
                            AddNewNuke(item);
                            added = true;
                        }

                        if (added)
                        {
                            RangeDrawing.AddRange(item);
                            ComboMenu.AddAbility(item.Name, item);
                        }
                    }

                    // Console.WriteLine(!MyAbilities.DefensiveAbilities.ContainsValue(item) + " " + NameManager.Name(item) + " " + data.IsHeal);
                    if (!MyAbilities.DefensiveAbilities.ContainsValue(item))
                    {
                        if (data.IsHeal)
                        {
                            AddNewHeal(item);
                        }

                        if (data.IsShield)
                        {
                            AddNewShield(item);
                        }
                    }
                }

                if (AbilityMain.Me.ClassID == ClassID.CDOTA_Unit_Hero_Rubick)
                {
                    foreach (var item in AllyHeroes.AbilityDictionary[NameManager.Name(AbilityMain.Me)])
                    {
                        var data = AbilityDatabase.Find(NameManager.Name(item));

                        if (data == null)
                        {
                            continue;
                        }

                        if (item.StoredName() == "spirit_breaker_charge_of_darkness")
                        {
                            MyAbilities.ChargeOfDarkness = item;
                        }

                        if (!MyAbilities.OffensiveAbilities.ContainsValue(item))
                        {
                            var added = false;
                            if (data.IsSlow)
                            {
                                AddNewSlow(item);
                                added = true;
                            }

                            if (data.IsHarras)
                            {
                                AddNewHarras(item);
                                added = true;
                            }

                            if (data.IsBuff)
                            {
                                AddNewBuff(item);
                                added = true;
                            }

                            if (data.IsSilence)
                            {
                                AddNewSilence(item);
                                added = true;
                            }

                            if (data.TrueSight || data.WeakensEnemy || data.IsPurge)
                            {
                                AddNewSpecial(item, data);
                                added = true;
                            }

                            if (data.IsDisable)
                            {
                                AddNewDisable(item);
                                added = true;
                            }

                            if (data.IsNuke)
                            {
                                AddNewNuke(item);
                                added = true;
                            }

                            if (added)
                            {
                                ComboMenu.AddAbility(item.Name, item);
                            }
                        }

                        // Console.WriteLine(!MyAbilities.DefensiveAbilities.ContainsValue(item) + " " + NameManager.Name(item) + " " + data.IsHeal);
                        if (!MyAbilities.DefensiveAbilities.ContainsValue(item))
                        {
                            if (data.IsHeal)
                            {
                                AddNewHeal(item);
                            }

                            if (data.IsShield)
                            {
                                AddNewShield(item);
                            }
                        }
                    }
                }

                Utils.Sleep(1000, "checkitems");
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
        }

        #endregion

        #region Methods

        private static void AddNewBuff(Ability item)
        {
            MyAbilities.OffensiveAbilities.Add(NameManager.Name(item) + "buff", item);
            if (!Buffs.BuffsTogglerCreated)
            {
                Buffs.BuffsTogglerCreated = true;
                MainMenu.Menu.AddItem(
                    new MenuItem("buffsToggler", "Buffs:").SetValue(new AbilityToggler(new Dictionary<string, bool>())));
                MainMenu.Menu.Item("buffsToggler").GetValue<AbilityToggler>().Add(NameManager.Name(item));
                MainMenu.AbilitiesMenu.AddSubMenu(Buffs.BuffsMenu);
            }
            else
            {
                MainMenu.Menu.Item("buffsToggler").GetValue<AbilityToggler>().Add(NameManager.Name(item));
            }

            var menu = BuffMenu.Create(NameManager.Name(item));
            Buffs.BuffsMenuDictionary.Add(NameManager.Name(item), menu);
            Buffs.BuffsMenu.AddSubMenu(menu);
        }

        private static void AddNewDisable(Ability item)
        {
            MyAbilities.OffensiveAbilities.Add(NameManager.Name(item) + "disable", item);
            if (!Disables.DisablesTogglerCreated)
            {
                Disables.DisablesTogglerCreated = true;
                MainMenu.Menu.AddItem(
                    new MenuItem("disablesToggler", "Disables:").SetValue(
                        new AbilityToggler(new Dictionary<string, bool>())));
                MainMenu.Menu.Item("disablesToggler").GetValue<AbilityToggler>().Add(NameManager.Name(item));
                MainMenu.AbilitiesMenu.AddSubMenu(Disables.DisablesMenu);
            }
            else
            {
                MainMenu.Menu.Item("disablesToggler").GetValue<AbilityToggler>().Add(NameManager.Name(item));
            }

            var menu = DisableMenu.Create(NameManager.Name(item), item);
            Disables.DisablesMenuDictionary.Add(NameManager.Name(item), menu);
            Disables.DisablesMenu.AddSubMenu(menu);
        }

        private static void AddNewHarras(Ability item)
        {
            MyAbilities.OffensiveAbilities.Add(NameManager.Name(item) + "harras", item);
            if (!Harrases.HarrasesTogglerCreated)
            {
                Harrases.HarrasesTogglerCreated = true;
                MainMenu.Menu.AddItem(
                    new MenuItem("harrasesToggler", "Harrases:").SetValue(
                        new AbilityToggler(new Dictionary<string, bool>())));
                MainMenu.Menu.Item("harrasesToggler").GetValue<AbilityToggler>().Add(NameManager.Name(item));
                MainMenu.AbilitiesMenu.AddSubMenu(Harrases.HarrasesMenu);
            }
            else
            {
                MainMenu.Menu.Item("harrasesToggler").GetValue<AbilityToggler>().Add(NameManager.Name(item));
            }

            var menu = HarrasMenu.Create(NameManager.Name(item));
            Harrases.HarrasesMenuDictionary.Add(NameManager.Name(item), menu);
            Harrases.HarrasesMenu.AddSubMenu(menu);
        }

        private static void AddNewHeal(Ability item)
        {
            if (NameManager.Name(item) != "item_soul_ring")
            {
                MyAbilities.DefensiveAbilities.Add(NameManager.Name(item) + "heal", item);
            }
            else if (Heals.HealsTogglerCreated
                     && MainMenu.Menu.Item("healsToggler")
                            .GetValue<AbilityToggler>()
                            .Dictionary.ContainsKey(NameManager.Name(item)))
            {
                return;
            }

            if (!Heals.HealsTogglerCreated)
            {
                Heals.HealsTogglerCreated = true;
                MainMenu.Menu.AddItem(
                    new MenuItem("healsToggler", "Heals:").SetValue(new AbilityToggler(new Dictionary<string, bool>())));
                MainMenu.Menu.Item("healsToggler").GetValue<AbilityToggler>().Add(NameManager.Name(item));
                MainMenu.AbilitiesMenu.AddSubMenu(Heals.HealsMenu);
            }
            else
            {
                MainMenu.Menu.Item("healsToggler").GetValue<AbilityToggler>().Add(NameManager.Name(item));
            }

            var menu = HealMenu.Create(NameManager.Name(item));
            Heals.HealsMenuDictionary.Add(NameManager.Name(item), menu);
            Heals.HealsMenu.AddSubMenu(menu);
        }

        private static void AddNewNuke(Ability item)
        {
            MyAbilities.OffensiveAbilities.Add(NameManager.Name(item) + "nuke", item);
            if (!Nukes.NukesTogglerCreated)
            {
                Nukes.NukesTogglerCreated = true;
                MainMenu.Menu.AddItem(
                    new MenuItem("nukesToggler", "Nukes:").SetValue(new AbilityToggler(new Dictionary<string, bool>())));
                MainMenu.Menu.Item("nukesToggler").GetValue<AbilityToggler>().Add(NameManager.Name(item));
                MainMenu.AbilitiesMenu.AddSubMenu(Nukes.NukesMenu);
            }
            else
            {
                MainMenu.Menu.Item("nukesToggler").GetValue<AbilityToggler>().Add(NameManager.Name(item));
            }

            var menu = NukeMenu.Create(NameManager.Name(item), item);
            Nukes.NukesMenuDictionary.Add(NameManager.Name(item), menu);
            Nukes.NukesMenu.AddSubMenu(menu);
        }

        private static void AddNewShield(Ability item)
        {
            MyAbilities.DefensiveAbilities.Add(NameManager.Name(item) + "shield", item);
            if (!Shields.ShieldsTogglerCreated)
            {
                Shields.ShieldsTogglerCreated = true;
                MainMenu.Menu.AddItem(
                    new MenuItem("shieldsToggler", "Shields:").SetValue(
                        new AbilityToggler(new Dictionary<string, bool>())));
                MainMenu.Menu.Item("shieldsToggler").GetValue<AbilityToggler>().Add(NameManager.Name(item));
                MainMenu.AbilitiesMenu.AddSubMenu(Shields.ShieldsMenu);
            }
            else
            {
                MainMenu.Menu.Item("shieldsToggler").GetValue<AbilityToggler>().Add(NameManager.Name(item));
            }

            var menu = ShieldMenu.Create(NameManager.Name(item), item);
            Shields.ShieldsMenuDictionary.Add(NameManager.Name(item), menu);
            Shields.ShieldsMenu.AddSubMenu(menu);
        }

        private static void AddNewSilence(Ability item)
        {
            MyAbilities.OffensiveAbilities.Add(NameManager.Name(item) + "silence", item);
            if (!Silences.SilencesTogglerCreated)
            {
                Silences.SilencesTogglerCreated = true;
                MainMenu.Menu.AddItem(
                    new MenuItem("silencesToggler", "Silences:").SetValue(
                        new AbilityToggler(new Dictionary<string, bool>())));
                MainMenu.Menu.Item("silencesToggler").GetValue<AbilityToggler>().Add(NameManager.Name(item));
                MainMenu.AbilitiesMenu.AddSubMenu(Silences.SilencesMenu);
            }
            else
            {
                MainMenu.Menu.Item("silencesToggler").GetValue<AbilityToggler>().Add(NameManager.Name(item));
            }

            var menu = SilenceMenu.Create(NameManager.Name(item));
            Silences.SilencesMenuDictionary.Add(NameManager.Name(item), menu);
            Silences.SilencesMenu.AddSubMenu(menu);
        }

        private static void AddNewSlow(Ability item)
        {
            MyAbilities.OffensiveAbilities.Add(NameManager.Name(item) + "slow", item);
            if (!Slows.SlowsTogglerCreated)
            {
                Slows.SlowsTogglerCreated = true;
                MainMenu.Menu.AddItem(
                    new MenuItem("slowsToggler", "Slows:").SetValue(new AbilityToggler(new Dictionary<string, bool>())));
                MainMenu.Menu.Item("slowsToggler").GetValue<AbilityToggler>().Add(NameManager.Name(item));
                MainMenu.AbilitiesMenu.AddSubMenu(Slows.SlowsMenu);
            }
            else
            {
                MainMenu.Menu.Item("slowsToggler").GetValue<AbilityToggler>().Add(NameManager.Name(item));
            }

            var menu = SlowMenu.Create(NameManager.Name(item));
            Slows.SlowsMenuDictionary.Add(NameManager.Name(item), menu);
            Slows.SlowsMenu.AddSubMenu(menu);
        }

        private static void AddNewSpecial(Ability item, AbilityInfo data)
        {
            MyAbilities.OffensiveAbilities.Add(NameManager.Name(item) + "special", item);
            if (!Specials.SpecialsTogglerCreated)
            {
                Specials.SpecialsTogglerCreated = true;
                MainMenu.Menu.AddItem(
                    new MenuItem("specialsToggler", "Specials:").SetValue(
                        new AbilityToggler(new Dictionary<string, bool>())));
                MainMenu.Menu.Item("specialsToggler").GetValue<AbilityToggler>().Add(NameManager.Name(item));
                MainMenu.AbilitiesMenu.AddSubMenu(Specials.SpecialsMenu);
            }
            else
            {
                MainMenu.Menu.Item("specialsToggler").GetValue<AbilityToggler>().Add(NameManager.Name(item));
            }

            var menu = SpecialMenu.Create(NameManager.Name(item), item);
            Specials.SpecialsMenuDictionary.Add(NameManager.Name(item), menu);
            Specials.SpecialsMenu.AddSubMenu(menu);
        }

        #endregion
    }
}