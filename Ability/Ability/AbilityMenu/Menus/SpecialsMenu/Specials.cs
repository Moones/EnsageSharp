namespace Ability.AbilityMenu.Menus.SpecialsMenu
{
    using System.Collections.Generic;
    using System.Linq;

    using Ability.Drawings;
    using Ability.ObjectManager;

    using Ensage;
    using Ensage.Common.AbilityInfo;
    using Ensage.Common.Extensions;
    using Ensage.Common.Menu;

    internal class Specials
    {
        #region Static Fields

        public static Menu SpecialsMenu;

        public static Dictionary<string, Menu> SpecialsMenuDictionary;

        public static bool SpecialsTogglerCreated;

        private static Dictionary<string, bool> specialsToggler;

        #endregion

        #region Public Methods and Operators

        public static void AddAllSpecials(Ability[] spells, Item[] myItems1)
        {
            specialsToggler = new Dictionary<string, bool>();
            foreach (var spell in
                from spell in spells
                let data = AbilityDatabase.Find(spell.Name)
                where
                    spell.Name != "zuus_thundergods_wrath" && data != null
                    && (data.TrueSight || data.WeakensEnemy || data.IsPurge)
                select spell)
            {
                AddSpecial(spell);
                RangeDrawing.AddRange(spell);
            }

            foreach (var spell in
                from spell in myItems1
                let data = AbilityDatabase.Find(spell.Name)
                where spell.Name != "item_gem" && data != null && (data.TrueSight || data.WeakensEnemy || data.IsPurge)
                select spell)
            {
                AddSpecial(spell);
                RangeDrawing.AddRange(spell);
            }

            if (AbilityMain.Me.ClassID == ClassID.CDOTA_Unit_Hero_Rubick)
            {
                var spellSteal = AbilityMain.Me.FindSpell("rubick_spell_steal");
                if (spellSteal != null)
                {
                    AddSpecial(spellSteal);
                    RangeDrawing.AddRange(spellSteal);
                }
            }

            if (SpecialsTogglerCreated)
            {
                MainMenu.Menu.AddItem(
                    new MenuItem("specialsToggler", "Specials:").SetValue(new AbilityToggler(specialsToggler)));
            }
        }

        public static void AddSpecial(Ability spell)
        {
            MyAbilities.OffensiveAbilities.Add(spell.Name + "special", spell);
            if (!SpecialsTogglerCreated)
            {
                SpecialsTogglerCreated = true;
                specialsToggler.Add(spell.Name, true);
                MainMenu.AbilitiesMenu.AddSubMenu(SpecialsMenu);
            }
            else
            {
                specialsToggler.Add(spell.Name, true);
            }

            var menu = SpecialMenu.Create(spell.Name, spell);
            SpecialsMenuDictionary.Add(spell.Name, menu);
            SpecialsMenu.AddSubMenu(menu);
        }

        #endregion
    }
}