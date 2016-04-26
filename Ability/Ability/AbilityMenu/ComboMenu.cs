namespace Ability.AbilityMenu
{
    using System;

    using Ability.Casting;
    using Ability.ObjectManager;

    using Ensage;
    using Ensage.Common.Menu;

    internal class ComboMenu
    {
        #region Public Methods and Operators

        public static void AddAbility(string name, Ability ability, bool defaultValue = true)
        {
            if (name == "antimage_mana_void" || name == "axe_culling_blade")
            {
                return;
            }

            if (name == "item_blink")
            {
                MyAbilities.OffensiveAbilities.Add("item_blinkblink", ability);
                MainMenu.BlinkMenu.AddItem(
                    new MenuItem("Ability#.BlinkRange", "BlinkPosition distance from target").SetValue(
                        new Slider(0, 0, 700)));
                MainMenu.BlinkMenu.AddItem(
                    new MenuItem("Ability#.BlinkMaxEnemiesAround", "Maximum enemies around target to use blink")
                        .SetValue(new Slider(2, 0, 3)))
                    .SetTooltip("If theres more enemies around then specified value, blink will not be used");
                MainMenu.OptionsMenu.AddSubMenu(MainMenu.BlinkMenu);
            }

            MainMenu.ComboKeysMenu.Item("comboAbilitiesToggler").GetValue<AbilityToggler>().Add(name, defaultValue);
            MainMenu.ComboKeysMenu.Item("Ability#.ComboOrder")
                .GetValue<PriorityChanger>()
                .Add(name, 10 - (uint)ComboOrder.GetComboOrder(ability, false));
        }

        #endregion
    }
}