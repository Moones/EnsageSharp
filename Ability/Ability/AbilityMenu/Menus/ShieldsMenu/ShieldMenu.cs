namespace Ability.AbilityMenu.Menus.ShieldsMenu
{
    using Ability.AbilityMenu.Items;

    using Ensage;
    using Ensage.Common.Extensions;
    using Ensage.Common.Menu;

    internal class ShieldMenu
    {
        #region Public Methods and Operators

        public static Menu Create(string name, Ability ability)
        {
            var menu = new Menu(name, name, textureName: name);
            if (!ability.IsAbilityBehavior(AbilityBehavior.NoTarget)
                || (name == "item_pipe" || name == "item_buckler" || name == "omniknight_guardian_angel"
                    || name == "item_crimson_guard"))
            {
                menu.AddItem(Togglers.UseOnAllies(name));
            }

            menu.AddItem(Sliders.MissingHpMin(name));
            menu.AddItem(Sliders.HpPercentBelow(name));
            if (name == "item_pipe" || name == "item_buckler" || name == "omniknight_guardian_angel"
                || name == "item_crimson_guard")
            {
                menu.AddItem(
                    new MenuItem(name + "minalliesaffect", "Minimum affected allies: ").SetValue(
                        new StringList(new[] { "1", "2", "3", "4" }, 1)));
            }

            menu.AddItem(
                new MenuItem(name + "minenemiesaround", "Minimum of enemies near: ").SetValue(
                    new StringList(new[] { "1", "2", "3", "4", "5" })));
            menu.AddItem(
                new MenuItem(name + "usenearbool", "Use when near selected enemy: ").SetValue(true)
                    .SetTooltip("Use the ability only when affected target is near selected enemy"));
            menu.AddItem(Togglers.UseNear(name));
            menu.AddItem(Togglers.OnDisableAlly(name));
            return menu;
        }

        #endregion
    }
}