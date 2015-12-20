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
            menu.AddItem(Togglers.UseOnAllies(name));
            menu.AddItem(Sliders.MissingHpMin(name));
            menu.AddItem(Sliders.HpPercentBelow(name));
            if (name == "item_mekansm" || name == "item_guardian_greaves" || name == "chen_hand_of_god")
            {
                menu.AddItem(
                    new MenuItem(name + "minalliesheal", "Minimum healed allies: ").SetValue(
                        new StringList(new[] { "1", "2", "3", "4" }, 1)));
            }
            menu.AddItem(
                new MenuItem(name + "minenemiesaround", "Minimum of enemies near: ").SetValue(
                    new StringList(new[] { "1", "2", "3", "4", "5" }, 1)));
            menu.AddItem(
                new MenuItem(name + "usenearbool", "Use when near selected enemy: ").SetValue(true)
                    .SetTooltip("Use the ability only when affected target is near selected enemy"));
            menu.AddItem(Togglers.UseNear(name));
            return menu;
        }

        #endregion
    }
}