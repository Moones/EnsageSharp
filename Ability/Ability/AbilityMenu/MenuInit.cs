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
    using Ability.ObjectManager;

    using Ensage;
    using Ensage.Common.AbilityInfo;
    using Ensage.Common.Menu;

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
            var blink = myItems1.FirstOrDefault(x => x.Name == "item_blink");
            MyAbilities.SoulRing = myItems1.FirstOrDefault(x => x.Name == "item_soul_ring");
            if (blink != null)
            {
                MyAbilities.Blink = blink;
                BlinkMenu.Create(blink);
            }

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
            MainMenu.ComboKeysMenu.AddItem(new MenuItem("abilityComboType", "Combo Order"))
                .SetValue(new StringList(new[] { "Normal", "Maximum Disable", "Maximum Damage" }));
            MainMenu.ComboKeysMenu.AddItem(new MenuItem("comboAbilitiesToggler", "Abilities in combo: "))
                .SetValue(new AbilityToggler(new Dictionary<string, bool>()));
            foreach (var spell in
                from spell in spells
                let data = AbilityDatabase.Find(spell.Name)
                where
                    data != null
                    && (data.IsNuke || data.IsDisable || data.IsHarras || data.IsBuff || data.WeakensEnemy
                        || data.IsSlow || data.IsSilence)
                select spell)
            {
                var dv = spell.AbilityType != AbilityType.Ultimate || spell.Name == "templar_assassin_psionic_trap";
                ComboMenu.AddAbility(spell.Name, dv);
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
                ComboMenu.AddAbility(spell.Name);
            }

            // if (blink != null)
            // {
            // MainMenu.ComboKeysMenu.Item("comboAbilitiesToggler").GetValue<AbilityToggler>().Add(blink.Name);
            // }
            MainMenu.OptionsMenu.AddSubMenu(MainMenu.ComboKeysMenu);
            MainMenu.OptionsMenu.AddSubMenu(MainMenu.DrawingsMenu);
            MainMenu.Menu.AddSubMenu(MainMenu.OptionsMenu);
            MainMenu.Menu.AddSubMenu(MainMenu.RangeDrawingMenu);
        }

        #endregion
    }
}