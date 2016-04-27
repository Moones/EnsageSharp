namespace Ability.AbilityMenu.Menus.HealsMenu
{
    using System.Collections.Generic;
    using System.Linq;

    using Ability.Drawings;
    using Ability.ObjectManager;

    using Ensage;
    using Ensage.Common.AbilityInfo;
    using Ensage.Common.Menu;

    internal class Heals
    {
        #region Static Fields

        public static Menu HealsMenu;

        public static Dictionary<string, Menu> HealsMenuDictionary;

        public static Dictionary<string, bool> HealsToggler;

        public static bool HealsTogglerCreated;

        #endregion

        #region Public Methods and Operators

        public static void AddAllHeals(Ability[] spells, Item[] myItems1)
        {
            HealsToggler = new Dictionary<string, bool>();
            foreach (var spell in
                from spell in spells
                let data = AbilityDatabase.Find(spell.Name)
                where data != null && data.IsHeal
                select spell)
            {
                AddHeal(spell);
                RangeDrawing.AddRange(spell);
            }

            foreach (var spell in
                from spell in myItems1
                let data = AbilityDatabase.Find(spell.Name)
                where data != null && data.IsHeal
                select spell)
            {
                if (MyAbilities.DefensiveAbilities.ContainsKey(spell.Name + "heal"))
                {
                    continue;
                }

                AddHeal(spell);
                RangeDrawing.AddRange(spell);
            }

            if (HealsTogglerCreated)
            {
                MainMenu.Menu.AddItem(new MenuItem("healsToggler", "Heals:").SetValue(new AbilityToggler(HealsToggler)));
            }
        }

        #endregion

        #region Methods

        private static void AddHeal(Ability spell)
        {
            if (spell.Name != "item_soul_ring")
            {
                MyAbilities.DefensiveAbilities.Add(spell.Name + "heal", spell);
            }

            if (!HealsTogglerCreated)
            {
                HealsTogglerCreated = true;
                HealsToggler.Add(spell.Name, true);
                MainMenu.AbilitiesMenu.AddSubMenu(HealsMenu);
            }
            else
            {
                HealsToggler.Add(spell.Name, true);
            }

            var menu = HealMenu.Create(spell.Name);
            HealsMenuDictionary.Add(spell.Name, menu);
            HealsMenu.AddSubMenu(menu);
        }

        #endregion
    }
}