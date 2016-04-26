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
            }

            MainMenu.ComboKeysMenu.Item("comboAbilitiesToggler").GetValue<AbilityToggler>().Add(name, defaultValue);
            MainMenu.ComboKeysMenu.Item("Ability#.ComboOrder")
                .GetValue<PriorityChanger>()
                .Add(name, 10 - (uint)ComboOrder.GetComboOrder(ability, false));
        }

        #endregion
    }
}