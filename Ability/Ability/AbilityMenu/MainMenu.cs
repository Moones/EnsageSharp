namespace Ability.AbilityMenu
{
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

    using Ensage;
    using Ensage.Common.Menu;

    internal class MainMenu
    {
        #region Static Fields

        public static Menu AbilitiesMenu;

        public static Menu AbilityOverlayMenu;

        public static Menu BlinkMenu;

        public static Menu ComboKeysMenu;

        public static Menu DamageIndicatorMenu;

        public static Menu DrawingsMenu;

        public static Menu GankDamageMenu;

        public static Menu InvokerMenu;

        public static Menu Menu;

        public static Menu OptionsMenu;

        public static Menu RangeDrawingMenu;

        #endregion

        #region Public Methods and Operators

        public static void InitializeMenu()
        {
            Menu = new Menu("Ability#", "abilityMenu", true);
            AbilitiesMenu = new Menu("Abilities Menu", "abilitiesMenu" + AbilityMain.Me.Name);
            Nukes.NukesMenu = new Menu("Nukes", "nukesMenu" + AbilityMain.Me.Name);
            Disables.DisablesMenu = new Menu("Disables", "disablesMenu" + AbilityMain.Me.Name);
            Slows.SlowsMenu = new Menu("Slows", "slowsMenu" + AbilityMain.Me.Name);
            Buffs.BuffsMenu = new Menu("Buffs", "buffsMenu" + AbilityMain.Me.Name);
            Harrases.HarrasesMenu = new Menu("Harrases", "harrasesMenu" + AbilityMain.Me.Name);
            Specials.SpecialsMenu = new Menu("Specials", "specialsMenu" + AbilityMain.Me.Name);
            Heals.HealsMenu = new Menu("Heals", "healsMenu" + AbilityMain.Me.Name);
            Shields.ShieldsMenu = new Menu("Shields", "shieldsMenu" + AbilityMain.Me.Name);
            DrawingsMenu = new Menu("Drawings", "drawingsMenu");
            Silences.SilencesMenu = new Menu("Silences", "silencesMenu" + AbilityMain.Me.Name);
            BlinkMenu = new Menu("Blink Configuration", "Ability#.Blink");
            OptionsMenu = new Menu("Options", "abilityOptions");
            if (AbilityMain.Me.ClassID == ClassID.CDOTA_Unit_Hero_Invoker)
            {
                InvokerMenu = new Menu("Invoker Configuration", "Ability#.InvokerConfig");
            }

            DamageIndicatorMenu = new Menu("DamageIndicator", "abilityDamageIndicator");
            GankDamageMenu = new Menu("GankDamage", "abilityGankDamage");
            AbilityOverlayMenu = new Menu("Ability Overlay", "abilityOverlayMenu");
            ComboKeysMenu = new Menu("Combo Keys", "comboKeysMenu");
            RangeDrawingMenu = new Menu("Ability Ranges", "Ability.RangeMenu");
            Menu.AddToMainMenu();
            Nukes.NukesMenuDictionary = new Dictionary<string, Menu>();
            Disables.DisablesMenuDictionary = new Dictionary<string, Menu>();
            Slows.SlowsMenuDictionary = new Dictionary<string, Menu>();
            Buffs.BuffsMenuDictionary = new Dictionary<string, Menu>();
            Harrases.HarrasesMenuDictionary = new Dictionary<string, Menu>();
            Specials.SpecialsMenuDictionary = new Dictionary<string, Menu>();
            Silences.SilencesMenuDictionary = new Dictionary<string, Menu>();
            Heals.HealsMenuDictionary = new Dictionary<string, Menu>();
            Shields.ShieldsMenuDictionary = new Dictionary<string, Menu>();
            Nukes.NukesTogglerCreated = false;
            Disables.DisablesTogglerCreated = false;
            Slows.SlowsTogglerCreated = false;
            Buffs.BuffsTogglerCreated = false;
            Harrases.HarrasesTogglerCreated = false;
            Specials.SpecialsTogglerCreated = false;
            Silences.SilencesTogglerCreated = false;
            Heals.HealsTogglerCreated = false;
            Shields.ShieldsTogglerCreated = false;
        }

        public static void RestartMenu()
        {
            if (Menu == null)
            {
                return;
            }

            Menu.RemoveFromMainMenu();
            Menu = null;
        }

        #endregion
    }
}