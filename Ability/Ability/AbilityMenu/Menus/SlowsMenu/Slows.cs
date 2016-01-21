namespace Ability.AbilityMenu.Menus.SlowsMenu
{
    using System.Collections.Generic;
    using System.Linq;

    using Ability.Drawings;
    using Ability.ObjectManager;

    using Ensage;
    using Ensage.Common.AbilityInfo;
    using Ensage.Common.Menu;

    internal class Slows
    {
        #region Static Fields

        public static Menu SlowsMenu;

        public static Dictionary<string, Menu> SlowsMenuDictionary;

        public static Dictionary<string, bool> SlowsToggler;

        public static bool SlowsTogglerCreated;

        #endregion

        #region Public Methods and Operators

        public static void AddAllSlows(Ability[] spells, Item[] myItems1)
        {
            SlowsToggler = new Dictionary<string, bool>();
            foreach (var spell in
                from spell in spells
                let data = AbilityDatabase.Find(spell.Name)
                where data != null && data.IsSlow
                select spell)
            {
                AddSlow(spell);
                RangeDrawing.AddRange(spell);
            }

            foreach (var spell in
                from spell in myItems1
                let data = AbilityDatabase.Find(spell.Name)
                where data != null && data.IsSlow
                select spell)
            {
                AddSlow(spell);
                RangeDrawing.AddRange(spell);
            }

            if (SlowsTogglerCreated)
            {
                MainMenu.Menu.AddItem(new MenuItem("slowsToggler", "Slows:").SetValue(new AbilityToggler(SlowsToggler)));
            }
        }

        #endregion

        #region Methods

        private static void AddSlow(Ability spell)
        {
            MyAbilities.OffensiveAbilities.Add(spell.Name + "slow", spell);
            if (!SlowsTogglerCreated)
            {
                SlowsTogglerCreated = true;
                SlowsToggler.Add(spell.Name, true);
                MainMenu.AbilitiesMenu.AddSubMenu(SlowsMenu);
            }
            else
            {
                SlowsToggler.Add(spell.Name, true);
            }

            var menu = SlowMenu.Create(spell.Name);
            SlowsMenuDictionary.Add(spell.Name, menu);
            SlowsMenu.AddSubMenu(menu);
        }

        #endregion
    }
}