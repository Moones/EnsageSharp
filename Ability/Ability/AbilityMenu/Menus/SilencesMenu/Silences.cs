namespace Ability.AbilityMenu.Menus.SilencesMenu
{
    using System.Collections.Generic;
    using System.Linq;

    using Ability.Drawings;
    using Ability.ObjectManager;

    using Ensage;
    using Ensage.Common.AbilityInfo;
    using Ensage.Common.Menu;

    internal class Silences
    {
        #region Static Fields

        public static Menu SilencesMenu;

        public static Dictionary<string, Menu> SilencesMenuDictionary;

        public static bool SilencesTogglerCreated;

        private static Dictionary<string, bool> silencesToggler;

        #endregion

        #region Public Methods and Operators

        public static void AddAllSilences(Ability[] spells, Item[] myItems1)
        {
            silencesToggler = new Dictionary<string, bool>();
            foreach (var spell in
                from spell in spells
                let data = AbilityDatabase.Find(spell.Name)
                where data != null && data.IsSilence
                select spell)
            {
                AddSilence(spell);
                RangeDrawing.AddRange(spell);
            }

            foreach (var spell in
                from spell in myItems1
                let data = AbilityDatabase.Find(spell.Name)
                where data != null && data.IsSilence
                select spell)
            {
                AddSilence(spell);
                RangeDrawing.AddRange(spell);
            }

            if (SilencesTogglerCreated)
            {
                MainMenu.Menu.AddItem(
                    new MenuItem("silencesToggler", "Silences:").SetValue(new AbilityToggler(silencesToggler)));
            }
        }

        #endregion

        #region Methods

        private static void AddSilence(Ability spell)
        {
            MyAbilities.OffensiveAbilities.Add(spell.Name + "silence", spell);
            if (!SilencesTogglerCreated)
            {
                SilencesTogglerCreated = true;
                silencesToggler.Add(spell.Name, true);
                MainMenu.AbilitiesMenu.AddSubMenu(SilencesMenu);
            }
            else
            {
                silencesToggler.Add(spell.Name, true);
            }

            var menu = SilenceMenu.Create(spell.Name);
            SilencesMenuDictionary.Add(spell.Name, menu);
            SilencesMenu.AddSubMenu(menu);
        }

        #endregion
    }
}