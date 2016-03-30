namespace Ability.AbilityMenu.Menus.NukesMenu
{
    using System.Collections.Generic;
    using System.Linq;

    using Ability.Drawings;
    using Ability.ObjectManager;

    using Ensage;
    using Ensage.Common.AbilityInfo;
    using Ensage.Common.Menu;
    using Ensage.Common.Objects;

    internal class Nukes
    {
        #region Static Fields

        public static Menu NukesMenu;

        public static Dictionary<string, Menu> NukesMenuDictionary;

        public static Dictionary<string, bool> NukesToggler;

        public static bool NukesTogglerCreated;

        #endregion

        #region Public Methods and Operators

        public static void AddAllNukes(Ability[] spells, Item[] myItems1)
        {
            NukesToggler = new Dictionary<string, bool>();
            foreach (var spell in
                from spell in spells
                let data = AbilityDatabase.Find(spell.Name)
                where data != null && data.IsNuke
                select spell)
            {
                AddNuke(spell);
                RangeDrawing.AddRange(spell);
            }

            foreach (var spell in
                from spell in myItems1
                let data = AbilityDatabase.Find(spell.Name)
                where data != null && (data.IsNuke || spell.StoredName() == "item_urn_of_shadows")
                select spell)
            {
                AddNuke(spell);
                RangeDrawing.AddRange(spell);
            }

            if (NukesTogglerCreated)
            {
                MainMenu.Menu.AddItem(new MenuItem("nukesToggler", "Nukes:").SetValue(new AbilityToggler(NukesToggler)));
            }
        }

        #endregion

        #region Methods

        private static void AddNuke(Ability spell)
        {
            MyAbilities.OffensiveAbilities.Add(spell.Name + "nuke", spell);
            if (!NukesTogglerCreated)
            {
                NukesTogglerCreated = true;
                NukesToggler.Add(spell.Name, true);
                MainMenu.AbilitiesMenu.AddSubMenu(NukesMenu);
            }
            else
            {
                NukesToggler.Add(spell.Name, true);
            }

            var menu = NukeMenu.Create(spell.Name, spell);
            NukesMenuDictionary.Add(spell.Name, menu);
            NukesMenu.AddSubMenu(menu);
        }

        #endregion
    }
}