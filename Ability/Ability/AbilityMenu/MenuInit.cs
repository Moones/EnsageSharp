namespace Ability.AbilityMenu
{
    using System.Collections.Generic;
    using System.Linq;

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

    using Ensage;
    using Ensage.Common.AbilityInfo;
    using Ensage.Common.Menu;
    using Ensage.Common.Objects;

    using SharpDX;

    internal class MenuInit
    {
        #region Public Methods and Operators

        public static void AddAllToMenu()
        {
            var mySpells = AbilityMain.Me.Spellbook.Spells.Where(x => AbilityDatabase.Find(x.Name) != null);
            var myItems = AbilityMain.Me.Inventory.Items;
            var spells = mySpells as Ability[] ?? mySpells.ToArray();
            var myItems1 = myItems as Item[] ?? myItems.ToArray();
            var blink = myItems1.FirstOrDefault(x => x.Name == "item_blink")
                        ?? spells.FirstOrDefault(x => x.Name == "antimage_blink" || x.Name == "queenofpain_blink");
            MyAbilities.SoulRing = myItems1.FirstOrDefault(x => x.Name == "item_soul_ring");
            MyAbilities.ForceStaff = myItems1.FirstOrDefault(x => x.Name == "item_force_staff");
            MyAbilities.ChargeOfDarkness = spells.FirstOrDefault(x => x.Name == "spirit_breaker_charge_of_darkness");
            MyAbilities.InvokerInvoke = AbilityMain.Me.Spellbook.Spells.FirstOrDefault(x => x.Name == "invoker_invoke");
            MyAbilities.InvokerQ = AbilityMain.Me.Spellbook.Spells.FirstOrDefault(x => x.Name == "invoker_quas");
            MyAbilities.InvokerW = AbilityMain.Me.Spellbook.Spells.FirstOrDefault(x => x.Name == "invoker_wex");
            MyAbilities.InvokerE = AbilityMain.Me.Spellbook.Spells.FirstOrDefault(x => x.Name == "invoker_exort");
            MainMenu.Menu.AddItem(new MenuItem("Ability#.EnableAutoKillSteal", "Enable AutoKillSteal:"))
                .SetValue(true)
                .SetFontStyle(fontColor: Color.DarkOrange);
            MainMenu.Menu.AddItem(new MenuItem("Ability#.EnableAutoUsage", "Enable AutoUsage:"))
                .SetValue(true)
                .SetFontStyle(fontColor: Color.DarkOrange);
            Nukes.AddAllNukes(spells, myItems1);
            Disables.AddAllDisables(spells, myItems1);
            Slows.AddAllSlows(spells, myItems1);
            Buffs.AddAllBuffs(spells, myItems1);
            Harrases.AddAllHarrases(spells, myItems1);
            Silences.AddAllSilences(spells, myItems1);
            Specials.AddAllSpecials(spells, myItems1);
            Heals.AddAllHeals(spells, myItems1);
            Shields.AddAllShields(spells, myItems1);
            MainMenu.Menu.AddSubMenu(MainMenu.AbilitiesMenu);
            MainMenu.DrawingsMenu.AddSubMenu(MainMenu.DamageIndicatorMenu);
            MainMenu.DrawingsMenu.AddSubMenu(MainMenu.GankDamageMenu);
            MainMenu.GankDamageMenu.AddItem(new MenuItem("enableGankDamage", "Enable: ").SetValue(true));
            MainMenu.GankDamageMenu.AddItem(
                new MenuItem("enableGankDamageAllies", "Enable for allies: ").SetValue(true));
            MainMenu.GankDamageMenu.AddItem(
                new MenuItem("enableGankDamageEnemies", "Enable for enemies: ").SetValue(true));
            MainMenu.DamageIndicatorMenu.AddItem(
                new MenuItem("abilityDamageIndicatorDrawDamage", "Draw Damage").SetValue(true));
            MainMenu.DamageIndicatorMenu.AddItem(
                new MenuItem("abilityDamageIndicatorDrawHits", "Draw hits").SetValue(true));
            MainMenu.DamageIndicatorMenu.AddItem(
                new MenuItem("abilityDamageIndicatorTextSize", "Increase text size").SetValue(new Slider(0, 0, 5)));
            MainMenu.DrawingsMenu.AddSubMenu(MainMenu.AbilityOverlayMenu);
            MainMenu.AbilityOverlayMenu.AddItem(
                new MenuItem("spellOverlay", "SPELLS OVERLAY:").SetFontStyle(fontColor: Color.White));
            MainMenu.AbilityOverlayMenu.AddItem(
                new MenuItem("enableSpellOverlayEnemy", "Enable for enemies: ").SetValue(true));
            MainMenu.AbilityOverlayMenu.AddItem(
                new MenuItem("enableSpellOverlayAlly", "Enable for allies: ").SetValue(true));
            MainMenu.AbilityOverlayMenu.AddItem(
                new MenuItem("sizeSliderSpell", "Increase the size: ").SetValue(new Slider(0, 0, 25)));
            MainMenu.AbilityOverlayMenu.AddItem(
                new MenuItem("itemOverlay", "ITEMS OVERLAY:").SetFontStyle(fontColor: Color.White));
            MainMenu.AbilityOverlayMenu.AddItem(
                new MenuItem("enableItemOverlayEnemy", "Enable for enemies: ").SetValue(true));
            MainMenu.AbilityOverlayMenu.AddItem(
                new MenuItem("enableItemOverlayAlly", "Enable for allies: ").SetValue(true));
            MainMenu.AbilityOverlayMenu.AddItem(
                new MenuItem("sizeSliderItem", "Increase the size: ").SetValue(new Slider(0, 0, 25)));
            MainMenu.ComboKeysMenu.AddItem(new MenuItem("unfinishedFeature", "This feature is currently unfinished!"))
                .SetFontStyle(fontColor: Color.OrangeRed)
                .SetTooltip("Contains unfinished stuff, not all combos are tweaked/in right order.");
            MainMenu.ComboKeysMenu.AddItem(new MenuItem("abilityKey1", "Combo Key"))
                .SetValue(new KeyBind('G', KeyBindType.Press));
            var priorityChanger =
                new MenuItem("Ability#.ComboOrder", "Custom Order: ").SetValue(
                    new PriorityChanger(new List<string>(), "Ability#.ComboOrder")).SetFontStyle(fontColor: Color.Gray);
            MainMenu.ComboKeysMenu.AddItem(new MenuItem("abilityComboType", "Combo Order"))
                .SetValue(new StringList(new[] { "Normal", "Maximum Disable", "Maximum Damage", "Custom" }))
                .ValueChanged +=
                (sender, args) =>
                    {
                        priorityChanger.SetFontStyle(
                            fontColor: args.GetNewValue<StringList>().SelectedIndex == 3 ? Color.Green : Color.Gray);
                    };

            if (MainMenu.ComboKeysMenu.Item("abilityComboType").GetValue<StringList>().SelectedIndex == 3)
            {
                priorityChanger.SetFontStyle(fontColor: Color.Green);
            }

            MainMenu.ComboKeysMenu.AddItem(new MenuItem("Ability.KeyCombo.Mode", "Move mode"))
                .SetValue(new StringList(new[] { "Orbwalk", "Move to mouse", "Attack target", "Do nothing" }));
            MainMenu.ComboKeysMenu.AddItem(new MenuItem("Ability.KeyCombo.NoMoveRange", "No move range"))
                .SetValue(new Slider(0, 0, 300))
                .SetTooltip("Your hero will not move if you put your mouse near your hero in the selected range");
            MainMenu.ComboKeysMenu.AddItem(new MenuItem("Ability.KeyCombo.Target", "Target selection"))
                .SetValue(new StringList(new[] { "ClosestToMouse", "FastestKillable" }));
            MainMenu.ComboKeysMenu.AddItem(new MenuItem("Ability.KeyCombo.TargetLock", "Target lock"))
                .SetValue(new StringList(new[] { "None", "WhenTheyGoToFog", "WhenKeyIsPressed" }));
            MainMenu.ComboKeysMenu.AddItem(
                new MenuItem("Ability#.MaximizeDamage", "Maximize damage (beta feature)").SetValue(false)
                    .SetFontStyle(fontColor: Color.Aqua)
                    .SetTooltip(
                        "Enabling this option will make sure that all damage/damage amplifying abilities are used right away"));
            MainMenu.ComboKeysMenu.AddItem(new MenuItem("comboAbilitiesToggler", "Abilities in combo: "))
                .SetValue(new AbilityToggler(new Dictionary<string, bool>()));
            MainMenu.ComboKeysMenu.AddItem(priorityChanger);

            foreach (var spell in
                from spell in AbilityMain.Me.Spellbook.Spells
                let data = AbilityDatabase.Find(spell.Name)
                where
                    (data != null
                     && (data.IsNuke || data.IsDisable || data.IsHarras || data.IsBuff || data.WeakensEnemy
                         || data.IsSlow || data.IsSilence)) || spell.Name == "tinker_rearm"
                    || spell.StoredName() == "invoker_emp" || spell.StoredName() == "invoker_forge_spirit"
                    || spell.StoredName() == "invoker_chaos_meteor"
                    || spell.StoredName() == "skywrath_mage_mystic_flare"
                select spell)
            {
                var dv = spell.AbilityType != AbilityType.Ultimate || spell.Name == "templar_assassin_psionic_trap";
                if (NameManager.Name(spell) == "tinker_rearm")
                {
                    MyAbilities.TinkerRearm = spell;
                }

                ComboMenu.AddAbility(spell.Name, spell, dv);
            }

            foreach (var spell in
                from spell in myItems1
                let data = AbilityDatabase.Find(spell.Name)
                where
                    data != null
                    && (data.IsNuke || data.IsDisable || data.IsHarras || data.IsBuff || data.WeakensEnemy
                        || data.IsSlow || data.IsSilence)
                select spell)
            {
                if (NameManager.Name(spell) == "item_cyclone")
                {
                    MyAbilities.Cyclone = spell;
                }

                ComboMenu.AddAbility(spell.Name, spell);
            }

            if (blink != null)
            {
                MyAbilities.Blink = blink;
                RangeDrawing.AddRange(blink);
                ComboMenu.AddAbility(blink.StoredName(), blink);
            }

            // if (blink != null)
            // {
            // MainMenu.ComboKeysMenu.Item("comboAbilitiesToggler").GetValue<AbilityToggler>().Add(blink.Name);
            // }
            MainMenu.OptionsMenu.AddSubMenu(MainMenu.ComboKeysMenu);
            MainMenu.OptionsMenu.AddSubMenu(MainMenu.DrawingsMenu);
            MainMenu.BlinkMenu.AddItem(
                new MenuItem("Ability#.BlinkRange", "BlinkPosition distance from target").SetValue(
                    new Slider(0, 0, 700)));
            MainMenu.BlinkMenu.AddItem(
                new MenuItem("Ability#.BlinkMaxEnemiesAround", "Maximum enemies around target to use blink").SetValue(
                    new Slider(2, 0, 3)))
                .SetTooltip("If theres more enemies around then specified value, blink will not be used");
            if (AbilityMain.Me.ClassID == ClassID.CDOTA_Unit_Hero_Invoker)
            {
                MainMenu.OptionsMenu.AddSubMenu(MainMenu.InvokerMenu);
                MainMenu.InvokerMenu.AddItem(
                    new MenuItem("Ability#.InvokerInvoke", "Enable AutoInvoke").SetValue(false));
            }

            MainMenu.OptionsMenu.AddSubMenu(MainMenu.BlinkMenu);
            MainMenu.Menu.AddSubMenu(MainMenu.OptionsMenu);
            MainMenu.Menu.AddSubMenu(MainMenu.RangeDrawingMenu);
        }

        #endregion
    }
}