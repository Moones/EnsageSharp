namespace Ability.AbilityMenu
{
    using System;

    using Ability.AbilityMenu.Menus.BuffsMenu;
    using Ability.AbilityMenu.Menus.NukesMenu;
    using Ability.Casting;
    using Ability.ObjectManager;

    using Ensage;
    using Ensage.Common.Menu;
    using Ensage.Common.Objects;

    internal class ComboMenu
    {
        #region Public Methods and Operators

        public static void AddAbility(string name, Ability ability, bool defaultValue = true)
        {
            if (name == "antimage_mana_void" || name == "axe_culling_blade")
            {
                return;
            }

            switch (name)
            {
                case "item_blink":
                    MyAbilities.OffensiveAbilities.Add(ability.StoredName() + "blink", ability);
                    break;
                case "antimage_blink":
                    MyAbilities.OffensiveAbilities.Add(ability.StoredName() + "blink", ability);
                    break;
                case "queenofpain_blink":
                    MyAbilities.OffensiveAbilities.Add(ability.StoredName() + "blink", ability);
                    break;
                case "invoker_emp":
                    {
                        var menu = NukeMenu.Create("invoker_emp", ability);
                        Nukes.NukesMenuDictionary.Add("invoker_emp", menu);
                        Nukes.NukesMenu.AddSubMenu(menu);
                        MyAbilities.OffensiveAbilities.Add("invoker_empnuke", ability);
                    }

                    break;
                case "invoker_forge_spirit":
                    {
                        var menu = BuffMenu.Create("invoker_forge_spirit");
                        Buffs.BuffsMenuDictionary.Add("invoker_forge_spirit", menu);
                        Buffs.BuffsMenu.AddSubMenu(menu);
                        MyAbilities.OffensiveAbilities.Add("invoker_forge_spiritbuff", ability);
                    }

                    break;
                case "invoker_chaos_meteor":
                    {
                        var menu = NukeMenu.Create("invoker_chaos_meteor", ability);
                        Nukes.NukesMenuDictionary.Add("invoker_chaos_meteor", menu);
                        Nukes.NukesMenu.AddSubMenu(menu);
                        MyAbilities.OffensiveAbilities.Add("invoker_chaos_meteornuke", ability);
                    }

                    break;
                case "skywrath_mage_mystic_flare":
                    {
                        var menu = NukeMenu.Create("skywrath_mage_mystic_flare", ability);
                        Nukes.NukesMenuDictionary.Add("skywrath_mage_mystic_flare", menu);
                        Nukes.NukesMenu.AddSubMenu(menu);
                        MyAbilities.OffensiveAbilities.Add("skywrath_mage_mystic_flarenuke", ability);
                    }

                    break;
            }

            MainMenu.ComboKeysMenu.Item("comboAbilitiesToggler").GetValue<AbilityToggler>().Add(name, defaultValue);
            MainMenu.ComboKeysMenu.Item("Ability#.ComboOrder")
                .GetValue<PriorityChanger>()
                .Add(name, 10 - (uint)ComboOrder.GetComboOrder(ability, false));
        }

        #endregion
    }
}