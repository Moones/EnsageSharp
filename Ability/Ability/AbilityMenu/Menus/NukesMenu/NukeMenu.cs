namespace Ability.AbilityMenu.Menus.NukesMenu
{
    using Ability.AbilityMenu.Items;

    using Ensage;
    using Ensage.Common.Extensions;
    using Ensage.Common.Menu;

    internal class NukeMenu
    {
        #region Public Methods and Operators

        public static Menu Create(string name, Ability ability)
        {
            var menu = new Menu(name, name, textureName: name);
            menu.AddItem(Togglers.UseOn(name));
            menu.AddItem(Sliders.MinHealth(name));
            if (name == "zuus_thundergods_wrath")
            {
                menu.AddItem(
                    new MenuItem(name + "minenemykill", "Minimum heroes to kill: ").SetValue(new Slider(1, 1, 5)));
            }

            menu.AddItem(
                new MenuItem(name + "combo", "Use in combo: ").SetValue(true)
                    .SetTooltip("Allows the ability to be used along with other nukes in combo when killstealing"));
            menu.AddItem(Sliders.MinManaCheck(name));
            menu.AddItem(Sliders.MinManaCheck(name, true));
            if (ability.IsAbilityBehavior(AbilityBehavior.AreaOfEffect, name)
                || ability.IsAbilityBehavior(AbilityBehavior.Point, name) || ability.IsSkillShot())
            {
                menu.AddItem(Togglers.OnDisableEnemy(name));
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