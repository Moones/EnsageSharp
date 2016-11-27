namespace Ability.AbilityMenu.Menus.DisablesMenu
{
    using System.Collections.Generic;
    using System.Linq;

    using Ability.Drawings;
    using Ability.ObjectManager;

    using Ensage;
    using Ensage.Common.AbilityInfo;
    using Ensage.Common.Menu;

    internal class Disables
    {
        #region Static Fields

        public static Menu DisablesMenu;

        public static Dictionary<string, Menu> DisablesMenuDictionary;

        public static Dictionary<string, bool> DisablesToggler;

        public static bool DisablesTogglerCreated;

        #endregion

        #region Public Methods and Operators

        public static void AddAllDisables(Ability[] spells, Item[] myItems1)
        {
            DisablesToggler = new Dictionary<string, bool>();
            foreach (var spell in
                from spell in spells
                let data = AbilityDatabase.Find(spell.Name)
                where data != null && data.IsDisable
                select spell)
            {
                AddDisable(spell);
                RangeDrawing.AddRange(spell);
            }

            foreach (var spell in
                from spell in myItems1
                let data = AbilityDatabase.Find(spell.Name)
                where data != null && data.IsDisable
                select spell)
            {
                AddDisable(spell);
                RangeDrawing.AddRange(spell);
            }

            if (DisablesTogglerCreated)
            {
                MainMenu.Menu.AddItem(
                    new MenuItem("disablesToggler", "Disables:").SetValue(new AbilityToggler(DisablesToggler)));
            }
        }

        #endregion

        #region Methods

        private static void AddDisable(Ability spell)
        {
            MyAbilities.OffensiveAbilities.Add(spell.Name + "disable", spell);
            if (!DisablesTogglerCreated)
            {
                DisablesTogglerCreated = true;
                DisablesToggler.Add(spell.Name, true);
                MainMenu.AbilitiesMenu.AddSubMenu(DisablesMenu);
            }
            else
            {
                DisablesToggler.Add(spell.Name, true);
            }

            var menu = DisableMenu.Create(spell.Name, spell);
            DisablesMenuDictionary.Add(spell.Name, menu);
            DisablesMenu.AddSubMenu(menu);
        }

        #endregion
    }
}