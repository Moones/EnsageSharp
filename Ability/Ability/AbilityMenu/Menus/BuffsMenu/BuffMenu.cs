namespace Ability.AbilityMenu.Menus.BuffsMenu
{
    using System.Drawing;

    using Ability.AbilityMenu.Items;

    using Ensage.Common.Menu;

    using Color = SharpDX.Color;

    internal class BuffMenu
    {
        #region Public Methods and Operators

        public static Menu Create(string name)
        {
            var menu = new Menu(name, name, textureName: name);
            menu.AddItem(Togglers.OnSight(name));
            menu.AddItem(Togglers.OnAttack(name));
            if (name == "item_armlet")
            {
                menu.AddItem(
                    new MenuItem(name + "belowhpslider", "Toggle when HP is below: ").SetValue(
                        new Slider(350, 100, 1000)));
                menu.AddItem(
                    new MenuItem(name + "armletdelay", "Toggles delay: ").SetValue(new Slider(1300, 500, 2500)));
                menu.AddItem(
                    new MenuItem(name + "alwaystoggle", "Toggle always: ").SetValue(false)
                        .SetTooltip("Toggle armlet always when HP goes below specified treshold"));
            }

            if (name == "item_satanic")
            {
                menu.AddItem(
                    new MenuItem(name + "missinghpslider", "Use when missing HP is higher then: ").SetValue(
                        new Slider(500, 100, 1000)));
                menu.AddItem(new MenuItem(name + "orsign", "   OR").SetFontStyle(FontStyle.Bold, Color.White));
                menu.AddItem(
                    new MenuItem(name + "belowhpslider", "Use when HP is below: ").SetValue(new Slider(1000, 500, 5000)));
            }

            menu.AddItem(Sliders.MinManaCheck(name));
            menu.AddItem(Sliders.MinManaCheck(name, true));
            return menu;
        }

        #endregion
    }
}