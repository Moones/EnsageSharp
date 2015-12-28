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
            if (name != "antimage_mana_void" && name != "necrolyte_reapers_scythe" && name != "item_cyclone")
            {
                menu.AddItem(Togglers.OnSight(name));
                menu.AddItem(Togglers.UnderTower(name));
                menu.AddItem(Togglers.OnChannel(name));
                menu.AddItem(Togglers.OnChainStun(name));
                menu.AddItem(Togglers.OnCast(name));
            }
            else
            {
                menu.AddItem(Togglers.OnSight(name));
                menu.AddItem(Togglers.UnderTower(name, false));
                menu.AddItem(Togglers.OnChannel(name));
                menu.AddItem(Togglers.OnChainStun(name, false));
                menu.AddItem(Togglers.OnCast(name, false));
            }
            menu.AddItem(Sliders.MinManaCheck(name));
            menu.AddItem(Sliders.MinManaCheck(name, true));
            return menu;
        }

        #endregion
    }
}