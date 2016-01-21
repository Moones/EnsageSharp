namespace Ability.AbilityMenu.Menus.HealsMenu
{
    using Ability.AbilityMenu.Items;

    using Ensage.Common.Menu;

    internal class HealMenu
    {
        #region Public Methods and Operators

        public static Menu Create(string name)
        {
            var menu = new Menu(name, name, textureName: name);
            if (name == "item_soul_ring")
            {
                menu.AddItem(
                    new MenuItem(name + "useBeforeCast", "Use when about to cast mana requiring ability").SetValue(true));
                menu.AddItem(new MenuItem(name + "minHp", "Minimum HP to use: ").SetValue(new Slider(200, 0, 1000)))
                    .SetTooltip("If your HP is below specified value, ability will not be used");
                return menu;
            }

            if (!(name == "item_magic_wand" || name == "item_magic_stick"))
            {
                menu.AddItem(Togglers.UseOnAllies(name));
            }

            if (name == "item_arcane_boots")
            {
                menu.AddItem(Sliders.MissingManaMin(name));
                menu.AddItem(Sliders.ManaPercentBelow(name));
            }
            else
            {
                menu.AddItem(Sliders.MissingHpMin(name));
                menu.AddItem(Sliders.HpPercentBelow(name));
            }

            if (name == "item_mekansm" || name == "item_guardian_greaves" || name == "chen_hand_of_god")
            {
                menu.AddItem(
                    new MenuItem(name + "minalliesheal", "Minimum of healed allies: ").SetValue(
                        new StringList(new[] { "1", "2", "3", "4" }, 1)));
                menu.AddItem(
                    new MenuItem(name + "waitrange", "Wait Range: ").SetValue(new Slider(2000, 1000, 6000))
                        .SetTooltip(
                            "If theres enabled ally hero in specified range, Ability# will wait for this hero to come in heal range"));
            }

            if (name == "item_arcane_boots")
            {
                menu.AddItem(
                    new MenuItem(name + "waitrange", "Wait Range: ").SetValue(new Slider(2000, 1000, 6000))
                        .SetTooltip(
                            "If theres enabled ally hero in specified range, Ability# will wait for this hero to come in heal range"));
            }
            else if (name != "item_urn_of_shadows")
            {
                menu.AddItem(
                    new MenuItem(name + "minenemiesaround", "Minimum of enemies near: ").SetValue(
                        new StringList(new[] { "1", "2", "3", "4", "5" })));
                menu.AddItem(
                    new MenuItem(name + "usenearbool", "Use when near selected enemy: ").SetValue(true)
                        .SetTooltip("Use the ability only when affected target is near selected enemy"));
                menu.AddItem(Togglers.UseNear(name));
            }

            return menu;
        }

        #endregion
    }
}