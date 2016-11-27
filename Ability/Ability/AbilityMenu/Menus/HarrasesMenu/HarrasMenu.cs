namespace Ability.AbilityMenu.Menus.HarrasesMenu
{
    using Ability.AbilityMenu.Items;

    using Ensage.Common.Menu;

    internal class HarrasMenu
    {
        #region Public Methods and Operators

        public static Menu Create(string name)
        {
            var menu = new Menu(name, name, textureName: name);
            menu.AddItem(Togglers.OnSight(name));
            menu.AddItem(Togglers.OnAttack(name));
            menu.AddItem(Sliders.MinManaCheck(name));
            menu.AddItem(Sliders.MinManaCheck(name, true));
            return menu;
        }

        #endregion
    }
}