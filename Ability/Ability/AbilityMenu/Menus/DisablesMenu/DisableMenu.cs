namespace Ability.AbilityMenu.Menus.DisablesMenu
{
    using Ability.AbilityMenu.Items;

    using Ensage.Common.Menu;

    internal class DisableMenu
    {
        #region Public Methods and Operators

        public static Menu Create(string name)
        {
            var menu = new Menu(name, name, textureName: name);
            menu.AddItem(Togglers.OnSight(name));
            menu.AddItem(Togglers.UnderTower(name));
            menu.AddItem(Togglers.OnChannel(name));
            menu.AddItem(Togglers.OnChainStun(name));
            menu.AddItem(Togglers.OnCast(name));
            return menu;
        }

        #endregion
    }
}