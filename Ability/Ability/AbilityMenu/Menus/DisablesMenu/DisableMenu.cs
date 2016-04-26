namespace Ability.AbilityMenu.Menus.DisablesMenu
{
    using Ability.AbilityMenu.Items;

    using Ensage;
    using Ensage.Common.Extensions;
    using Ensage.Common.Menu;

    internal class DisableMenu
    {
        #region Public Methods and Operators

        public static Menu Create(string name, Ability ability)
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

            if (ability.IsAbilityBehavior(AbilityBehavior.AreaOfEffect, name)
                || ability.IsAbilityBehavior(AbilityBehavior.Point, name) || ability.IsSkillShot())
            {
                menu.AddItem(
                    new MenuItem(name + "minstraighttime", "Minimum enemy straight time (ms): ").SetValue(
                        new Slider(600, 0, 5000))
                        .SetTooltip(
                            "Enemy has to walk straight for this amount of ms in order for this ability to be used"));
            }

            return menu;
        }

        #endregion
    }
}