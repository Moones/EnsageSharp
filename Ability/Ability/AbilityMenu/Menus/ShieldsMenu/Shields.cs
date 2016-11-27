namespace Ability.AbilityMenu.Menus.ShieldsMenu
{
    using System.Collections.Generic;
    using System.Linq;

    using Ability.Drawings;
    using Ability.ObjectManager;

    using Ensage;
    using Ensage.Common.AbilityInfo;
    using Ensage.Common.Menu;

    internal class Shields
    {
        #region Static Fields

        public static Menu ShieldsMenu;

        public static Dictionary<string, Menu> ShieldsMenuDictionary;

        public static Dictionary<string, bool> ShieldsToggler;

        public static bool ShieldsTogglerCreated;

        #endregion

        #region Public Methods and Operators

        public static void AddAllShields(Ability[] spells, Item[] myItems1)
        {
            ShieldsToggler = new Dictionary<string, bool>();
            foreach (var spell in
                from spell in spells
                let data = AbilityDatabase.Find(spell.Name)
                where data != null && data.IsShield
                select spell)
            {
                AddShield(spell);
                RangeDrawing.AddRange(spell);
            }

            foreach (var spell in
                from spell in myItems1
                let data = AbilityDatabase.Find(spell.Name)
                where data != null && data.IsShield
                select spell)
            {
                AddShield(spell);
                RangeDrawing.AddRange(spell);
            }

            if (ShieldsTogglerCreated)
            {
                MainMenu.Menu.AddItem(
                    new MenuItem("shieldsToggler", "Shields:").SetValue(new AbilityToggler(ShieldsToggler)));
            }
        }

        #endregion

        #region Methods

        private static void AddShield(Ability spell)
        {
            MyAbilities.DefensiveAbilities.Add(spell.Name + "shield", spell);
            if (!ShieldsTogglerCreated)
            {
                ShieldsTogglerCreated = true;
                ShieldsToggler.Add(spell.Name, true);
                MainMenu.AbilitiesMenu.AddSubMenu(ShieldsMenu);
            }
            else
            {
                ShieldsToggler.Add(spell.Name, true);
            }

            var menu = ShieldMenu.Create(spell.Name, spell);
            ShieldsMenuDictionary.Add(spell.Name, menu);
            ShieldsMenu.AddSubMenu(menu);
        }

        #endregion
    }
}