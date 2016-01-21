namespace Ability.AbilityMenu.Menus.BuffsMenu
{
    using System.Collections.Generic;
    using System.Linq;

    using Ability.Drawings;
    using Ability.ObjectManager;

    using Ensage;
    using Ensage.Common.AbilityInfo;
    using Ensage.Common.Menu;

    internal class Buffs
    {
        #region Static Fields

        public static Menu BuffsMenu;

        public static Dictionary<string, Menu> BuffsMenuDictionary;

        public static bool BuffsTogglerCreated;

        private static Dictionary<string, bool> buffsToggler;

        #endregion

        #region Public Methods and Operators

        public static void AddAllBuffs(Ability[] spells, Item[] myItems1)
        {
            buffsToggler = new Dictionary<string, bool>();
            foreach (var spell in
                from spell in spells
                let data = AbilityDatabase.Find(spell.Name)
                where data != null && data.IsBuff
                select spell)
            {
                AddBuff(spell);
                RangeDrawing.AddRange(spell);
            }

            foreach (var spell in
                from spell in myItems1
                let data = AbilityDatabase.Find(spell.Name)
                where data != null && data.IsBuff
                select spell)
            {
                AddBuff(spell);
                RangeDrawing.AddRange(spell);
            }

            if (BuffsTogglerCreated)
            {
                MainMenu.Menu.AddItem(new MenuItem("buffsToggler", "Buffs:").SetValue(new AbilityToggler(buffsToggler)));
            }
        }

        #endregion

        #region Methods

        private static void AddBuff(Ability spell)
        {
            MyAbilities.OffensiveAbilities.Add(spell.Name + "buff", spell);
            if (!BuffsTogglerCreated)
            {
                BuffsTogglerCreated = true;
                buffsToggler.Add(spell.Name, true);
                MainMenu.AbilitiesMenu.AddSubMenu(BuffsMenu);
            }
            else
            {
                buffsToggler.Add(spell.Name, true);
            }

            var menu = BuffMenu.Create(spell.Name);
            BuffsMenuDictionary.Add(spell.Name, menu);
            BuffsMenu.AddSubMenu(menu);
        }

        #endregion
    }
}