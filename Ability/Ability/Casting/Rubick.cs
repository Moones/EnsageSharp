namespace Ability.Casting
{
    using System;
    using System.Collections.Generic;

    using Ability.AbilityMenu.Menus.SpecialsMenu;
    using Ability.ObjectManager;
    using Ability.ObjectManager.Heroes;
    using Ability.OnUpdate;

    using Ensage;
    using Ensage.Common;
    using Ensage.Common.Extensions;
    using Ensage.Common.Menu;

    internal class Rubick
    {
        #region Static Fields

        public static Dictionary<string, bool> CdDictionary = new Dictionary<string, bool>();

        public static Dictionary<string, Ability> LastCastedDictionary = new Dictionary<string, Ability>();

        public static Dictionary<string, float> MyCdDictionary = new Dictionary<string, float>();

        #endregion

        #region Public Methods and Operators

        public static bool Cooldown(string name)
        {
            return !MyCdDictionary.ContainsKey(name) || MyCdDictionary[name] < Utils.TickCount;
        }

        public static void Game_OnUpdate(EventArgs args)
        {
            if (!OnUpdateChecks.CanUpdate() || !Specials.SpecialsMenuDictionary.ContainsKey("rubick_spell_steal"))
            {
                return;
            }

            foreach (var hero in EnemyHeroes.Heroes)
            {
                var heroName = NameManager.Name(hero);
                if (!LastCastedDictionary.ContainsKey(heroName))
                {
                    LastCastedDictionary.Add(heroName, null);
                }

                if (Specials.SpecialsMenuDictionary["rubick_spell_steal"].Item(heroName) == null)
                {
                    Specials.SpecialsMenuDictionary["rubick_spell_steal"].AddItem(
                        new MenuItem(heroName, hero.ClassID.ToString().Substring("CDOTA_Unit_Hero_".Length) + ":")
                            .SetValue(new AbilityToggler(new Dictionary<string, bool>())));
                }

                if (!EnemyHeroes.AbilityDictionary.ContainsKey(heroName))
                {
                    continue;
                }

                foreach (var ability in EnemyHeroes.AbilityDictionary[heroName])
                {
                    var name = NameManager.Name(ability);
                    if (!CdDictionary.ContainsKey(name))
                    {
                        CdDictionary.Add(name, false);
                    }

                    var cd = CdDictionary[name];
                    if ((ability.Cooldown > 0
                         || (ability.IsAbilityBehavior(AbilityBehavior.Toggle, name) && ability.IsToggled)) && !cd)
                    {
                        CdDictionary[name] = true;
                        LastCastedDictionary[heroName] = ability;
                    }

                    if ((ability.Cooldown <= 0
                         || (ability.IsAbilityBehavior(AbilityBehavior.Toggle, name) && !ability.IsToggled)) && cd)
                    {
                        CdDictionary[name] = false;
                    }

                    if (ability.IsAbilityBehavior(AbilityBehavior.Passive, name) || name == "invoker_invoke"
                        || name == "invoker_quas" || name == "invoker_wex" || name == "invoker_exort")
                    {
                        continue;
                    }

                    var d =
                        Specials.SpecialsMenuDictionary["rubick_spell_steal"].Item(heroName).GetValue<AbilityToggler>();
                    if (!d.Dictionary.ContainsKey(name))
                    {
                        d.Add(name, ability.AbilityType == AbilityType.Ultimate);
                    }
                }
            }
        }

        public static Ability LastCasted(string name)
        {
            return !LastCastedDictionary.ContainsKey(name) ? null : LastCastedDictionary[name];
        }

        #endregion
    }
}