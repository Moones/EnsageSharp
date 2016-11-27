namespace Ability.AbilityMenu.Menus.HarrasesMenu
{
    using System.Collections.Generic;
    using System.Linq;

    using Ability.Drawings;
    using Ability.ObjectManager;

    using Ensage;
    using Ensage.Common.AbilityInfo;
    using Ensage.Common.Menu;

    internal class Harrases
    {
        #region Static Fields

        public static Menu HarrasesMenu;

        public static Dictionary<string, Menu> HarrasesMenuDictionary;

        public static bool HarrasesTogglerCreated;

        private static Dictionary<string, bool> harrasesToggler;

        #endregion

        #region Public Methods and Operators

        public static void AddAllHarrases(Ability[] spells, Item[] myItems1)
        {
            harrasesToggler = new Dictionary<string, bool>();
            foreach (var spell in
                from spell in spells
                let data = AbilityDatabase.Find(spell.Name)
                where data != null && data.IsHarras
                select spell)
            {
                AddHarras(spell);
                RangeDrawing.AddRange(spell);
            }

            foreach (var spell in
                from spell in myItems1
                let data = AbilityDatabase.Find(spell.Name)
                where data != null && data.IsHarras
                select spell)
            {
                AddHarras(spell);
                RangeDrawing.AddRange(spell);
            }

            if (HarrasesTogglerCreated)
            {
                MainMenu.Menu.AddItem(
                    new MenuItem("harrasesToggler", "Harrases:").SetValue(new AbilityToggler(harrasesToggler)));
            }
        }

        #endregion

        #region Methods

        private static void AddHarras(Ability spell)
        {
            MyAbilities.OffensiveAbilities.Add(spell.Name + "harras", spell);
            if (!HarrasesTogglerCreated)
            {
                HarrasesTogglerCreated = true;
                harrasesToggler.Add(spell.Name, true);
                MainMenu.AbilitiesMenu.AddSubMenu(HarrasesMenu);
            }
            else
            {
                harrasesToggler.Add(spell.Name, true);
            }

            var menu = HarrasMenu.Create(spell.Name);
            HarrasesMenuDictionary.Add(spell.Name, menu);
            HarrasesMenu.AddSubMenu(menu);
        }

        #endregion
    }
}