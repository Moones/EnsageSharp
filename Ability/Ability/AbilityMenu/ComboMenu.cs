namespace Ability.AbilityMenu
{
    using Ensage.Common.Menu;

    internal class ComboMenu
    {
        #region Public Methods and Operators

        public static void AddAbility(string name, bool defaultValue = true)
        {
            if (name == "antimage_mana_void" || name == "axe_culling_blade")
            {
                return;
            }
            MainMenu.ComboKeysMenu.Item("comboAbilitiesToggler").GetValue<AbilityToggler>().Add(name, defaultValue);
        }

        #endregion
    }
}